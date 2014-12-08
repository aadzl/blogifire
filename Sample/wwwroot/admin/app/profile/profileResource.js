(function () {
    "use strict";

    angular.module("common.services").factory("profileResource", ["$resource", profileResource]);

    function profileResource($resource) {
        return $resource(webApiRoot + "settings")
    }
}());