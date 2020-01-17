using System;

namespace HealthOnlone.DevTest.CurrencyViewer.Models
{
    /// <summary>
    /// POCO for representing a data source
    /// </summary>
    public class CurrencyDataSourceModel
    {
        /// <summary>
        /// The id for the instance
        /// </summary>
        public Guid CurrencyDataSourceModelId { get; set; }

        /// <summary>
        /// The name of the currency data provider
        /// </summary>
        public string DataSourceName { get; set; }

        /// <summary>
        /// The url to their data source
        /// </summary>
        public string DataSourceUrl {get; set;}
    }
}
