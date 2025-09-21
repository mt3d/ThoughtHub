using Microsoft.AspNetCore.Mvc;
using backend.Data;

namespace backend.Logic.Users
{
	[Route("user")]
	public class UserController(PlatformContext context) : Controller
	{
		// TODO: Authentication required
		//[HttpPut]
		//public async Task<IActionResult> UpdateUser(
		//	string username,
		//	string email,
		//	string password,
		//	string bio,
		//	string profilePic)
		//{
		//	var currentUsername = accessor.GetCurrentUsername();
		//	var user = await context.Users.Where(u => u.Username == currentUsername).FirstOrDefaultAsync();

		//	if (user == null)
		//	{
		//		return NotFound(StatusCodes.Status404NotFound);
		//	}

		//	// TODO: Create a user service?

		//	user.Username = username ?? user.Username;
		//	user.Email = email ?? user.Email;
		//	user.Bio = bio ?? user.Bio;
		//	user.ProfilePic = profilePic ?? user.ProfilePic;

		//	// TODO: Update password in a separate endpoint

		//	await context.SaveChangesAsync();

		//	return Ok(mapper.Map<User, UserDto>(user));
		//}
	}
}
