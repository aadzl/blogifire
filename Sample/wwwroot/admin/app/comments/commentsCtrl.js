(function () {
    "use strict";
    angular.module("blogifire").controller("commentsCtrl", ["commentsResource", commentsCtrl]);

    function commentsCtrl(commentsResource) {
        var vm = this;
        vm.Title = "Comments";
        vm.pager = {};
        vm.pager.items = [];

        vm.load = function () {
            commentsResource.query(function (data) {
                angular.copy(data, vm.pager.items);
                initPager(vm.pager);
            });
        }  

        vm.showMore = function () {
            vm.pager.nextPage();
            vm.pager.showMoreItems();
            commentsResource.query();
        }
        vm.checkAll = function () {
            vm.pager.toggleCheckAll();
            commentsResource.query();
        }
        vm.isChecked = function () {
            try {
                return vm.pager.itemsChecked();
            }
            catch (err) {
                return false;
            }
        }
        vm.load();
    }
}());