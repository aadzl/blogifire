var loadComments = function (){
    xmlhttp = getXmlHttp();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            document.getElementById("status").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("GET", "/blog/post/comments", true);
    xmlhttp.send();
}

var saveComment = function () {
    var status = document.getElementById("status");
    var commentname = document.getElementById("commentname").value;
    var commentemail = document.getElementById("commentemail").value;
    var commentcontent = document.getElementById("commentcontent").value;

    if (window.XMLHttpRequest) {
        xmlhttp = new XMLHttpRequest();
    } else {
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.open("POST", apiurl, false);
    xmlhttp.setRequestHeader("Content-type", "application/json;charset=UTF-8");
    xmlhttp.setRequestHeader("Connection", "close");
    xmlhttp.send(JSON.stringify({ Author: commentname, Email: commentemail, Content: commentcontent, PostId: postid }));

    var response = xmlhttp.responseText;
    if (response && response == "success") {
        status.innerHTML = "success";
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