using Netwealth.Currency.Interview.Test.UI.Business.Models.Common;
using Netwealth.Currency.Interview.Test.UI.Business.ViewModels;
using System.Threading.Tasks;

namespace Netwealth.Currency.Interview.Test.UI.Business.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<Result<double>> ConvertAmount(ConvertCurrencyViewModel data);
    }
}
