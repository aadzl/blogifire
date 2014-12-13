(function () {
    "use strict";
    angular.module("blogifire").controller("postListCtrl",
        ["$window", "postResource", "dataService", postListCtrl]);

    function postListCtrl($window, postResource, dataService) {
        var vm = this;
        vm.pager = {};
        vm.pager.items = [];

        vm.load = function () {
            postResource.query(function (data) {
                angular.copy(data, vm.pager.items);
                initPager(vm.pager);
            });
        }
        vm.newPost = function () {
            window.location = "#/posts/0";
        }
        vm.processChecked = function (action) {
            dataService.processChecked(ApiRoot + "posts/" + action, vm.pager.selectedItems)
            .success(function (data) {
                toastr.success("Processed");
                if ($('#chkAll')) {
                    $('#chkAll').prop('checked', false);
                }
                vm.load();
            })
            .error(function (data) {
                toastr.error("Failed");
            });
        }

        vm.prevPage = function () {
            vm.pager.prevPage();
            postResource.query();
        }
        vm.nextPage = function () {
            vm.pager.nextPage();
            postResource.query();
        }
        vm.showMore = function () {
            vm.pager.nextPage();
            vm.pager.showMoreItems();
            postResource.query();
        }
        vm.checkAll = function () {
            vm.pager.toggleCheckAll();
            postResource.query();
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
