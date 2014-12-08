(function () {
    "use strict";
    angular.module("blogifire").controller("dashboardCtrl", [dashboardCtrl]);

    function dashboardCtrl() {
        var vm = this;

        vm.Title = "Dashboard";
    }
}());