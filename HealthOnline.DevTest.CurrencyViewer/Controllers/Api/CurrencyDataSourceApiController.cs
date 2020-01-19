using HealthOnline.DevTest.CurrencyViewer.Models;
using HealthOnline.DevTest.CurrencyViewer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace HealthOnline.DevTest.CurrencyViewer.Controllers.Api
{
    /// <summary>
    /// A controller that handles CRUD operations for currency data sources
    /// </summary>
    [Route("api/v1")]
    public class CurrencyDataSourceApiController : ControllerBase
    {

        private IRepository _repository;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public CurrencyDataSourceApiController(IRepository repository, ILogger<CurrencyDataSourceApiController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        /// <summary>
        /// Gets the main currency data sources
        /// </summary>
        /// <returns>A list of data sources</returns>
        [HttpGet]
        [Route("datasources")]
        public IActionResult GetDataSources()
        {
            try
            {
                // dummy data for now
                var models = _repository.GetCurrencyDataSourceModels();

                return Ok(models);

            } catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Error in GetDataSource", e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }            
        }

        [HttpPost]
        [Route("datasources")]
        public IActionResult UpdateDataSource([FromBody] CurrencyDataSourceModel source)
        {
            try
            {
                // if the provided source doesn't have an id provide it with the empty GUID to signify it is new
                if(source.CurrencyDataSourceModelId == null)
                {
                    source.CurrencyDataSourceModelId = Guid.Empty;
                }

                _repository.UpdateDataSource(source);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Error in CreateDataSource", e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("datasoureces")]
        public IActionResult DeleteDataSource([FromBody] CurrencyDataSourceModel source)
        {
            try
            {
                _repository.DeleteDataSource(source);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Error in CreateDataSource", e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
