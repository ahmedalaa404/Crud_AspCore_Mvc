using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL_CrudOperations.InterFaces
{
	public interface IGenericesRepo<T>
	{

		Task<IEnumerable<T>> GetAll();
		Task<T> GetById(int id);

		void Delete(T Item);
		Task Create(T Item);
		void Update(T Item);

		Task<IEnumerable<T>> Search(string value);

	}
}
