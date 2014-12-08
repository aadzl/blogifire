(function () {
    "use strict";

    angular.module("common.services").factory("dashboardResource", ["$resource", dashboardResource]);

    function dashboardResource($resource) {
        return $resource(webApiRoot + "dashboard")
    }
}());