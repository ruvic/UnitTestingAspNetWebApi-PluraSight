
using EmployeeManagement.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagement.Test
{
    public class CheckShowStatisticsHeaderTests
    {

        [Fact]
        public void OnActionExecuting_ShowStatisticsHeaderMissing_MustReturnBadRequestResult()
        {
            //Arrange
            var actionContext = new ActionContext(new DefaultHttpContext(), new(), new(), new());
            var actionExecutingContext = new ActionExecutingContext(
                actionContext, 
                new List<IFilterMetadata>(), 
                new Dictionary<string, object?>(), 
                new());
            var statisticHeaderFilter = new CheckShowStatisticsHeader();

            //Act
            statisticHeaderFilter.OnActionExecuting(actionExecutingContext);

            //Assert
            Assert.IsType<BadRequestResult>(actionExecutingContext.Result);

        }
    }
}
