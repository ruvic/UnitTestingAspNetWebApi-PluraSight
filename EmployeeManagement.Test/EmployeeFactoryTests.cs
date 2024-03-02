

using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTests : IDisposable
    {
        private EmployeeFactory _employeeFactory;

        public EmployeeFactoryTests()
        {
            _employeeFactory = new EmployeeFactory();
        }

        public void Dispose()
        {
            //Setup code for cleanup here if necessary
        }


        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
            //Act
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.Equal(2500, employee.Salary);
        }

        [Fact(Skip = "Skipping this one for demos reasons")]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryBetween2500And3500()
        {
            //Act
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryBetween2500And3500_AlternativeWay()
        {
            //Act
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.True(employee.Salary >= 2500);
            Assert.True(employee.Salary <= 3500);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryBetween2500And3500_InRange()
        {
            //Arrange 
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.InRange(employee.Salary, 2500, 3500);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PrecisionExample()
        {
            //Act
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Ruvic", "MBOGNI");
            employee.Salary = 2500.123m;

            //Assert
            Assert.Equal(2500, employee.Salary, 0);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_ReturnType")]
        public void CreateEmployee_IsExternalTrue_ReturnMustBeExternalEmployee()
        {
            //Act
            var employee = _employeeFactory.CreateEmployee("Ruvic", "MBOGNI", "Eneo", true);

            //Assert
            Assert.IsType<ExternalEmployee>(employee);
            //Assert.IsAssignableFrom<Employee>(employee);
        }

       
    }
}
