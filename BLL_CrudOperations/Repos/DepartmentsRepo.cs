using BLL_CrudOperations.InterFaces;
using Dal_CrudOperations.Database;
using Dal_CrudOperations.DomainModel;

namespace BLL_CrudOperations.Repos
{
	public class DepartmentsRepo : GenericesRepo<Department>, IDepartmentsRepo
	{

		public DepartmentsRepo(Context DbConnection) : base(DbConnection)
		{

		}
	}
}
