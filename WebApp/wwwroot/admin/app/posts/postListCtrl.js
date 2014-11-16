(function () {
    "use strict";
    angular.module("blogifire").controller("PostListCtrl", ["postResource", "dataService", PostListCtrl]);

    function PostListCtrl(postResource, dataService) {
        var vm = this;

        postResource.query(function(data) {
            vm.posts = data;
        });

        //dataService.getItems('/blog/api/posts')
        //.success(function (data) {
        //    vm.posts = data;
        //})
        //.error(function () {
        //    //toastr.error("error");
        //});
    }
}());
