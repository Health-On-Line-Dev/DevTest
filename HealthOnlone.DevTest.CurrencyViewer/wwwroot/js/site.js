function ViewModel() {

    var self = this;
    self.datasources = ko.observableArray();
}

var model = new ViewModel();

$(document).ready(function () {

    //Get the data sources from our api
    var datasources;

    function currency(data) {
        this.name = data.name;
        this.value = data.value;
    }

    //function currencySource(data) {
    //    this.name = data.name;

    //    //this.currencies = ko.observableArray(data.currencies);
    //}

    ko.applyBindings(model);
    getDataSources();

    function getDataSources() {
        $.ajax("/api/v1/datasources").done(function (data) {
            datasources = data
            refreshCurrencies(datasources);
        }).fail(function () {
            console.log("failed to get data sources")
        });
    }

    function getCurrencyRatesFromSource(source) {

        $.ajax({
            url: "https://" + source.dataSourceUrl,
            type: "GET",
            crossDomain: true,
            dataType: "json",
            source: source,
            success: function (data) { handleCurrencyFromSource(data, source) },
            error: function (xhr, status) {
                alert("failed to get rates")
            }
        });

        function handleCurrencyFromSource(data, source) {
            var currenciesFromProvider = {
                source: source.dataSourceName,
                currencies : []
            };

            var keys = Object.keys(data[0]);

            for(var i = 0; i < data.length; i++) {

                var thisCurrency = {
                    name: data[i][keys[0]],
                    rate: data[i][keys[2]]
                }

                currenciesFromProvider.currencies.push(thisCurrency);
            }

            model.datasources.push(currenciesFromProvider);
        }
    }

    function refreshCurrencies(datasources) {
        for (var i = 0; i < datasources.length; i++) {
            getCurrencyRatesFromSource(datasources[i])
        }
    }
});