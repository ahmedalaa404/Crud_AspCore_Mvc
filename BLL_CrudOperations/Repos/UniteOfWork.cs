using BLL_CrudOperations.InterFaces;
using Dal_CrudOperations.Database;
using System.Threading.Tasks;

namespace BLL_CrudOperations.Repos
{
	public class UniteOfWork : IUniteOFWork
	{

		private readonly Context _context;
		public IEmployeeRepo EmployeeRepo { get; set; }
		public IDepartmentsRepo DepartmentsRepo { get; set; }

		public UniteOfWork(Context context)
		{
			EmployeeRepo = new EmployeeRepo(context);
			DepartmentsRepo = new DepartmentsRepo(context);
			_context = context;
		}



		public void Dispose() => _context.Dispose();

		public async Task<int> Complit() =>await _context.SaveChangesAsync();

	}
}
