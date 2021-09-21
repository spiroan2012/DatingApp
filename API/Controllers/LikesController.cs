using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{

	[Authorize]
	public class LikesController : BaseApiController
	{
		private readonly IUnitOfWork _unitOfWork;

		public LikesController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpPost("{username}")]
		public async Task<ActionResult> AddLike(string username)
		{
			var sourceId = User.GetUserId();
			var likedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
			var sourceUser = await _unitOfWork.LikesRepository.GetUserWithLike(sourceId);

			if (likedUser == null) return NotFound();

			if (sourceUser.UserName == likedUser.UserName) return BadRequest("You cannnot like yourself");

			var userLike = await _unitOfWork.LikesRepository.GetUserLike(sourceId, likedUser.Id);

			if (userLike != null)
			{
				sourceUser.LikedUsers.Remove(userLike);

				if (await _unitOfWork.Complete())
				{
					return Ok("You have removed the like from " +likedUser.KnownAs) ;
				}
			}
			else
			{
				userLike = new UserLike()
				{
					SourceUserId = sourceId,
					LikedUserId = likedUser.Id
				};

				sourceUser.LikedUsers.Add(userLike);

				if (await _unitOfWork.Complete())
				{
					return Ok("You have likes "+likedUser.KnownAs);
				}
			}


			return BadRequest("Failed to like user");
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
		{
			likesParams.UserId = User.GetUserId();
			var users = await _unitOfWork.LikesRepository.GetUserLikes(likesParams);

			Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

			return Ok(users);
		}
	}
}
