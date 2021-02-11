

using CorporateQnA.Services.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Services
{
	public class AuthService : IAuthService
	{
		private readonly SignInManager<AppIdentityUser> signInManager;
		private readonly UserManager<AppIdentityUser> userManager;
		private readonly IIdentityServerInteractionService interactionService;
		private readonly IUserService userService;

		public AuthService(SignInManager<AppIdentityUser> signInManager, UserManager<AppIdentityUser> userManager,
			  IIdentityServerInteractionService interactionService, IUserService userService)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.interactionService = interactionService;
			this.userService = userService;
			this.userManager = userManager;
			this.interactionService = interactionService;
			this.signInManager = signInManager;
		}

		public async Task<bool> Login(string email, string password)
		{
			var getUser = await this.userManager.FindByEmailAsync(email);

			if (getUser == null)
			{
				return false;
			}

			var signinResult = await this.signInManager.PasswordSignInAsync(getUser, password, false, false);
			if (signinResult.Succeeded)
			{
				return true;
			}
			return false;
		}

		public async Task<List<string>> Register(string name, string username, string email, string password, string location, string designation, string department, string profileImage)
		{

			List<string> errors = new List<string>();
			var user = await this.userManager.FindByEmailAsync(email);

			if (user != null)
			{
				errors.Add("User already exists");
				return errors; ;
			}

			var appUser = new AppUser
			{
				Department = department,
				Location = location,
				Email = email,
				FullName = name,
				Designation = designation,
				ProfileImage=profileImage
			};

			var validators = this.userManager.PasswordValidators;

			foreach (var validator in validators)
			{
				var passwordResult = await validator.ValidateAsync(this.userManager, null, password);

				if (!passwordResult.Succeeded)
				{
					foreach (var error in passwordResult.Errors)
					{
						errors.Add(error.Description);
					}
				}
			}
			if (errors.Count != 0)
			{
				return errors;
			}

			var userId = this.userService.Create(appUser);
			var newUser = new AppIdentityUser
			{
				UserID = userId,
				Email = email,
				UserName = username
			};

			var result = await this.userManager.CreateAsync(newUser, password);

			if (result.Succeeded)
			{
				await this.signInManager.SignInAsync(newUser, false);
				return null;
			}
			else
			{
				foreach (var i in result.Errors)
				{
					errors.Add(i.Description);
				}
			}
			
			return null;
		}

		public async Task<string> Logout(string logoutId)
		{
			await signInManager.SignOutAsync();
			var logoutRequest = await this.interactionService.GetLogoutContextAsync(logoutId);
			return logoutRequest.PostLogoutRedirectUri;
		}
	}
}
