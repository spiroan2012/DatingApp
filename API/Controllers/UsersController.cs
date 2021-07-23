using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		//private readonly DataContext _context;
		private readonly IUserRepository _usersRepository;
		private readonly IMapper _mapper;

		public UsersController(IUserRepository usersRepository, IMapper mapper)
		{
			_usersRepository = usersRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
		{
			var users = await _usersRepository.GetMembersAsync();
			return Ok(users);
		}

		[HttpGet("{username}")]
		public async Task<ActionResult<MemberDto>> GetUsers(string username)
		{
			return await _usersRepository.GetMemberAsync(username);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
		{
			var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var user = await _usersRepository.GetUserByUsernameAsync(username);

			_mapper.Map(memberUpdateDto, user);

			_usersRepository.Update(user);

			if (await _usersRepository.SaveAllAsync()) return NoContent();

			return BadRequest("Failed to update user");
		}
	}
}
