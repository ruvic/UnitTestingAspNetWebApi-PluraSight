using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using Xunit.Abstractions;

namespace EmployeeManagement.Test
{
    [Collection("EmployeeServiceCollection")]
    public class EmployeeServiceTests //: IClassFixture<EmployeeServiceFixture>
    {

        private readonly EmployeeServiceFixture _fixture;
        private readonly ITestOutputHelper _testOutHelper;

        public EmployeeServiceTests(EmployeeServiceFixture fixture, ITestOutputHelper testOutputHelper) 
        { 
            _fixture = fixture; 
            _testOutHelper = testOutputHelper;
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

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveObligatoryAsFirstCourses_WithPredicate()
        {
            //Act
            var internalEmployee = _fixture.EmployeeService.CreateInternalEmployee("Ruvic", "MBOGNI");

            _testOutHelper.WriteLine($"Employee after act : {internalEmployee.FirstName} {internalEmployee.LastName}");
            internalEmployee.AttendedCourses.ForEach(course => _testOutHelper.WriteLine($"Attended course: {course.Id} {course.Title}"));

            //Assert
            Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchesObligatories_RM()
        {
            //Arrange
            var obligatoryCourses = _fixture.EmployeeManagementTestDataRepo
                .GetCourses(
                    Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"), 
                    Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //Act
            var internalEmployee = _fixture.EmployeeService.CreateInternalEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.True(obligatoryCourses.All(x => internalEmployee.AttendedCourses.Exists(y => y.Id == x.Id)));

        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchesObligatories()
        {
            //Arrange
            var obligatoryCourses = _fixture.EmployeeManagementTestDataRepo
                .GetCourses(
                    Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                    Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));


            //Act
            var internalEmployee = _fixture.EmployeeService.CreateInternalEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustBeNew()
        {
            //Act
            var internalEmployee = _fixture.EmployeeService.CreateInternalEmployee("Ruvic", "MBOGNI");
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
            var internalEmployee = _fixture.EmployeeService.CreateInternalEmployee("Ruvic", "MBOGNI");
            //internalEmployee.AttendedCourses[1].IsNew = true;

            //Assert
            Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew));

        }

        [Fact]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustBeNew_Async()
        {
            //Act
            var internalEmployee = await _fixture.EmployeeService.CreateInternalEmployeeAsync("Ruvic", "MBOGNI");

            //Assert
            Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew));

        }

        [Fact]
        public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
        {
            //Arrange
            var internalEmployee = new InternalEmployee("Ruvic", "MBOGNI", 5, 3000, false, 1);

            //Act & Assert
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await _fixture.EmployeeService.GiveRaiseAsync(internalEmployee, 50));

        }



    }
}
