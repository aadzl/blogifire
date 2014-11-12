(function () {
    "use strict";

    var app = angular.module("postResourceMock", ["ngMockE2E"]);

    app.run(function ($httpBackend) {
        var posts = [
            {
                "Id": 1,
                "Title": "Post One",
                "Author": "admin",
				"Content": "This is a post one",
                "Comments": 2,
                "Created": "2014-11-02",
                "Published": true
            },
            {
                "Id": 2,
                "Title": "Post Two",
                "Author": "editor",
				"Content": "This is a post two",
                "Comments": 0,
                "Created": "2014-11-02",
                "Published": false
            }
        ];

        var postUrl = "/api/posts";
        var editingRegex = new RegExp(postUrl + "/[0-9][0-9]*", '');

        $httpBackend.whenGET(editingRegex).respond(function (method, url, data) {
            var post = {"Id": 0};
            var parameters = url.split('/');
            var length = parameters.length;
            var id = parameters[length - 1];

            if (id > 0) {
                for (var i = 0; i < posts.length; i++) {
                    if (posts[i].Id == id) {
                        post = posts[i];
                        break;
                    }
                };
            }
            return [200, post, {}];
        });
		
		$httpBackend.whenGET(postUrl).respond(posts);

        $httpBackend.whenPOST(postUrl).respond(function (method, url, data) {
            var post = angular.fromJson(data);

            if (!post.Id) {
                // new post Id
                post.Id = posts[posts.length - 1].Id + 1;
                posts.push(post);
            }
            else {
                // Updated post
                for (var i = 0; i < posts.length; i++) {
                    if (posts[i].Id == post.Id) {
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