(function () {
    "use strict";
    angular.module("blogifire").controller("PostEditCtrl", [PostEditCtrl]);

    function PostEditCtrl() {
        var vm = this;

        vm.Title = "Post";
    }
}());