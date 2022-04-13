using Microsoft.Extensions.Configuration;
using Netwealth.Currency.Interview.Test.UI.Business.Clients.Interfaces;
using Netwealth.Currency.Interview.Test.UI.Business.Models.Common;
using Netwealth.Currency.Interview.Test.UI.Business.Services.Interfaces;
using Netwealth.Currency.Interview.Test.UI.Business.ViewModels;
using System.Threading.Tasks;

namespace Netwealth.Currency.Interview.Test.UI.Business.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IHttpClientBase _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string ConvertApiUrl;

        public CurrencyService(
            IHttpClientBase httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var webSection = _configuration.GetSection("Web");
            ConvertApiUrl = webSection["ConvertApiUrl"];
        }

        public async Task<Result<double>> ConvertAmount(ConvertCurrencyViewModel data)
        {
            var path = string.Join("/", ConvertApiUrl, data.FromCurrency, data.ToCurrency, data.Amount);
            return await _httpClient.Get<double>(path);
        }
    }
}
