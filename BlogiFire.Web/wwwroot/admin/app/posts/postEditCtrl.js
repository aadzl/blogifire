(function () {
    "use strict";
    angular.module("blogifire").controller("PostEditCtrl", ["post", "$state", PostEditCtrl]);

    function PostEditCtrl(post, $state) {
        var vm = this;

        vm.Title = "Editor";
		vm.post = post;
		
		if (vm.post && vm.post.Id) {
            vm.Title = "Edit: " + vm.post.Title;
        }
        else {
            vm.Title = "New post";
        }
		
		$('.summernote').code(post.Content);
		
    }
}());