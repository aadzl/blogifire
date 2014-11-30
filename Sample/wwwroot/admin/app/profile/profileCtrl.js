(function () {
    "use strict";
    angular.module("blogifire").controller("profileCtrl", ["profileResource", profileCtrl]);

    function profileCtrl(profileResource) {
        var vm = this;
        vm.Title = "Profile";
        vm.item = {};

        profileResource.get(function (data) {
            vm.item = data;
        });
    }
}());