(function () {
    "use strict";
    var app = angular.module("blogifire", ["common.services", "ui.router"]); // , "postResourceMock"

    app.config(function ($provide) {
        $provide.decorator("$exceptionHandler",
        ["$delegate", function ($delegate) {
            return function (exception, cause) {
                exception.message = "Error happened. \n Message: " + exception.message;
                $delegate(exception, cause);
                toastr.error(exception.message);
            };
        }]);
    });

    app.config(["$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            toastr.options.positionClass = 'toast-bottom-right';
            toastr.options.backgroundpositionClass = 'toast-bottom-right';

            $urlRouterProvider.otherwise("/");

            $stateProvider
            .state("dashbaord", {
                url: "/",
                templateUrl: "app/dashboard/dashboard.html",
                controller: "dashboardCtrl as vm",
                authenticate: true
            })
            .state("postList", {
                url: "/posts",
                templateUrl: "app/posts/postListView.html",
                controller: "postListCtrl as vm",
                authenticate: true
            })
 			.state("postEdit", {
 			    url: "/posts/:Id",
 				templateUrl: "app/posts/postEditView.html",
 				controller: "postEditCtrl as vm",
 				authenticate: true,
 				resolve: {
 				    postResource: "postResource",
 				    post: function (postResource, $stateParams) {
 				        var Id = $stateParams.Id;
 				        return postResource.get({ Id: Id }).$promise;
 				    }
 				}
 			})
            .state("comments", {
                url: "/comments",
                templateUrl: "app/comments/comments.html",
                controller: "commentsCtrl as vm",
                authenticate: true
            })
            .state("settings", {
                url: "/settings",
                templateUrl: "app/settings/settings.html",
                controller: "settingsCtrl as vm",
                authenticate: true
            })
            .state("help", {
                url: "/help",
                templateUrl: "app/help/help.html",
                controller: "HelpCtrl as vm",
                authenticate: true
            })
            .state("profile", {
                url: "/profile",
                templateUrl: "app/profile/profile.html",
                controller: "profileCtrl as vm",
                authenticate: true
            })
        }]
    );

    app.run(function ($rootScope, $state, $http, authService) {
        $rootScope.$on("$stateChangeStart",
            function (event, toState, toParams, fromState, fromParams) {
                // check authentication and redirect to login if false
                if (toState.authenticate && !authService.isAuthenticated) {
                    window.location.href = webRoot + 'account/login';
                }
            }
        );
    });

}());