(function () {
    "use strict";

    angular.module("common.services").factory("settingsResource", ["$resource", settingsResource]);

    function settingsResource($resource) {
        return $resource("/blog/api/settings")
    }
}());