(function () {
    "use strict";
    angular.module("blogifire").controller("PostEditCtrl", ["post", "dataService", "$state", "$window", PostEditCtrl]);

    function PostEditCtrl(post, dataService, $state, $window) {
        var vm = this;

        vm.Title = "Editor";
        vm.post = post;
        vm.postCopy = angular.copy(post); // compare on cancel
		
		if (vm.post && vm.post.Id) {
            vm.Title = "Edit: " + vm.post.Title;
        }
        else {
            vm.Title = "New post";
        }
		
		$('.summernote').code(post.Content);
		
		this.save = function() {
		    if (vm.post.Id > 0) {
		        dataService.updateItem("/blog/api/posts/" + vm.post.Id, vm.post)
		    }
		    else {
		        var p = {};
		        p = { "Title": "Post four", "Content": "This is post four", "CommentsCount": 0, "Published": true };
		        dataService.addItem("/blog/api/posts", p);
		    }
		    
		}
		this.publish = function() {
		    alert('will publish ' + vm.post.Id);
		}
		this.cancel = function () {
		    vm.post = vm.postCopy; // restore state
		    $window.history.back();
		}
    }
}());