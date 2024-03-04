

using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Services.Test;
using Moq;

namespace EmployeeManagement.Test
{
    public class MoqTests
    {
        [Fact]
        public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated()
        {
            //Arrange
            var employeeManagementRepo = new EmployeeManagementTestDataRepository();
            //var employeeFactory = new EmployeeFactory();
            var employeeFactoryMock = new Mock<EmployeeFactory>();
            var employeeService = new EmployeeService(employeeManagementRepo, employeeFactoryMock.Object);

            //Act
            var internalEmployee = employeeService.FetchInternalEmployee(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));
            
            //Assert
            Assert.Equal(400, internalEmployee!.SuggestedBonus);
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_SuggestedBonusMustBeCalculated()
        {
            //Arrange
            var employeeManagementRepo = new EmployeeManagementTestDataRepository();
            var employeeFactoryMock = new Mock<EmployeeFactory>();
            employeeFactoryMock
                .Setup(x => x.CreateEmployee(
                    "Ruvic",
                    It.IsAny<string>(),
                    null,
                    false
                ))
                .Returns(new InternalEmployee("Ruvic", "MBOGNI", 5, 2500, false, 1));
            employeeFactoryMock
                .Setup(x => x.CreateEmployee(
                    "Brielle",
                    It.IsAny<string>(),
                    null,
                    false
                ))
                .Returns(new InternalEmployee("Brielle", "TIOTSIA", 5, 2500, false, 1));
            employeeFactoryMock
                .Setup(x => x.CreateEmployee(
                    It.Is<string>(value => value.Contains("e")),
                    It.IsAny<string>(),
                    null,
                    false
                ))
                .Returns(new InternalEmployee("SomethingWithE", "MBOGNI", 5, 2500, false, 1));

            var employeeService = new EmployeeService(employeeManagementRepo, employeeFactoryMock.Object);

            //suggested bonus for new employees = 
            // (years in service if > 0) * attended courses * 100
            decimal suggestedBonus = 1000;

            //Act
            var internalEmployee = employeeService.CreateInternalEmployee("Brielle", "MBOGNI");

            //Assert
            Assert.Equal(suggestedBonus, internalEmployee.SuggestedBonus);
        }

        [Fact]
        public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated_MoqInterface()
        {
            //Arrange
            var employeeManagementRepo = new Mock<IEmployeeManagementRepository>();
            employeeManagementRepo
                .Setup(x => x.GetInternalEmployee(It.IsAny<Guid>()))
                .Returns(new InternalEmployee("Ruvic", "MBOGNI", 2, 2500, false, 1)
                {
                    AttendedCourses = new List<Course>
                    {
                        new Course("A course"),
                        new Course("Another Course")
                    }
                });

            var employeeFactoryMock = new Mock<EmployeeFactory>();
            var employeeService = new EmployeeService(employeeManagementRepo.Object, employeeFactoryMock.Object);

            //Act
            var internalEmployee = employeeService.FetchInternalEmployee(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            //Assert
            Assert.Equal(400, internalEmployee!.SuggestedBonus);
        }

        [Fact]
        public async Task FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated_MoqInterface_Async()
        {
            //Arrange
            var employeeManagementRepo = new Mock<IEmployeeManagementRepository>();
            employeeManagementRepo
                .Setup(x => x.GetInternalEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new InternalEmployee("Ruvic", "MBOGNI", 2, 2500, false, 1)
                {
                    AttendedCourses = new List<Course>
                    {
                        new Course("A course"),
                        new Course("Another Course")
                    }
                });

            var employeeFactoryMock = new Mock<EmployeeFactory>();
            var employeeService = new EmployeeService(employeeManagementRepo.Object, employeeFactoryMock.Object);

            //Act
            var internalEmployee = await employeeService.FetchInternalEmployeeAsync(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            //Assert
            Assert.Equal(400, internalEmployee!.SuggestedBonus);
        }


    }
}
