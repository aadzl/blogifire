(function () {
    "use strict";
    angular.module("blogifire").controller("postListCtrl",
        ["$rootScope", "$window", "postResource", postListCtrl]);

    function postListCtrl($rootScope, $window, postResource) {
        var vm = this;
        vm.pager = {};
        vm.pager.items = [];

        postResource.query(function (data) {
            angular.copy(data, vm.pager.items);
            initPager(vm.pager);
        });

        vm.prevPage = function () {
            vm.pager.prevPage();
            postResource.query();
        }
        vm.nextPage = function () {
            vm.pager.nextPage();
            postResource.query();
        }
    }
}());
