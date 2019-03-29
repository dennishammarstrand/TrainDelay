// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var map;
function loadMapScenario() {
    map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
        center: new Microsoft.Maps.Location(56.8967, 12.8034),
        zoom: 10
    });
}

function loadPushpins() {
    array.forEach(function (element) {
        var pos = element.indexOf(" ");
        var suB = element.substring(pos + 1);
        var firstSpace = suB.indexOf(" ");
        var long = suB.substring(1, firstSpace);
        var lastPar = suB.length;
        var lat = suB.substring(firstSpace + 1, lastPar - 1);
        var coordinate = new Microsoft.Maps.Location(lat, long);
        var pushpin = new Microsoft.Maps.Pushpin(coordinate);
        map.entities.push(pushpin);
    })
}

window.onload = function () {
    loadMapScenario()
    loadPushpins()
}
