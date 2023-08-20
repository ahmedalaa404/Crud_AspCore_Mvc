using AutoMapper;
using BLL_CrudOperations.InterFaces;
using CrudOperations.Models;
using Dal_CrudOperations.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudOperations.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
	{
		//private readonly IDepartmentsRepo _uniteOFWork.DepartmentsRepo;
		private readonly IMapper _mapper;
		private readonly IUniteOFWork _uniteOFWork;

		public DepartmentController(/*IDepartmentsRepo departmentRepo,*/IMapper Mapper, IUniteOFWork uniteOFWork)
		{
			//_uniteOFWork.DepartmentsRepo = departmentRepo;
			_mapper = Mapper;
			_uniteOFWork = uniteOFWork;
		}

		public async Task<IActionResult> Index(string SearchValue)
		{

			IEnumerable<Department> department;
			if (SearchValue is null)
				department = await _uniteOFWork.DepartmentsRepo.GetAll();
			else
				department = await _uniteOFWork.DepartmentsRepo.Search(SearchValue);

			var DepartmentVm = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentVM>>(department);
			return View(DepartmentVm);
		}

		#region create
		[HttpGet]
		public IActionResult Create()
		{
			return View();

		}

		[HttpPost]
		public async Task<IActionResult> Create(DepartmentVM departmentVM)
		{
			if (ModelState.IsValid)
			{
				var department = _mapper.Map<DepartmentVM, Department>(departmentVM);
				try
				{
					await _uniteOFWork.DepartmentsRepo.Create(department);
					await _uniteOFWork.Complit();
					return RedirectToAction("Index");
				}
				catch (System.Exception x)
				{

					ModelState.AddModelError(string.Empty, x.Message);
					return View(departmentVM);
				}



			}
			else
				return View(departmentVM);

		}

		#endregion


		#region Details
		[HttpGet]
		public async Task<IActionResult> Details(int? id, string NameView = "Details")
		{
			if (id is null)
				return BadRequest(string.Empty);

			var Department = await _uniteOFWork.DepartmentsRepo.GetById(id.Value);

			if (Department is null)
				return NotFound();

			var DepartmentVM = _mapper.Map<Department, DepartmentVM>(Department);
			return View(NameView, DepartmentVM);
		}
		#endregion


		#region Delete
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		public async Task<IActionResult> Delete([FromRoute] int id, DepartmentVM DepartmentVm)
		{
			if (id != DepartmentVm.id)
				return BadRequest(string.Empty);
			if (ModelState.IsValid)
			{
				var _department = _mapper.Map<DepartmentVM, Department>(DepartmentVm);
				_uniteOFWork.DepartmentsRepo.Delete(_department);
				await _uniteOFWork.Complit();
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return View(DepartmentVm);
			}
		}
		#endregion


		#region Update
		[HttpGet]
		public async Task<IActionResult> Update(int? id)

		{
			return await Details(id, "Update");
		}
		[HttpPost]
		public IActionResult Update([FromRoute] int id, DepartmentVM departmentvm)
		{
			if (id != departmentvm.id)
				return NotFound(string.Empty);
			if (ModelState.IsValid)
			{
				try
				{
					var department = _mapper.Map<DepartmentVM, Department>(departmentvm);
					_uniteOFWork.DepartmentsRepo.Update(department);

					var count = _uniteOFWork.Complit().Result;
					if (count >0)
					{
						return RedirectToAction(nameof(Index));
					}
				}
				catch (Exception message)
				{
					ModelState.AddModelError(string.Empty, message.Message);
				}
			}
			return View(departmentvm);

		}

		#endregion



	}
}
