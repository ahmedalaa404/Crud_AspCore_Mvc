using BLL_CrudOperations.InterFaces;
using Dal_CrudOperations.Database;
using Dal_CrudOperations.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL_CrudOperations.Repos
{
	public class GenericesRepo<t> : IGenericesRepo<t> where t : class
	{
		private readonly Context _context;

		public GenericesRepo(Context ContextDb)
		{
			_context = ContextDb;
		}

		public async Task Create(t Item)
		{
			await _context.Set<t>().AddAsync(Item);
		}

		public void Delete(t Item)
		{
			_context.Set<t>().Remove(Item);

		}

		public async Task<IEnumerable<t>> GetAll()
		{
			if (typeof(t) == typeof(Employee))
			{
				return (IEnumerable<t>)await _context.employees.Include(x => x.department).Select(x => x).ToListAsync();
			}
			return await _context.Set<t>().Select(x => x).ToListAsync();

		}


		public async Task<t> GetById(int id) => await _context.Set<t>().FindAsync(id);



		public async Task<IEnumerable<t>> Search(string value)
		{
			if (typeof(t) == typeof(Employee))
			{
				return await _context.employees.Where(x => x.name.ToLower().Contains(value.ToLower())).Select(x => x).ToListAsync() as IEnumerable<t>;
			}
			else
			{
				return await _context.Departments.Where(x => x.Name.ToLower().Contains(value.ToLower())).Select(x => x).ToListAsync() as IEnumerable<t>;
			}
		}


		public void Update(t Item)
		{
			_context.Set<t>().Update(Item);
			_context.SaveChanges();
		}


	}
}
