(function () {
    "use strict";

    var app = angular.module("postResourceMock", ["ngMockE2E"]);

    app.run(function ($httpBackend) {
        var posts = [
            {
                "PostId": 1,
                "Title": "Post One",
                "Author": "admin",
                "Comments": 2,
                "Created": "2014-11-02",
                "Published": true
            },
            {
                "PostId": 2,
                "Title": "Post Two",
                "Author": "admin",
                "Comments": 0,
                "Created": "2014-11-02",
                "Published": true
            }
        ];

        var postUrl = "/api/posts";
        var editingRegex = new RegExp(postUrl + "/[0-9][0-9]*", '');

        $httpBackend.whenGET(postUrl).respond(posts);

        $httpBackend.whenGET(editingRegex).respond(function (method, url, data) {
            var post = {"postId": 0};
            var parameters = url.split('/');
            var length = parameters.length;
            var id = parameters[length - 1];

            if (id > 0) {
                for (var i = 0; i < posts.length; i++) {
                    if (posts[i].postId == id) {
                        post = posts[i];
                        break;
                    }
                };
            }
            return [200, post, {}];
        });

        $httpBackend.whenPOST(postUrl).respond(function (method, url, data) {
            var post = angular.fromJson(data);

            if (!post.postId) {
                // new post Id
                post.postId = posts[posts.length - 1].postId + 1;
                posts.push(post);
            }
            else {
                // Updated post
                for (var i = 0; i < posts.length; i++) {
                    if (posts[i].postId == post.postId) {
                        posts[i] = post;
                        break;
                    }
                };
            }
            return [200, post, {}];
        });

        $httpBackend.whenGET(/app/).passThrough();
    })
}());