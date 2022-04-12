using Netwealth.Currency.Interview.Test.UI.Business.Models.Common;
using System.Threading.Tasks;

namespace Netwealth.Currency.Interview.Test.UI.Business.Clients.Interfaces
{
    public interface IHttpClientBase
    {
        Task<Result<T>> Get<T>(string path);

        Task<Result<T>> Post<T>(string path, object data);

        Task<Result<T>> Put<T>(string path, object data);

        Task<Result<T>> Delete<T>(string path);
    }
}
