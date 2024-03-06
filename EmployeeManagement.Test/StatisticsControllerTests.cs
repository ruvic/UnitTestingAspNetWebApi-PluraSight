
using AutoMapper;
using EmployeeManagement.Controllers;
using EmployeeManagement.MapperProfiles;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace EmployeeManagement.Test
{
    public class StatisticsControllerTests
    {
        [Fact]
        public void GetStatistics_InputFromHttpConnectionFeature_MustReturnInputtedIps()
        {
            //Arrange
            var localIpAdress = IPAddress.Parse("127.0.0.1");
            var localPort = 5000;
            var remoteIpAdress = IPAddress.Parse("127.0.0.2");
            var remotePort = 2400;

            var featureCollectionMock = new Mock<IFeatureCollection>();
            featureCollectionMock
                .Setup(x => x.Get<IHttpConnectionFeature>())
                .Returns(new HttpConnectionFeature
                {
                    LocalIpAddress= localIpAdress,
                    LocalPort=localPort,
                    RemoteIpAddress= remoteIpAdress,
                    RemotePort=remotePort
                });

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock
                .Setup(x => x.Features)
                .Returns(featureCollectionMock.Object);

            //var httpContext = new DefaultHttpContext(featureCollectionMock.Object);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<StatisticsProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var statisticsController = new StatisticsController(mapper);
            statisticsController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            //Act
            var result = statisticsController.GetStatistics();

            //Assert
            var actionResult = Assert.IsType<ActionResult<StatisticsDto>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var statisticDto = Assert.IsType<StatisticsDto>(okObjectResult.Value);
            Assert.Equal(localIpAdress.ToString(), statisticDto.LocalIpAddress);
            Assert.Equal(localPort, statisticDto.LocalPort);
            Assert.Equal(remoteIpAdress.ToString(), statisticDto.RemoteIpAddress);
            Assert.Equal(remotePort, statisticDto.RemotePort);

        }
    }
}
