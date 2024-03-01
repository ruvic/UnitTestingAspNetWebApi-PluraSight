using EmployeeManagement.Business;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveObligatoryAsFirstCourses_WithObject()
        {
            //Arrange
            var employeeManagementTestDataRepo = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepo, new EmployeeFactory());
            var obligatoryCourse = employeeManagementTestDataRepo
                .GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            //Act
            var internalEmployee = employeeService.CreateInternalEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveObligatoryAsFirstCourses_WithPredicate()
        {
            //Arrange
            var employeeManagementTestDataRepo = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepo, new EmployeeFactory());

            //Act
            var internalEmployee = employeeService.CreateInternalEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchesObligatories_RM()
        {
            //Arrange
            var employeeManagementTestDataRepo = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepo, new EmployeeFactory());
            var obligatoryCourses = employeeManagementTestDataRepo
                .GetCourses(
                    Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"), 
                    Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //Act
            var internalEmployee = employeeService.CreateInternalEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.True(obligatoryCourses.All(x => internalEmployee.AttendedCourses.Exists(y => y.Id == x.Id)));

        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchesObligatories()
        {
            //Arrange
            var employeeManagementTestDataRepo = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepo, new EmployeeFactory());
            var obligatoryCourses = employeeManagementTestDataRepo
                .GetCourses(
                    Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                    Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //Act
            var internalEmployee = employeeService.CreateInternalEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustBeNew()
        {
            //Arrange
            var employeeManagementTestDataRepo = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepo, new EmployeeFactory());

            //Act
            var internalEmployee = employeeService.CreateInternalEmployee("Ruvic", "MBOGNI");
            //internalEmployee.AttendedCourses[0].IsNew = true;

            //Assert
            foreach (var course in internalEmployee.AttendedCourses)
            {
                Assert.False(course.IsNew, $"Aucun nouveau cours n'est accepté (Title = {course.Title}, Id = {course.Id})");
            }

        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustBeNew2()
        {
            //Arrange
            var employeeManagementTestDataRepo = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepo, new EmployeeFactory());

            //Act
            var internalEmployee = employeeService.CreateInternalEmployee("Ruvic", "MBOGNI");
            //internalEmployee.AttendedCourses[1].IsNew = true;

            //Assert
            Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew));

        }

        [Fact]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustBeNew_Async()
        {
            //Arrange
            var employeeManagementTestDataRepo = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepo, new EmployeeFactory());

            //Act
            var internalEmployee = await employeeService.CreateInternalEmployeeAsync("Ruvic", "MBOGNI");

            //Assert
            Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew));

        }

        [Fact]
        public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
        {
            //Arrange
            var employeeService = new EmployeeService(new EmployeeManagementTestDataRepository(), new EmployeeFactory());
            var internalEmployee = new InternalEmployee("Ruvic", "MBOGNI", 5, 3000, false, 1);

            //Act & Assert
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await employeeService.GiveRaiseAsync(internalEmployee, 50));

        }






    }
}
