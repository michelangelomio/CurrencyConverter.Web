using Microsoft.AspNetCore.Mvc;
using Netwealth.Currency.Interview.Test.UI.Business.Services.Interfaces;
using Netwealth.Currency.Interview.Test.UI.Business.ViewModels;
using Netwealth.Currency.Interview.Test.UI.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Netwealth.Currency.Interview.Test.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public HomeController(
            ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult Index(ConvertCurrencyViewModel model)
        {
            PopulateViewModel(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConvertAmount(ConvertCurrencyViewModel model)
        {
            var response = await _currencyService.ConvertAmount(model).ConfigureAwait(false);

            if (response.IsSuccess)
            {
                model.SuccessfullyConverted = true;
                model.ConversionResponse = response.Content.ToString("N2");
            }
            else
            {
                model.ConversionResponse = response.ErrorMessage;
            }

            return RedirectToAction("Index", "Home", model);
        }

        private ConvertCurrencyViewModel PopulateViewModel(ConvertCurrencyViewModel model)
        {
            var currenciesSelectList = from Currencies e in Enum.GetValues(typeof(Currencies))
                                       select new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                                       {
                                           Value = e.ToString(),
                                           Text = e.ToString()
                                       };

            model.Currencies = currenciesSelectList;

            return model;
        }
    }
}
