(function () {
    "use strict";
    angular.module("blogifire").controller("PostListCtrl", ["postResource", PostListCtrl]);

    function PostListCtrl(postResource) {
        var vm = this;

        postResource.query(function(data) {
            vm.posts = data;
        });
    }
}());
