using AutoMapper;
using CrudOperations.Models;
using Dal_CrudOperations.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperations.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationsUser> _usermanager;
		private readonly SignInManager<ApplicationsUser> _signinmanager;
		private readonly IMapper mapper;

		public UserController(UserManager<ApplicationsUser> usermanager, SignInManager<ApplicationsUser> signinmanager, IMapper mapper)
		{
			_usermanager = usermanager;
			_signinmanager = signinmanager;
			this.mapper = mapper;
		}


		[HttpGet]
		public async Task<IActionResult> Index(string SearchValue)
		{
			if (SearchValue is not null)
			{
				var Users = await _usermanager.FindByEmailAsync(SearchValue);
				var UserVm = new UserVM
				{
					id = Users.Id,
					Fname = Users.Fname,
					Lname = Users.Lname,
					Email = Users.Email,
					PhoneNumber = Users.PhoneNumber,
					Role = _usermanager.GetRolesAsync(Users).Result
				};

				return View(new List<UserVM>() { UserVm });

			}


			var users = _usermanager.Users.Select(x => new UserVM
			{
				id = x.Id,
				Fname = x.Fname,
				Lname = x.Lname,
				Email = x.Email,
				PhoneNumber = x.PhoneNumber,
				Role = _usermanager.GetRolesAsync(x).Result

			}) as IEnumerable<UserVM>;
			return View(users);
		}


		#region Create
		//[HttpGet]
		//public IActionResult Create()
		//{
		//	return View();
		//}



		//[HttpPost]
		//public async Task<IActionResult> Create(RegisterVM Model)
		//{
		//	  if(ModelState.IsValid)
		//		{
		//		try
		//		{
		//			var User = new ApplicationsUser()
		//			{
		//				Fname = Model.Fname,
		//				Lname = Model.Lname,
		//				UserName = Model.Email.Split("@")[0],
		//				Email = Model.Email,
		//				IsAgree = Model.IsAgree
		//			};
		//			await _usermanager.CreateAsync(User);
		//			return RedirectToAction("index");
		//		}
		//		catch (System.Exception x)
		//		{
		//			return View(Model);
		//		}

		//		}
		//	return View(Model);

		//}
		#endregion




		[HttpGet]
		public async Task<IActionResult> Details(string id, string NameView = "Details")
		{
			if (id is null)
				return BadRequest();

			var user = await _usermanager.FindByIdAsync(id);
			if (user is null)
				return NotFound();

			var Uservm = mapper.Map<UserVM>(user);
			#region Old mapping
			//var Uservm = new UserVM()
			//{
			//	Fname = user.Fname, Lname = user.Lname,
			//	Email = user.Email,
			//	PhoneNumber = user.PhoneNumber,
			//	Role = IEnumerable<string>(user);
			//   }
			#endregion
			return View(NameView, Uservm);


		}


		#region update
		[HttpGet]

		public async Task<IActionResult> Update(string id)
		{
			return await Details(id, "Update");

		}
		[HttpPost]



		public async Task<IActionResult> Update([FromRoute] string id, UserVM Users)
		{
			if (id != Users.id)
				return BadRequest(string.Empty);


			if (ModelState.IsValid)
			{
				try
				{
					var userApps = await _usermanager.FindByIdAsync(Users.id);
					userApps.PhoneNumber = Users.PhoneNumber;
					userApps.Fname = Users.Fname;
					userApps.Lname = Users.Lname;
					userApps.Email = Users.Email;

					var resulat = await _usermanager.UpdateAsync(userApps);

					if (resulat.Succeeded)
						return RedirectToAction(nameof(Index));
				}
				catch (System.Exception x)
				{

					ModelState.AddModelError(string.Empty, x.Message);
				}

			}
			else
			{
				return View(Users);
			}
			return View(Users);


		}



		#region Delete
		[HttpGet]
		public async Task<IActionResult> Delete(string? id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		public async Task<IActionResult> Delete([FromRoute] string id, UserVM Model)
		{
			if (id != Model.id)
				return BadRequest(string.Empty);
			if (ModelState.IsValid)
			{

				var user = await _usermanager.FindByIdAsync(Model.id);

				await _usermanager.DeleteAsync(user);

				return RedirectToAction(nameof(Index));
			}
			else
			{
				return View(Model);
			}
		}
		#endregion









		#endregion




	}
}
