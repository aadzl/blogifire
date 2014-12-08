(function () {
    "use strict";
    angular.module("blogifire").controller("postEditCtrl", ["post", "$state", "$window", "$http", postEditCtrl]);

    function postEditCtrl(post, $state, $window, $http) {
        var vm = this;

        vm.post = post;
        vm.postCopy = angular.copy(vm.post);
        $('.summernote').code(vm.postCopy.Content);

        this.save = function () {
            vm.postCopy.Content = $('.summernote').code();
            vm.postCopy.Saved = new Date();
            vm.postCopy.Slug = this.convertToSlug(vm.postCopy.Title);

            if (!vm.postCopy.BlogId) {
                vm.postCopy.BlogId = 0;
            }         
            
            vm.postCopy.$save(function (data) {
                toastr.success('saved');
            }, function (data) {
                toastr.error('failed');
            });
        }

        this.publish = function () {
            vm.post.Published = new Date();
            this.save();
        }

        this.cancel = function () {
            //vm.post = vm.postCopy;
            $window.history.back();
        }

        this.convertToSlug = function (title) {
            return title.toLowerCase()
                .replace(/[^\w ]+/g, '')
                .replace(/ +/g, '-');
        }
    }
}());