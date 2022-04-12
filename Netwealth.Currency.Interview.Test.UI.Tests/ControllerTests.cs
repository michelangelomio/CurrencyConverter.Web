using Microsoft.AspNetCore.Mvc;
using Moq;
using Netwealth.Currency.Interview.Test.UI.Business.Models.Common;
using Netwealth.Currency.Interview.Test.UI.Business.Services.Interfaces;
using Netwealth.Currency.Interview.Test.UI.Business.ViewModels;
using Netwealth.Currency.Interview.Test.UI.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Netwealth.Currency.Interview.Test.UI.Tests
{
    public class ControllerTests
    {
        [Test]
        public async Task ConvertAmount_WhenCalled_ReturnConvertedValueAndRedirectToIndex()
        {
            //Arrange
            var mockService = new Mock<ICurrencyService>();

            mockService
                .Setup(x => x.ConvertAmount(It.IsAny<ConvertCurrencyViewModel>()))
                .ReturnsAsync(new Result<double>());

            var functionUnderTest = new HomeController(mockService.Object);

            //Act
            var result = (RedirectToActionResult)await functionUnderTest.ConvertAmount(new ConvertCurrencyViewModel());

            //Assert
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }
    }
}
