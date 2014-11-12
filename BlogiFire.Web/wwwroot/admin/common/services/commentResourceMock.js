(function () {
    "use strict";

    var app = angular.module("commentResourceMock", ["ngMockE2E"]);

    app.run(function ($httpBackend) {
        var comments = [
            {
                "Id": 1,
                "PostId" : 1,
                "Author": "commentor 1",
                "Content": "This is a comment one",
                "Created": "2014-11-02",
                "Published": true
            },
            {
                "Id": 2,
                "PostId": 1,
                "Author": "commentor 2",
                "Content": "This is a comment two",
                "Created": "2014-11-02",
                "Published": false
            }
        ];

        var commentUrl = "/api/comments";
        var editingRegex = new RegExp(commentUrl + "/[0-9][0-9]*", '');

        $httpBackend.whenGET(editingRegex).respond(function (method, url, data) {
            var comment = { "Id": 0 };
            var parameters = url.split('/');
            var length = parameters.length;
            var id = parameters[length - 1];

            if (id > 0) {
                for (var i = 0; i < comments.length; i++) {
                    if (comments[i].Id == id) {
                        comment = comments[i];
                        break;
                    }
                };
            }
            return [200, comment, {}];
        });

        $httpBackend.whenGET(commentUrl).respond(comments);

        $httpBackend.whenPOST(commentUrl).respond(function (method, url, data) {
            var comment = angular.fromJson(data);

            if (!comment.Id) {
                // new comment Id
                comment.Id = comments[comments.length - 1].Id + 1;
                comments.push(comment);
            }
            else {
                // Updated comment
                for (var i = 0; i < comments.length; i++) {
                    if (comments[i].Id == comment.Id) {
                        comments[i] = comment;
                        break;
                    }
                };
            }
            return [200, comment, {}];
        });

        $httpBackend.whenGET(/app/).passThrough();
    })
}());