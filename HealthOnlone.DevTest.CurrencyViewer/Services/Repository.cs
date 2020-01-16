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
        // static data is just for the initial version
        // v2 would involve getting data from a database
        private static IList<CurrencyDataSourceModel> _dataSources;

        private static IList<CurrencyDataSourceModel> Datasources
        {
            get {

                if(_dataSources == null || _dataSources.Count == 0)
                {
                    _dataSources = new List<CurrencyDataSourceModel>
                    {
                        new CurrencyDataSourceModel
                        {
                            DataSourceName = "Currency",
                            DataSourceUrl = "localhost:44374/api/v1/Currency"
                        },

                        new CurrencyDataSourceModel
                        {
                            DataSourceName = "Exchange",
                            DataSourceUrl = "localhost:44382/api/v1/Exchange"
                        }
                    };
                }

                return _dataSources;
            }
        }

        /// <summary>
        /// Gets the currently available data sources
        /// </summary>
        /// <returns>A list of data sources</returns>
        public IList<CurrencyDataSourceModel> GetCurrencyDataSourceModels()
        {
             return Datasources;          
        }
    }
}
