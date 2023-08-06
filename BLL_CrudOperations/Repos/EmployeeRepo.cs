using BLL_CrudOperations.InterFaces;
using Dal_CrudOperations.Database;
using Dal_CrudOperations.DomainModel;

namespace BLL_CrudOperations.Repos
{
	public class EmployeeRepo : GenericesRepo<Employee>, IEmployeeRepo
	{

		public EmployeeRepo(Context DbConnection) : base(DbConnection)
		{

		}


	}
}
