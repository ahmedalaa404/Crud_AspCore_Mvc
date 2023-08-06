using AutoMapper;
using CrudOperations.Models;
using Dal_CrudOperations.DomainModel;

namespace CrudOperations.Profiler
{
	public class MappingProfilers : Profile
	{

		public MappingProfilers()
		{
			CreateMap<Employee, EmployeeVm>().ReverseMap();
			CreateMap<Department, DepartmentVM>().ReverseMap();

			CreateMap<ApplicationsUser,UserVM>().ReverseMap();

		}

	}
}
