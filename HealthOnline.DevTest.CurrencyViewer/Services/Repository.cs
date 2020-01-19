using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthOnline.DevTest.CurrencyViewer.Models;

namespace HealthOnline.DevTest.CurrencyViewer.Services
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
                            CurrencyDataSourceModelId = Guid.NewGuid(),
                            DataSourceName = "Currency",
                            DataSourceUrl = "localhost:44374/api/v1/Currency"
                        },

                        new CurrencyDataSourceModel
                        {
                            CurrencyDataSourceModelId = Guid.NewGuid(),
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

        public void DeleteDataSource(CurrencyDataSourceModel source)
        {
            var itemToRemove = _dataSources.Where(x => x.CurrencyDataSourceModelId == source.CurrencyDataSourceModelId)
                .FirstOrDefault();

            if(itemToRemove != null)
            {
                _dataSources.Remove(itemToRemove);
            }
        }

        /// <summary>
        /// Updates the repository of data sources. This method can create or update based on existing data
        /// </summary>
        /// <param name="source">The source to update or create</param>
        public void UpdateDataSource(CurrencyDataSourceModel source)
        {
            // find out if the item already exists
            var exitsingItem = Datasources.Where(x => x.CurrencyDataSourceModelId == source.CurrencyDataSourceModelId)
                .FirstOrDefault();

            if(exitsingItem == null)
            {
                // the data source is brand new
                // in a future version the ORM would manage the id, for now we will manage it here
                source.CurrencyDataSourceModelId = Guid.NewGuid();

                Datasources.Add(source);
            } else
            {
                exitsingItem.DataSourceName = source.DataSourceName;
                exitsingItem.DataSourceUrl = source.DataSourceUrl;
            }            
        }
    }
}
