

using EmployeeManagement.DataAccess.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Test
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void RegisterDataServices_Execute_ServiceMustBeRegistered()
        {
            //Arrange 
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:EmployeeManagementDB", "Any Connection string Value" }
                })
                .Build();

            //Act
            serviceCollection.RegisterDataServices(configuration);

            //Assert
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var employeeManagementRepo = serviceProvider.GetService<IEmployeeManagementRepository>();
            Assert.NotNull(employeeManagementRepo);
            Assert.IsType<EmployeeManagementRepository>(employeeManagementRepo);

        }
    }
}
