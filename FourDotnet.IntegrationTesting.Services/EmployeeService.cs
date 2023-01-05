using FourDotnet.IntegrationTesting.Database;
using Microsoft.EntityFrameworkCore;

namespace FourDotnet.IntegrationTesting.Services;

public class EmployeeService : IEmployeeService
{
	private readonly ExampleContext _db;
	
	public EmployeeService(ExampleContext databaseContext)
	{
		_db = databaseContext;
	}

	public async Task<List<Employee>> List()
	{
		return await _db.Employees.ToListAsync();
	}

	public async Task Pay()
	{
		var employees = await List();
		foreach (var employee in employees)
		{
			employee.BankBalance += employee.Salary;
		}

		await _db.SaveChangesAsync();
	}
}