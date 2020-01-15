using HealthOnlone.DevTest.CurrencyViewer.Controllers.Api;
using HealthOnlone.DevTest.CurrencyViewer.Models;
using HealthOnlone.DevTest.CurrencyViewer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class CurrenyDataSourceApiControlllerTestFixture
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Can_get_list_of_data_providers_from_api_controller ()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CurrencyDataSourceApiController>>();
        
            var controllerUnderTest = new CurrencyDataSourceApiController(
                new Repository(),
                loggerMock.Object
                );

            // Act
            var datasourcesResult = controllerUnderTest.GetDataSources() as OkObjectResult;
            var modelReturned = datasourcesResult.Value as IList<CurrencyDataSourceModel>;

            // Assert
            Assert.That(modelReturned, Is.Not.Null);
            Assert.That(modelReturned.Count, Is.EqualTo(2));
        }
    }
}