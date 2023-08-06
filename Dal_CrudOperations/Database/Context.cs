using Dal_CrudOperations.DomainModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Dal_CrudOperations.Database
{
	public class Context : IdentityDbContext<ApplicationsUser>
	{

		#region OLd Db context to Oppen Auth
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//    optionsBuilder.UseSqlServer(connection => { })
		//}
		#endregion
		public Context(DbContextOptions<Context> Options) : base(Options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Employee>().HasOne(X => X.department).WithMany(x => x.Employees).HasForeignKey(X => X.Departmentid);

		}

		public DbSet<Employee> employees { get; set; }
		public DbSet<Department> Departments { get; set; }

		//      public DbSet<IdentityUser> Users { get; set; }
		//public DbSet<IdentityRole> Roles { get; set; }

	}
}
