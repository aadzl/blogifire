(function () {
    "use strict";
    angular.module("blogifire").controller("ContentCtrl", ["postResource", ContentCtrl]);

    function ContentCtrl(postResource) {
        var vm = this;

        postResource.query(function(data) {
            vm.posts = data;
        });
    }
}());
