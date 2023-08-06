using AutoMapper;
using CrudOperations.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperations.Controllers
{
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleController(RoleManager<IdentityRole> Roles)
		{
			_roleManager = Roles;
		}
		[HttpGet]
		public async Task<IActionResult> Index(string SearchValue)
		{
			if (SearchValue is null)
			{
				var Roles = await _roleManager.Roles.Select(x => new RoleViewModel()
				{
					id = x.Id,
					RoleName = x.Name,

				}).ToListAsync();
				return View(Roles);
			}
			else
			{


				var RolesNames = await _roleManager.FindByNameAsync(SearchValue) as IEnumerable<IdentityRole>;
				return View(RolesNames);
			}

		}















		[HttpGet]
		public async Task<IActionResult> Details(string id, string NameView = "Details")
		{
			if (id is null)
				return BadRequest();

			var user = await _roleManager.FindByIdAsync(id);
			if (user is null)
				return NotFound();

			var RoleVm = new RoleViewModel()
			{
				RoleName = user.Name,
				id = user.Id
			};
			return View(NameView, RoleVm);
		}


		#region update
		[HttpGet]

		public async Task<IActionResult> Update(string id)
		{
			return await Details(id, "Update");

		}
		[HttpPost]



		public async Task<IActionResult> Update([FromRoute] string id, RoleViewModel model)
		{
			if (id != model.id)
				return BadRequest(string.Empty);


			if (ModelState.IsValid)
			{
				try
				{
					var x = await _roleManager.FindByIdAsync(model.id);
					x.Name = model.RoleName;

					var Result = await _roleManager.UpdateAsync(x);

					if (Result.Succeeded)
						return RedirectToAction(nameof(Index));
				}
				catch (System.Exception x)
				{

					ModelState.AddModelError(string.Empty, x.Message);
					return View(model);
				}

			}
			else
			{
				return View(model);
			}
			return View(model);


		}

		#endregion


		#region Delete
		[HttpGet]
		public async Task<IActionResult> Delete(string? id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel Model)
		{
			if (id != Model.id)
				return BadRequest(string.Empty);
			if (ModelState.IsValid)
			{

				var Role = await _roleManager.FindByIdAsync(Model.id);

				await _roleManager.DeleteAsync(Role);

				return RedirectToAction(nameof(Index));
			}
			else
			{
				return View(Model);
			}
		}
		#endregion



		#region Create
		[HttpGet]
		public IActionResult Create() 
		{

			return View();	  
		}


		[HttpPost]
		public async Task<IActionResult> Create(RoleViewModel Model)
		{

		  var Role=new IdentityRole()
		  {
			  Name=Model.RoleName,
			  Id=Model.id
		  };

			await _roleManager.CreateAsync(Role);


			return RedirectToAction("index");
		}
		#endregion





	}
}