namespace CRUD_Operation.Models
{
	public class Employee
	{
		public Guid Id { get; set; }

		public string? Name { get; set; }

		public  string? Address{ get; set; }

		public long Mobile {  get; set; }

		public DateTime DateOfBirth { get; set; }
	}
}
