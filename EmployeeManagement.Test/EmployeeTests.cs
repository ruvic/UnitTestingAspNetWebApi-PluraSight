using EmployeeManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class EmployeeTests
    {
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_MustBeConcatenation()
        {
            //Arrange
            var employee = new InternalEmployee("Ruvic", "MBOGNI", 6, 2500, false, 2);

            //Act
            employee.FirstName = "Brielle";
            employee.LastName = "TIOTSIA";

            //Assert
            Assert.Equal("Brielle TIOTSIA", employee.FullName);
        }

        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_MustStartWithFirstName()
        {
            //Arrange
            var employee = new InternalEmployee("Ruvic", "MBOGNI", 6, 2500, false, 2);

            //Act
            employee.FirstName = "Brielle";
            employee.LastName = "TIOTSIA";

            //Assert
            Assert.StartsWith("Brielle", employee.FullName);
        }

        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_MustEndWithLastName()
        {
            //Arrange
            var employee = new InternalEmployee("Ruvic", "MBOGNI", 6, 2500, false, 2);

            //Act
            employee.FirstName = "Brielle";
            employee.LastName = "TIOTSIA";

            //Assert
            Assert.EndsWith("TIOTSIA", employee.FullName);
        }

        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_ContainsPartOfConcatenation()
        {
            //Arrange
            var employee = new InternalEmployee("Ruvic", "MBOGNI", 6, 2500, false, 2);

            //Act
            employee.FirstName = "Brielle";
            employee.LastName = "TIOTSIA";

            //Assert
            Assert.Contains("le TI", employee.FullName);
        }


    }
}
