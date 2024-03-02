using EmployeeManagement.Test.Fixtures;

namespace EmployeeManagement.Test
{
    public class EmployeeServiceTestsWithAspNetCoreDI : IClassFixture<EmployeeServiceWithAspNetCoreDIFixture>
    {
        private readonly EmployeeServiceWithAspNetCoreDIFixture _fixture;

        public EmployeeServiceTestsWithAspNetCoreDI(EmployeeServiceWithAspNetCoreDIFixture fixture) 
        { 
            _fixture = fixture;
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveObligatoryAsFirstCourses_WithObject()
        {
            //Arrange
            var obligatoryCourse = _fixture.EmployeeManagementTestDataRepo
                .GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            //Act
            var internalEmployee = _fixture.EmployeeService.CreateInternalEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
        }

    }
}
