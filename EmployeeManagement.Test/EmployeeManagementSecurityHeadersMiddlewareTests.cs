
using EmployeeManagement.Middleware;
using Microsoft.AspNetCore.Http;
using Moq;

namespace EmployeeManagement.Test
{
    public class EmployeeManagementSecurityHeadersMiddlewareTests
    {
        [Fact]
        public async void InvokeAsync_Invoke_SetsExpectedResponseHeaders()
        {
            //Arrange 
            HttpContext httpContext = new DefaultHttpContext();
            RequestDelegate next = (HttpContext context) => Task.CompletedTask;

            //var requestDelegateMock = new Mock<RequestDelegate>();
            //requestDelegateMock
            //    .Setup(x => x(It.IsAny<HttpContext>()))
            //    .Returns(Task.CompletedTask);
            //var middleware = new EmployeeManagementSecurityHeadersMiddleware(requestDelegateMock.Object);

            var middleware = new EmployeeManagementSecurityHeadersMiddleware(next);

            //Act 
            await middleware.InvokeAsync(httpContext);

            //Assert
            var cspHeaders = httpContext.Response.Headers["Content-Security-Policy"];
            var xContentTypeOptionHeaders = httpContext.Response.Headers["X-Content-Type-Options"];
            Assert.Equal("default-src 'self';frame-ancestors 'none';", cspHeaders);
            Assert.Equal("nosniff", xContentTypeOptionHeaders);

        }
    }
}
