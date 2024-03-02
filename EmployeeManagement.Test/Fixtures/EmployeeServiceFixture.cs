using EmployeeManagement.Business;
using EmployeeManagement.Services.Test;

namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeeServiceFixture : IDisposable
    {
        public EmployeeManagementTestDataRepository EmployeeManagementTestDataRepo { get; }
        public EmployeeService EmployeeService { get; }

        public EmployeeServiceFixture() 
        {
            EmployeeManagementTestDataRepo = new EmployeeManagementTestDataRepository();
            EmployeeService = new EmployeeService(EmployeeManagementTestDataRepo, new EmployeeFactory());
        }

        public void Dispose()
        {
            //cleanup code here if required
        }

    }
}
