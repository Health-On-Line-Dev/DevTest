using HealthOnlone.DevTest.CurrencyViewer.Models;
using System.Collections.Generic;

namespace HealthOnlone.DevTest.CurrencyViewer.Services
{
    /// <summary>
    /// Interface for the main repository
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Method to fetch the Currency Data Sources
        /// </summary>
        /// <returns>A List of data sources</returns>
        IList<CurrencyDataSourceModel> GetCurrencyDataSourceModels();

        /// <summary>
        /// Updates the data source. If the GUID is empty it will create a new one
        /// </summary>
        /// <param name="source">The data source to create or update</param>
        void UpdateDataSource(CurrencyDataSourceModel source);

        /// <summary>
        /// Removes a data source
        /// </summary>
        /// <param name="source">The datasource to remove</param>
        void DeleteDataSource(CurrencyDataSourceModel source);
    }
}
