using System;
using System.Threading.Tasks;

namespace BLL_CrudOperations.InterFaces
{
	public interface IUniteOFWork : IDisposable
	{
		public IEmployeeRepo EmployeeRepo { get; set; }
		public IDepartmentsRepo DepartmentsRepo { get; set; }

		public Task<int> Complit();
	}
}
