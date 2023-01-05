using FourDotnet.IntegrationTesting.Database;

namespace FourDotnet.IntegrationTesting.Services;

public interface IEmployeeService
{
	public Task<List<Employee>> List();
	public Task Pay();
}