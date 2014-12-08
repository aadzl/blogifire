(function () {
    "use strict";

    angular.module("common.services").factory("commentsResource", ["$resource", commentsResource]);

    function commentsResource($resource) {
        return $resource(webApiRoot + "comments")
    }
}());