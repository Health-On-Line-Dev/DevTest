function ViewModel() {

    var self = this;
    self.datasources = ko.observableArray();
    self.listOfCurrencies = ko.observableArray();
    self.listOfProviders = ko.observableArray();

    // for comparisons
    self.selectedCurrency1 = ko.observable();
    self.selectedCurrency2 = ko.observable();

    // for calculations
    self.selectedProvider = ko.observable();
    self.amountForCalculation = ko.observable();
    self.selectedCurrencyForCalculation = ko.observable();
    self.selectedCurrencyForCalculation2 = ko.observable();

}

var model = new ViewModel();

$(document).ready(function () {

    //Get the data sources from our api
    var datasources;

    //var listOfCurrencies = [];

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

            model.listOfProviders.push(currenciesFromProvider.source);

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

        return (accumulator / numberOfDataSources).toFixed(2);
    }

    return "waiting for data";
}

// converts between 2 currencies for each provider
function convertBetweenProvider(data) {

    var currency1 = 0;
    var currency2 = 0;

    for (var i = 0; i < data.currencies.length; i++) {

        if (currency1 < 0 && currency2 < 0) {
            break; // all currencies matched, get out of the loop
        }

        if (data.currencies[i].name === model.selectedCurrency1()) {
            currency1 = data.currencies[i].rate;
        }

        if (data.currencies[i].name === model.selectedCurrency2()) {
            currency2 = data.currencies[i].rate;
        }

    }

    return currency2 > 0 ? (currency1 / currency2).toFixed(4) : 0;
}

function calculate(amount, source, currency1, currency2) {

    var rate1 = 0;
    var rate2 = 0;

    if (amount() >= 0) {
        for (var i = 0; i < model.datasources().length; i++) {
            if (model.datasources()[i].source === source()) {

                for (var j = 0; j < model.datasources()[i].currencies.length; j++) {
                    if (model.datasources()[i].currencies[j].name === currency1()) {
                        rate1 = model.datasources()[i].currencies[j].rate;
                    }   

                    if (model.datasources()[i].currencies[j].name === currency2()) {
                        rate2 = model.datasources()[i].currencies[j].rate;
                    }   
                }
                break;
            }
        }

        if (rate1 > 0) {
            return (amount() * (rate2 / rate1)).toFixed(2);
        }        
    }

    return 0;
}

function changeTabs(element) {

    var tabs = $("a.nav-link.currencyTab");

    for (var i = 0; i < tabs.length; i++) {
        if (tabs[i].id === element.id) {
            $("#"+tabs[i].id+"Panel").show();
            $(tabs[i]).addClass("active");
        } else {
            $("#" + tabs[i].id + "Panel").hide();
            $(tabs[i]).removeClass("active");
        }
    }
}