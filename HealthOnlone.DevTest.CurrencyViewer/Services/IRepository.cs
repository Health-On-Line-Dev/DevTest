using HealthOnlone.DevTest.CurrencyViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
