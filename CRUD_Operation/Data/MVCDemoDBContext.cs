using CRUD_Operation.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operation.Data
{
	public class MVCDemoDBContext: DbContext
	{
		public MVCDemoDBContext(DbContextOptions options): base(options)
		{

		}

		public DbSet<Employee> Employees { get; set; }
	}
}
