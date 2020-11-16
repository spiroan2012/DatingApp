using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly DataContext _context;
		public UsersController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}

		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<AppUser>> GetUsers(int id)
		{
			return await _context.Users.FindAsync(id);
		}
	}
}
