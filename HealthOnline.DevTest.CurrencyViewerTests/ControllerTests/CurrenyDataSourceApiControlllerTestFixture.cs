using HealthOnline.DevTest.CurrencyViewer.Controllers.Api;
using HealthOnline.DevTest.CurrencyViewer.Models;
using HealthOnline.DevTest.CurrencyViewer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class CurrenyDataSourceApiControlllerTestFixture
    {
        private CurrencyDataSourceApiController _controllerUnderTest;

        [SetUp]
        public void Setup()
        {

            var loggerMock = new Mock<ILogger<CurrencyDataSourceApiController>>();

            _controllerUnderTest = new CurrencyDataSourceApiController(
                new Repository(),
                loggerMock.Object
            );
        }

        [Test]
        public void Can_get_list_of_data_providers_from_api_controller()
        {
            // Arrange

            // Act
            var datasourcesResult = _controllerUnderTest.GetDataSources() as OkObjectResult;
            var modelReturned = datasourcesResult.Value as IList<CurrencyDataSourceModel>;

            // Assert
            Assert.That(modelReturned, Is.Not.Null);
            Assert.That(modelReturned.Count, Is.GreaterThan(0));
        }

        [Test]
        public void can_save_new_currency_source()
        {
            // Arrange
            var newDataSource = new CurrencyDataSourceModel()
            {
                DataSourceName = "test source",
                DataSourceUrl = "https://exampleUrl/api/v1/currency",
            };

            // Act
            var updateResult = _controllerUnderTest.UpdateDataSource(newDataSource) as OkResult;

            // Assert
            Assert.That(updateResult, Is.Not.Null);
            Assert.That(updateResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

            // Todo: when this project persists data sources to a database, verify the persistance from the source
            // instead for now we have to trust the controller
            var datasourcesResult = _controllerUnderTest.GetDataSources() as OkObjectResult;
            var modelReturned = datasourcesResult.Value as IList<CurrencyDataSourceModel>;

            Assert.That(modelReturned.SingleOrDefault(x => x.DataSourceName == newDataSource.DataSourceName), Is.Not.Null);
            Assert.That(modelReturned.SingleOrDefault(x => x.DataSourceUrl == newDataSource.DataSourceUrl), Is.Not.Null);
        }

        [Test]
        public void can_delete_currency_souce()
        {
            // Arrange

            // in a future version this would be created on a test db
            var testDataSources = _controllerUnderTest.GetDataSources() as OkObjectResult;
            var sourceToRemove = (testDataSources.Value as IList<CurrencyDataSourceModel>).First();

            // Act
            var deleteResult = _controllerUnderTest.DeleteDataSource(sourceToRemove) as OkResult;

            // Assert
            Assert.That(deleteResult, Is.Not.Null);
            Assert.That(deleteResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

            // Todo: when this project persists data sources to a database, verify the persistance from the source
            // instead for now we have to trust the controller
            var datasourcesResult = _controllerUnderTest.GetDataSources() as OkObjectResult;
            var modelListReturned = datasourcesResult.Value as IList<CurrencyDataSourceModel>;

            Assert.That(modelListReturned.Any(x => x.CurrencyDataSourceModelId == sourceToRemove.CurrencyDataSourceModelId), Is.False);
        }

        [Test]
        public void can_update_a_currency_source()
        {
            // Arrange
            // in a future version this would be created on a test db
            var testDataSources = _controllerUnderTest.GetDataSources() as OkObjectResult;
            var sourceToUpdate = (testDataSources.Value as IList<CurrencyDataSourceModel>).First();

            sourceToUpdate.DataSourceName = "updated";
            sourceToUpdate.DataSourceUrl = "https://updatedexample.com/api/v1";

            // Act
            var updateResult = _controllerUnderTest.UpdateDataSource(sourceToUpdate) as OkResult;

            // Assert
            Assert.That(updateResult, Is.Not.Null);
            Assert.That(updateResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

            // Todo: when this project persists data sources to a database, verify the persistance from the source
            // instead for now we have to trust the controller
            var datasourcesResult = _controllerUnderTest.GetDataSources() as OkObjectResult;
            var updatedModel = (datasourcesResult.Value as IList<CurrencyDataSourceModel>).FirstOrDefault(x => x.CurrencyDataSourceModelId == sourceToUpdate.CurrencyDataSourceModelId);

            Assert.That(updatedModel, Is.Not.Null);
            Assert.That(updatedModel.DataSourceName, Is.EqualTo(sourceToUpdate.DataSourceName));
            Assert.That(updatedModel.DataSourceUrl, Is.EqualTo(sourceToUpdate.DataSourceUrl));
        }

    }
}