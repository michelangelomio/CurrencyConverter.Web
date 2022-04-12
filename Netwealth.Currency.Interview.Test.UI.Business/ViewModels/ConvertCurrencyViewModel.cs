using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Netwealth.Currency.Interview.Test.UI.Business.ViewModels
{
    public class ConvertCurrencyViewModel
    {
        public IEnumerable<SelectListItem> Currencies { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public double Amount { get; set; }
        public string ConversionResponse { get; set; }
        public bool SuccessfullyConverted { get; set; }

    }
}
