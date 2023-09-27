using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherSampleOne.Domain;
using WeatherSampleOne.Domain.Network;
using WeatherSampleOne.Services.Contract.Common;
using WeatherSampleOne.ViewModels;

namespace WeatherSampleOne.UnitTests
{
    [TestClass]
    public class WeatherSearchViewModelTests
    {
        private WeatherSearchViewModel viewModel;
        private Mock<IWeatherService> weatherServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            weatherServiceMock = new Mock<IWeatherService>();
            viewModel = new WeatherSearchViewModel(weatherServiceMock.Object);
        }

        [TestMethod]
        public void ValidateCity_ValidCity_ReturnsTrue()
        {
            // Arrange
            viewModel.TheWeatherRequest.City = "Lagos";

            // Act
            bool isValid = viewModel.ValidateCity();

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsFalse(viewModel.HasErrors);
        }

        [TestMethod]
        public void ValidateCity_EmptyCity_ReturnsFalseAndAddsError()
        {
            // Arrange
            viewModel.TheWeatherRequest.City = string.Empty;

            // Act
            bool isValid = viewModel.ValidateCity();

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(viewModel.HasErrors);
            Assert.AreEqual("City is required.", viewModel.Errors[nameof(WeatherRequest.City)]);
        }

        [TestMethod]
        public void ValidateCoordinates_ValidCoordinates_ReturnsTrue()
        {
            // Arrange
            viewModel.TheWeatherRequest.Latitude = 40.7128;
            viewModel.TheWeatherRequest.Longitude = -74.0060;

            // Act
            bool isValid = viewModel.ValidateCoordinates();

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsFalse(viewModel.HasErrors);
        }

        [TestMethod]
        public void ValidateCoordinates_MissingLatitude_ReturnsFalseAndAddsError()
        {
            // Arrange
            viewModel.TheWeatherRequest.Longitude = -74.0060;

            // Act
            bool isValid = viewModel.ValidateCoordinates();

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(viewModel.HasErrors);
            Assert.AreEqual("Latitude is required.", viewModel.Errors[nameof(WeatherRequest.City)]);
        }

        [TestMethod]
        public void ValidateCoordinates_MissingLongitude_ReturnsFalseAndAddsError()
        {
            // Arrange
            viewModel.TheWeatherRequest.Latitude = 40.7128;

            // Act
            bool isValid = viewModel.ValidateCoordinates();

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(viewModel.HasErrors);
            Assert.AreEqual("Longitude is required.", viewModel.Errors[nameof(WeatherRequest.City)]);
        }

        // You can write similar tests for other scenarios, such as testing GetWeatherAsync.

        [TestMethod]
        public async Task GetWeatherAsync_ValidRequest_CallsServiceAndSetsResponse()
        {
            // Arrange
            var expectedResponse = new WeatherResponse();
            weatherServiceMock.Setup(s => s.GetWeatherAsync(It.IsAny<WeatherRequest>(), It.IsAny<Action<WeatherResponse>>(), It.IsAny<Action<ApiError>>()))
                .Callback<WeatherRequest, Action<WeatherResponse>, Action<ApiError>>((request, onSuccess, onError) =>
                {
                    // Simulate the service callback
                    onSuccess(expectedResponse);
                });

            // Act
            await viewModel.GetWeatherAsync(response =>
            {
                // Assert
                Assert.AreEqual(expectedResponse, response);
            }, onError: null);

            // Assert
            Assert.AreEqual(expectedResponse, viewModel.TheWeatherResponse);
        }
    }
}
