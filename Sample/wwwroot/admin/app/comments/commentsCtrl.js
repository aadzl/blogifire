(function () {
    "use strict";
    angular.module("blogifire").controller("commentsCtrl", [commentsCtrl]);

    function commentsCtrl() {
        var vm = this;

        vm.Title = "Comments";
    }
}());