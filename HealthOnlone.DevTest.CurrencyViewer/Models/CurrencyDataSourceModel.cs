using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthOnlone.DevTest.CurrencyViewer.Models
{
    /// <summary>
    /// POCO for representing a data source
    /// </summary>
    public class CurrencyDataSourceModel
    {
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
