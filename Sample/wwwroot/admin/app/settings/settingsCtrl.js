(function () {
    "use strict";
    angular.module("blogifire").controller("settingsCtrl", ["settingsResource", settingsCtrl]);

    function settingsCtrl(settingsResource) {
        var vm = this;
        vm.Title = "Settings";
        vm.item = {};

        settingsResource.get(function (data) {
            vm.item = data;
        });
    }
}());