using AutoMapper;
using BLL_CrudOperations.InterFaces;
using CrudOperations.Helper;
using CrudOperations.Models;
using Dal_CrudOperations.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudOperations.Controllers
{
	//IUniteOFWork._uniteOFWork.EmployeeRepoRepo
	[Authorize]
	public class EmployeeController : Controller
	{

		private readonly IMapper _mapping;
		private readonly IUniteOFWork _uniteOFWork;


		public EmployeeController(/*IEmployeeRepo employee,*/ IMapper mapping, IUniteOFWork uniteOFWork)
		{
			//_uniteOFWork.EmployeeRepo = employee;
			_mapping = mapping;
			_uniteOFWork = uniteOFWork;
		}
		[AllowAnonymous]
		public async Task<IActionResult> Index(string SearchValue)
		{


			IEnumerable<Employee> employees;
			if (SearchValue is null)
				employees = await _uniteOFWork.EmployeeRepo.GetAll();
			else
				employees = await _uniteOFWork.EmployeeRepo.Search(SearchValue);


			var EmployeeVM = _mapping.Map<IEnumerable<Employee>, IEnumerable<EmployeeVm>>(employees);
			return View(EmployeeVM);
		}

		#region Create
		[HttpGet]

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(EmployeeVm employeeVm)
		{


			if (ModelState.IsValid)
			{
				employeeVm.ImageName = await DocumentSetting.UploadFillesAsync(employeeVm.Image, "Image");

				var Employee = _mapping.Map<EmployeeVm, Employee>(employeeVm);
				try
				{
					await _uniteOFWork.EmployeeRepo.Create(Employee);
					await _uniteOFWork.Complit();
					return RedirectToAction("Index");
				}
				catch (System.Exception x)
				{

					ModelState.AddModelError(string.Empty, x.Message);
					return View(employeeVm);

				}
			}
			else
			{
				return View(employeeVm);
			}

		}
		#endregion
		#region Details
		[HttpGet]
		public async Task<IActionResult> Details(int? id, string NameView = "Details")
		{
			if (id is null)
				return BadRequest(string.Empty);

			var Employees = await _uniteOFWork.EmployeeRepo.GetById(id.Value);
			if (Employees is null)
				return NotFound();

			var EmployeeVM = _mapping.Map<Employee, EmployeeVm>(Employees);
			return View(NameView, EmployeeVM);
		}
		#endregion



		#region Delete
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		public async Task<IActionResult> Delete([FromRoute] int id, EmployeeVm EmployeeVM)
		{
			if (id != EmployeeVM.id)
				return BadRequest(string.Empty);
			if (ModelState.IsValid)
			{
				DocumentSetting.DeleteFile(EmployeeVM.ImageName, "Image");
				var employee = _mapping.Map<EmployeeVm, Employee>(EmployeeVM);
				_uniteOFWork.EmployeeRepo.Delete(employee);
				await _uniteOFWork.Complit();
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return View(EmployeeVM);
			}
		}
		#endregion

		#region update
		[HttpGet]

		public async Task<IActionResult> Update(int id)
		{
			return await Details(id, "Update");

		}
		[HttpPost]
		public async Task<IActionResult> Update([FromRoute] int id, EmployeeVm EmployeeVM)
		{
			if (id != EmployeeVM.id)
				return BadRequest(string.Empty);
			if (ModelState.IsValid)
			{
				DocumentSetting.DeleteFile(EmployeeVM.ImageName, "Image");
				var employee = _mapping.Map<EmployeeVm, Employee>(EmployeeVM);
				_uniteOFWork.EmployeeRepo.Update(employee);
				await _uniteOFWork.Complit();
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return View(EmployeeVM);
			}

		}



		#endregion


	}
}
