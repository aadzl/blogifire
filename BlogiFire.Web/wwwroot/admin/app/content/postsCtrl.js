(function () {
    "use strict";
    angular.module("blogifire").controller("PostsCtrl", ["postResource", PostsCtrl]);

    function PostsCtrl(postResource) {
        var vm = this;

        postResource.query(function(data) {
            vm.posts = data;
        });
    }
}());
