using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthOnlone.DevTest.CurrencyViewer.Models;

namespace HealthOnlone.DevTest.CurrencyViewer.Services
{
    /// <summary>
    /// The repository for accessing data
    /// </summary>
    public class Repository : IRepository
    {
        /// <summary>
        /// Gets the currently available data sources
        /// </summary>
        /// <returns>A list of data sources</returns>
        public IList<CurrencyDataSourceModel> GetCurrencyDataSourceModels()
        {
            try
            {
                // for now use dummy data
                var sources = new List<CurrencyDataSourceModel>();

                sources.Add(new CurrencyDataSourceModel
                {
                    DataSourceName = "Currency",
                    DataSourceUrl = "localhost:44374/api/v1/Currency"
                });

                sources.Add(new CurrencyDataSourceModel
                {
                    DataSourceName = "Exchange",
                    DataSourceUrl = "localhost:44374/api/v1/Exchange"
                });

                return sources;
            } catch (Exception e)
            {
                throw e;
            }            
        }
    }
}
