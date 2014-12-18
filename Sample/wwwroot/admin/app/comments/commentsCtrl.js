(function () {
    "use strict";
    angular.module("blogifire").controller("commentsCtrl", ["commentsResource", "dataService", commentsCtrl]);

    function commentsCtrl(commentsResource, dataService) {
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
        vm.processChecked = function (action) {
            dataService.processChecked(ApiRoot + "comments/" + action, vm.pager.selectedItems)
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