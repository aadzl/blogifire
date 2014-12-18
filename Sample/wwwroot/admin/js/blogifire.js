var loadComments = function (slug) {
    xmlhttp = getXmlHttp();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var commentList = document.getElementById("comments");
            var showComments = document.getElementById("showComments");
            var comments = JSON.parse(xmlhttp.responseText);

            commentList.innerHTML = "";
            showComments.innerHTML = "<h2>Comments</h2>";
            
            for (var i = 0; i < comments.length; i++) {
                var pubDate = new Date(comments[i].Published);
                var options = {
                    weekday: "long", year: "numeric", month: "short", day: "numeric", hour: "2-digit", minute: "2-digit"
                };
                commentList.innerHTML += "<article class='clearfix panel'>";
                commentList.innerHTML += "<img src='http://placehold.it/70x70' class='pull-left' style='margin-right: 10px'>";
                commentList.innerHTML += "<div>" + comments[i].Author + " on " + pubDate.toLocaleTimeString("en-us", options) + "</div>";
                commentList.innerHTML += "<div class='panel-body'>" + comments[i].Content + "</div>";
                commentList.innerHTML += "</article>";
            }
        }
    }
    xmlhttp.open("GET", blogurl + "/comments/" + slug, true);
    xmlhttp.send();
}

var saveComment = function (slug) {
    var status = document.getElementById("status");
    var commentname = document.getElementById("commentname").value;
    var commentemail = document.getElementById("commentemail").value;
    var commentcontent = document.getElementById("commentcontent").value;

    if (commentname.length < 1 || commentemail.length < 1 || commentcontent.length < 1) {
        status.innerHTML = "All fields are required";
        return false;
    }

    xmlhttp = getXmlHttp();
    xmlhttp.open("POST", blogurl + "/comments", false);
    xmlhttp.setRequestHeader("Content-type", "application/json;charset=UTF-8");
    xmlhttp.setRequestHeader("Connection", "close");
    xmlhttp.send(JSON.stringify({ Author: commentname, Email: commentemail, Content: commentcontent, PostId: postid }));

    var response = xmlhttp.responseText;
    if (response && response == "success") {
        status.innerHTML = "success";
        loadComments(slug);
    }
    else {
        status.innerHTML = "failed";
    }
}

var getXmlHttp = function () {
    if (window.XMLHttpRequest) {
        return new XMLHttpRequest();
    } else {
        return new ActiveXObject("Microsoft.XMLHTTP");
    }
}