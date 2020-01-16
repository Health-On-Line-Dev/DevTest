function ViewModel() {

    var self = this;
    self.datasources = ko.observableArray();
    self.listOfCurrencies = ko.observableArray();
}

var model = new ViewModel();

$(document).ready(function () {

    //Get the data sources from our api
    var datasources;

    var listOfCurrencies = [];

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

                model.listOfCurrencies.push(data[i][keys[0]]);

                currenciesFromProvider.currencies.push(thisCurrency);
            }
            
            model.datasources.push(currenciesFromProvider);

            // make a list of currencies - first remove any duplicates, remove all and add from the fresh list
            var deduplicatedList = model.listOfCurrencies().filter((v, i) => model.listOfCurrencies.indexOf(v) === i);
            model.listOfCurrencies.removeAll();
            for (var i = 0; i < deduplicatedList.length; i++) {
                model.listOfCurrencies.push(deduplicatedList[i]);
            }

        }
    }

    function refreshCurrencies(datasources) {
        for (var i = 0; i < datasources.length; i++) {
            getCurrencyRatesFromSource(datasources[i])
        }
    }
});

// gets the average value for a currency across data sources
function getAverage(currencyKey) {

    var numberOfDataSources = model.datasources().length;
    var accumulator = 0;

    if (numberOfDataSources > 0) {

        for (var i = 0; i < numberOfDataSources; i++) {
            for (var j = 0; j < model.datasources()[i].currencies.length; j++) {
                if (model.datasources()[i].currencies[j].name === currencyKey) {
                    accumulator += model.datasources()[i].currencies[j].rate;
                }
            }
        }

        return accumulator / numberOfDataSources;
    }

    return "waiting for data";
}