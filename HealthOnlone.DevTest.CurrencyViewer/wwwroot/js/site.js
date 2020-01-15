// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    //Get the data sources from our api
    var datasources;

    function currency(data) {
        this.name = data.name;
        this.value = data.value;
    }

    function currencySource(data) {
        this.name = data.name;

        this.currencies = ko.observableArray(data.currencies);
    }

    function ViewModel() {

        var self = this
        var datasources = ko.observableArray()
    }

    var model = new ViewModel();
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
            success: handleCurrencyFromSource,
            error: function (xhr, status) {
                alert("failed to get rates")
            }
        });

        function handleCurrencyFromSource(data) {
            
        }
    }

    function refreshCurrencies(datasources) {
        for (var i = 0; i < datasources.length; i++) {
            getCurrencyRatesFromSource(datasources[i])
        }
    }
});