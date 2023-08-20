using CrudOperations.Helper;
using CrudOperations.Models;
using Dal_CrudOperations.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace CrudOperations.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationsUser> _usermanager; // To sign IN User;
		private readonly SignInManager<ApplicationsUser> _signManager  /*to make User Create*/;
		private readonly IEmailSettings _Mailmanager;

		public AccountController(UserManager<ApplicationsUser> usermanager, SignInManager<ApplicationsUser> SignManager, IEmailSettings mailmanager)
		{
			_usermanager = usermanager;
			_signManager = SignManager;
			_Mailmanager = mailmanager;
		}

		#region Register
		[HttpGet]
		public IActionResult Register()
		{

			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM Model)
		{
			if (ModelState.IsValid)
			{
				var User = new ApplicationsUser()
				{
					Fname = Model.Fname,
					Lname = Model.Lname,
					UserName = Model.Email.Split("@")[0],
					Email = Model.Email,
					IsAgree = Model.IsAgree,
					PhoneNumber = string.Concat("+2", Model.PhoneNumber)

				};

				var Resulate = await _usermanager.CreateAsync(User, Model.Password);
				if (Resulate.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}
				else
				{
					foreach (var item in Resulate.Errors)
					{
						ModelState.AddModelError(string.Empty, item.Description);
					}
				}
			}

			return View(Model);
		}
		#endregion





		#region Login

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Login(LoginVM UserLogin)
		{
			if (ModelState.IsValid)
			{
				var User = await _usermanager.FindByEmailAsync(UserLogin.Email);
				if (User is not null)
				{
					var flag = await _usermanager.CheckPasswordAsync(User, UserLogin.Password);
					if (flag)
					{
						var Resulate = await _signManager.PasswordSignInAsync(User, UserLogin.Password, UserLogin.RememberMe, false);
						if (Resulate.Succeeded)
						{
							return RedirectToAction("Index", "Employee");
						}
					}
					ModelState.AddModelError(string.Empty, "Password is not Correct Yet");

				}
				ModelState.AddModelError(string.Empty, "Email is not Registeration Yet");

			}
			return View();
		}
		#endregion


		#region SignOut

		public new async Task<IActionResult> SignOut()
		{
			await _signManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}



		#endregion


		#region Forget Password
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]





		public async Task<IActionResult> SendEmail(ForgetPasswordVM Model)
		{
			if (ModelState.IsValid)
			{
				var User = await _usermanager.FindByEmailAsync(Model.Email);

				if (User is not null)
				{
					var TokenResetPassword = await _usermanager.GeneratePasswordResetTokenAsync(User);
					var PasswordLink = Url.Action("ResetPassword", "account", new { email = Model.Email, token = TokenResetPassword }, Request.Scheme);

					Email email = new Email()
					{
						Subject = "Reset PassWord ",
						To = User.Email,
						Body = PasswordLink,
					};
					//_mailmanager.SendMail(email);
					_Mailmanager.SendEmail(email);
					return RedirectToAction(nameof(CheckBox));
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email is not Existed");
					return View("ForgetPassword", Model);
				}

			}
			return View("ForgetPassword", Model);
		}









		//public async Task<IActionResult> SendSMS(ForgetPasswordVM Model)
		//{
		//	if (ModelState.IsValid)
		//	{




		//		var User = await _usermanager.FindByEmailAsync(Model.Email);

		//		if (User is not null)
		//		{
		//			var TokenResetPassword = await _usermanager.GeneratePasswordResetTokenAsync(User);
		//			var PasswordLink = Url.Action("ResetPassword", "account", new { email = Model.Email, token = TokenResetPassword }, Request.Scheme);

		//			var sms = new SmsMessage()
		//			{
		//				Body = PasswordLink,
		//				NumberPhone = User.PhoneNumber
		//			};
		//			_smsServices.send(sms);

		//			return Ok("Check Phone Number");
		//		}
		//		else
		//		{
		//			ModelState.AddModelError(string.Empty, "Email is not Existed");
		//			return View("ForgetPassword", Model);
		//		}

		//	}
		//	return View("ForgetPassword", Model);
		//}














		public IActionResult CheckBox()
		{
			return View();
		}

		#endregion

		#region Reset Password 
		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["Email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetVM model)
		{
			var email = TempData["email"] as string;
			var token = TempData["token"] as string;
			var user = await _usermanager.FindByEmailAsync(email);

			if (ModelState.IsValid)
			{
				var result = await _usermanager.ResetPasswordAsync(user, token, model.NewPassword);
				if (result.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(string.Empty, item.Description);
				}
			}
			return View(model);


		}

		#endregion



		#region Auth With Google 

		//public IActionResult LoginWithAuth()
		//{
		//	var prop = new AuthenticationProperties
		//	{
		//		RedirectUri = Url.Action("GoogleResponse")
		//	};
		//	return Challenge(prop, GoogleDefaults.AuthenticationScheme);
		//}




		//public async Task<IActionResult> GoogleResponse(string issuer)
		//{
		//	var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
		//	var cliams = result.Principal.Identities.FirstOrDefault().Claims.Select(x =>
		//	new
		//	{
		//		x.Issuer,
		//		x.OriginalIssuer,
		//		x.Type,
		//		x.Value
		//	});
		//	return RedirectToAction("index", "home");
		//}
		#endregion



	}
}
