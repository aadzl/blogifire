(function () {
    "use strict";
    angular.module("blogifire").controller("DashboardCtrl", [DashboardCtrl]);

    function DashboardCtrl() {
        var vm = this;

        vm.Title = "Dashboard";

        //dashboardResource.query(function (data) {
        //    vm.dashboard = data;
        //});
    }
}());