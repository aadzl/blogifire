(function () {
    "use strict";
    var app = angular.module("blogifire", ["common.services", "ui.router", "postResourceMock"]);

    app.config(function ($provide) {
        $provide.decorator("$exceptionHandler",
        ["$delegate", function ($delegate) {
            return function (exception, cause) {
                exception.message = "Error happened. \n Message: " + exception.message;
                $delegate(exception, cause);
                alert(exception.message);
            };
        }]);
    });

    app.config(["$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise("/");

            $stateProvider
            .state("postList", {
                url: "/",
                templateUrl: "app/content/posts.html",
                controller: "PostsCtrl as vm"
            })
            .state("postEdit", {
                url: "/content/edit/:postId",
                templateUrl: "app/content/edit.html",
                controller: "EditCtrl as vm",
                resolve: {
                    postResource: "postResource",

                    post: function (postResource, $stateParams) {
                        var postId = $stateParams.postId;
                        return postResource.get({ postId: postId }).$promise;
                    }
                }
            })
            .state("settings", {
                url: "/settings",
                templateUrl: "app/settings/settings.html",
                controller: "SettingsCtrl as vm"
            })
            .state("help", {
                url: "/help",
                templateUrl: "app/help/help.html",
                controller: "HelpCtrl as vm"
            })
            .state("profile", {
                url: "/profile",
                templateUrl: "app/profile/profile.html",
                controller: "ProfileCtrl as vm"
            })
        }]
    );
}());