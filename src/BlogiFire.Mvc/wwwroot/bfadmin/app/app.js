(function () {
    "use strict";
    var app = angular.module("blogifire",
        ["common.services",
            "ui.router",
            "postResourceMock"]);

    app.config(function ($provide) {
        $provide.decorator("$exceptionHandler",
            ["$delegate",
                function ($delegate) {
                    return function (exception, cause) {
                        exception.message = "Error happened. \n Message: " +
                                                                exception.message;
                        $delegate(exception, cause);
                        alert(exception.message);
                    };
                }]);
    });

    app.config(["$stateProvider",
            "$urlRouterProvider",
            function ($stateProvider, $urlRouterProvider) {
                $urlRouterProvider.otherwise("/");

                $stateProvider
                    //.state("home", {
                    //    url: "/",
                    //    templateUrl: "app/dashboard/dashboardView.html",
                    //    controller: "DashboardCtrl as vm"
                    //})
                    .state("content", {
                        url: "/",
                        templateUrl: "app/content/contentView.html",
                        controller: "ContentCtrl as vm"
                    })
                    .state("postEdit", {
                        url: "/content/postedit/:postId",
                        templateUrl: "app/content/postEditView.html",
                        controller: "PostEditCtrl as vm",
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
                        templateUrl: "app/settings/settingsView.html",
                        controller: "SettingsCtrl as vm"
                    })
                    .state("help", {
                        url: "/help",
                        templateUrl: "app/help/helpView.html",
                        controller: "HelpCtrl as vm"
                    })
            }]
    );
}());