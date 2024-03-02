using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using EmployeeManagement.Test.TestData;

namespace EmployeeManagement.Test
{
    [Collection("EmployeeServiceCollection")]
    public class DataDrivenEmployeeServiceTests //: IClassFixture<EmployeeServiceFixture>
    {
        private readonly EmployeeServiceFixture _fixture;

        public DataDrivenEmployeeServiceTests(EmployeeServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("37e03ca7-c730-4351-834c-b66f280cdb01")]
        [InlineData("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e")]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveObligatoryAsFirstCourses(Guid courseId)
        {
            //Act
            var internalEmployee = _fixture.EmployeeService.CreateInternalEmployee("Ruvic", "MBOGNI");

            //Assert
            Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == courseId);
        }

        [Fact]
        public async Task GiveRaise_MinimumRaiseGiven_EmployeeMinimumRaiseGivenMustBeTrue()
        {
            //Arrange
            var internalEmployee = new InternalEmployee("Ruvic", "MBOGNI", 5, 3000, false, 1);

            //Act
            await _fixture.EmployeeService.GiveRaiseAsync(internalEmployee, 100);

            //Assert
            Assert.True(internalEmployee.MinimumRaiseGiven);
        }

        [Fact]
        public async Task GiveRaise_MoreThanMinimumRaiseGiven_EmployeeMinimumRaiseGivenMustBeFalse()
        {
            //Arrange
            var internalEmployee = new InternalEmployee("Ruvic", "MBOGNI", 5, 3000, false, 1);

            //Act
            await _fixture.EmployeeService.GiveRaiseAsync(internalEmployee, 200);

            //Assert
            Assert.False(internalEmployee.MinimumRaiseGiven);
        }

        public static IEnumerable<object[]> ExampleTestDataForGiveRaise_WithProperty
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { 100, true },
                    new object[] { 200, false },
                };
            }
        }

        public static IEnumerable<object[]> ExampleTestDataForGiveRaise_WithMethod(int nbInstancesProvide)
        {
            var testData =  new List<object[]>
            {
                new object[] { 100, true },
                new object[] { 300, false },
            };
            return testData.Take(nbInstancesProvide);
        }

        public static TheoryData<int, bool> StronglyTypedExampleTestDataForGiveRaise
        {
            get
            {
                return new TheoryData<int, bool>
                {
                    { 100, true },
                    { 600, false },
                };
            }
        }

        [Theory]
        //[MemberData(nameof(ExampleTestDataForGiveRaise_WithMethod), 1)]
        //[ClassData(typeof(EmployeeServiceTestData))]
        //[ClassData(typeof(StronglyTypedEmployeeServiceTestData))]
        //[MemberData(nameof(StronglyTypedExampleTestDataForGiveRaise))]
        [ClassData(typeof(StronglyTypedEmployeeServiceTestDataFromFile))]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMachesValue(int raiseGiven, bool expectedValueMinimumRaiseGiven)
        {
            //Arrange
            var internalEmployee = new InternalEmployee("Ruvic", "MBOGNI", 5, 3000, false, 1);

            //Act
            await _fixture.EmployeeService.GiveRaiseAsync(internalEmployee, raiseGiven);

            //Assert
            Assert.Equal(expectedValueMinimumRaiseGiven, internalEmployee.MinimumRaiseGiven);
        }


    }
}
