using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Services.Test;
using Microsoft.Extensions.DependencyInjection;


namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeeServiceWithAspNetCoreDIFixture : IDisposable
    {

        private readonly ServiceProvider _serviceProvider;

        public IEmployeeManagementRepository EmployeeManagementTestDataRepo 
        {
            get
            {
                return _serviceProvider.GetService<IEmployeeManagementRepository>()!;
            }
        }

        public IEmployeeService EmployeeService 
        {
            get
            {
                return _serviceProvider.GetService<IEmployeeService>()!;
            }
        }

        public EmployeeServiceWithAspNetCoreDIFixture()
        {
            var services = new ServiceCollection();
            services.AddScoped<EmployeeFactory>();
            services.AddScoped<IEmployeeManagementRepository, EmployeeManagementTestDataRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            
            //build service provider
            _serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            //cleanup code here if required
        }
    }
}
