

using EmployeeManagement.Business;
using System.Text;
using System.Text.Json;

namespace EmployeeManagement.Test.HttpMessageHandlers
{
    public class TestablePromotionEligibilityHandler : HttpMessageHandler
    {
        private bool _isEligible;

        public TestablePromotionEligibilityHandler(bool isEligible)
        {
            _isEligible = isEligible;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseData = new PromotionEligibility
            {
                EligibleForPromotion = _isEligible,
            };

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(responseData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    })
                    , Encoding.ASCII
                    , "application/json"
                )
            };

            return Task.FromResult(response);
        }

    }
}
