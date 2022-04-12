using Microsoft.Extensions.Configuration;
using Moq;
using Netwealth.Currency.Interview.Test.UI.Business.Clients.Interfaces;
using Netwealth.Currency.Interview.Test.UI.Business.Models.Common;
using Netwealth.Currency.Interview.Test.UI.Business.Services;
using Netwealth.Currency.Interview.Test.UI.Business.ViewModels;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Netwealth.Currency.Interview.Test.UI.Tests
{
    public class CurrencyServiceTests
    {
        [Test]
        public async Task ConvertAmount_WhenSuccess_ReturnConvertedValueAsContent()
        {
            //Arrange
            var mockClient = new Mock<IHttpClientBase>();
            var mockConfig = new Mock<IConfiguration>();
            var mockSection = new Mock<IConfigurationSection>();
            var response = new Result<double>
            {
                IsSuccess = true,
                Content = 1.1
            };

            mockClient
                .Setup(x => x.Post<double>(It.IsAny<string>(), It.IsAny<ConvertCurrencyViewModel>()))
                .ReturnsAsync(response);

            mockSection
                .Setup(x => x.Value)
                .Returns("sectionValue");

            mockConfig
                .Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(mockSection.Object);

            var functionUnderTest = new CurrencyService(mockClient.Object, mockConfig.Object);

            //Act
            var result = await functionUnderTest.ConvertAmount(new ConvertCurrencyViewModel());

            //Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Content, 1.1);
        }

        [Test]
        public async Task ConvertAmount_WhenFailed_ReturnErrorMessage()
        {
            //Arrange
            var mockClient = new Mock<IHttpClientBase>();
            var mockConfig = new Mock<IConfiguration>();
            var mockSection = new Mock<IConfigurationSection>();
            var response = new Result<double>
            {
                IsSuccess = false,
                ErrorMessage = "Some error message"
            };

            mockClient
                .Setup(x => x.Post<double>(It.IsAny<string>(), It.IsAny<ConvertCurrencyViewModel>()))
                .ReturnsAsync(response);

            mockSection
                .Setup(x => x.Value)
                .Returns("sectionValue");

            mockConfig
                .Setup(x => x.GetSection(It.IsAny<string>()))
                .Returns(mockSection.Object);

            var functionUnderTest = new CurrencyService(mockClient.Object, mockConfig.Object);

            //Act
            var result = await functionUnderTest.ConvertAmount(new ConvertCurrencyViewModel());

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(result.ErrorMessage, "Some error message");
        }
    }
}
