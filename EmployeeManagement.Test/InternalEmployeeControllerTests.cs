using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.MapperProfiles;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Test
{
    public class InternalEmployeeControllerTests
    {
        private InternalEmployee _firstEmployee;
        private InternalEmployeesController _employeeController;

        public InternalEmployeeControllerTests()
        {
            _firstEmployee = new InternalEmployee("Ruvic", "MBOGNI", 2, 3000, false, 1)
            {
                Id = Guid.Parse("22e326cb-30ce-40f2-894b-99957a56050b"),
                SuggestedBonus= 600
            };

            var employeeServiceMock= new Mock<IEmployeeService>();
            employeeServiceMock
               .Setup(x => x.FetchInternalEmployeesAsync())
               .ReturnsAsync(new List<InternalEmployee>
               {
                    _firstEmployee,
                    new InternalEmployee("Jocelin", "FOWE", 2, 3500, false, 1),
                    new InternalEmployee("Paterne", "KOUAYEP", 3, 2500, false, 1),
               });

            //var mapperMock = new Mock<IMapper>();
            //mapperMock
            //    .Setup(x => x.Map<InternalEmployeeDto>(It.IsAny<InternalEmployee>()))
            //    .Returns(new InternalEmployeeDto());
            
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile> { new EmployeeProfile() }));
            var mapper = new Mapper(mapperConfiguration);

            _employeeController = new InternalEmployeesController(employeeServiceMock.Object, mapper);
        }


        [Fact]
        public async void GetInternalEmployees_GetAction_MustReturnObjectResult()
        {
            //Act
            var result = await _employeeController.GetInternalEmployees();

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetInternalEmployees_GetAction_MustReturnIEnumerableOfInternalEmployeeDtosAsModelType()
        {
            //Act
            var result = await _employeeController.GetInternalEmployees();

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);
            Assert.IsAssignableFrom<IEnumerable<InternalEmployeeDto>>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public async void GetInternalEmployees_GetAction_MustReturnNumberInternalEmployeeInputted()
        {
            //Act
            var result = await _employeeController.GetInternalEmployees();

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);
            var listOfEmployees = ((OkObjectResult)actionResult.Result).Value;
            Assert.Equal(3, ((IEnumerable<InternalEmployeeDto>)listOfEmployees).Count());
        }

        [Fact]
        public async void GetInternalEmployees_GetAction_ReturnsOkObjectWithCorrectNumberOfInternalÊmployees()
        {
            //Act
            var result = await _employeeController.GetInternalEmployees();

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var listOfEmployeDtos = Assert.IsAssignableFrom<IEnumerable<InternalEmployeeDto>>(okObjectResult.Value); 
            Assert.Equal(3, listOfEmployeDtos.Count());
            
            var dto = listOfEmployeDtos.First();
            Assert.Equal(_firstEmployee.Id, dto.Id);
            Assert.Equal(_firstEmployee.FirstName, dto.FirstName);
            Assert.Equal(_firstEmployee.LastName, dto.LastName);
            Assert.Equal(_firstEmployee.Salary, dto.Salary);
            Assert.Equal(_firstEmployee.SuggestedBonus, dto.SuggestedBonus);
            Assert.Equal(_firstEmployee.YearsInService, dto.YearsInService);
        }


    }
}
