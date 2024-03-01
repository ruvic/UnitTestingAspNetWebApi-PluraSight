

using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTests
    {
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
            //Arrange 
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.Equal(2500, employee.Salary);
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryBetween2500And3500()
        {
            //Arrange 
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500);
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryBetween2500And3500_AlternativeWay()
        {
            //Arrange 
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.True(employee.Salary >= 2500);
            Assert.True(employee.Salary <= 3500);
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryBetween2500And3500_InRange()
        {
            //Arrange 
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.InRange(employee.Salary, 2500, 3500);
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PrecisionExample()
        {
            //Arrange 
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Ruvic", "MBOGNI");
            employee.Salary = 2500.123m;

            //Assert
            Assert.Equal(2500, employee.Salary, 0);
        }


    }
}
