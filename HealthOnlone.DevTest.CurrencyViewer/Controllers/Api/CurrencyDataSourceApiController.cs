using HealthOnlone.DevTest.CurrencyViewer.Models;
using HealthOnlone.DevTest.CurrencyViewer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace HealthOnlone.DevTest.CurrencyViewer.Controllers.Api
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
    }
}
