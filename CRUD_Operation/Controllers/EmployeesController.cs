using CRUD_Operation.Data;
using CRUD_Operation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CRUD_Operation.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly MVCDemoDBContext _mVcDemoDBContext;

		public EmployeesController(MVCDemoDBContext mVCDemoDBContexts)
		{
			this._mVcDemoDBContext = mVCDemoDBContexts;
		}
		[HttpGet]

		public async Task<IActionResult> Index()
		{
			var employees = await _mVcDemoDBContext.Employees.ToListAsync();
			return View(employees);

		}

		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Add(AddEmployeeViewModel addEmpoyeerequest)
		{
			var employee = new Employee()
			{
				Id = Guid.NewGuid(),
				Name = addEmpoyeerequest.Name,
				Address = addEmpoyeerequest.Address,
				Mobile = addEmpoyeerequest.Mobile,
				DateOfBirth = addEmpoyeerequest.DateOfBirth
			};
			await _mVcDemoDBContext.Employees.AddAsync(employee);
			await _mVcDemoDBContext.SaveChangesAsync();


			return RedirectToAction("Index");
		}

		public async Task<IActionResult> View(Guid Id)
		{
			var empoyeeData = await _mVcDemoDBContext.Employees.FirstOrDefaultAsync(a => a.Id == Id);
			if (empoyeeData != null)
			{
				var viewModel = new UpdateEmployeeViewModel()
				{
					Id = empoyeeData.Id,
					Name = empoyeeData.Name,
					Address = empoyeeData.Address,
					Mobile = empoyeeData.Mobile,
					DateOfBirth = empoyeeData.DateOfBirth
				};
				return await Task.Run(() => View("View", viewModel));
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> View(UpdateEmployeeViewModel updateEmployeeViewModel)
		{
			if (ModelState.IsValid)
			{
				var employeeData = await _mVcDemoDBContext.Employees.FindAsync(updateEmployeeViewModel.Id);
				if (employeeData != null)
				{
					employeeData.Name = updateEmployeeViewModel.Name;
					employeeData.Address = updateEmployeeViewModel.Address;
					employeeData.Mobile = updateEmployeeViewModel.Mobile;
					employeeData.DateOfBirth = updateEmployeeViewModel.DateOfBirth;
					await _mVcDemoDBContext.SaveChangesAsync();
					return RedirectToAction("Index");
				}
			}
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<IActionResult> Delete(UpdateEmployeeViewModel deleteEmployeeViewModel)
		{
			var employeeData = await _mVcDemoDBContext.Employees.FindAsync(deleteEmployeeViewModel.Id);
			if (employeeData != null)
			{
				_mVcDemoDBContext.Employees.Remove(employeeData);
				await _mVcDemoDBContext.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}
	}
}






