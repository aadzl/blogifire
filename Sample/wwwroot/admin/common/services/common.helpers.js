function initPager(p) {
    p.pagedItems = [];
    p.itemsToShow = [];
    p.currentPage = 0;
    if (p.itemsPerPage === undefined) {
        p.itemsPerPage = 10;
    }
    p.prevPage = function () {
        if (p.currentPage > 0) {
            p.currentPage--;
        }
    };
    p.nextPage = function () {
        if (p.currentPage < p.pagedItems.length - 1) {
            p.currentPage++;
        }
    };
    p.showMoreItems = function () {
        p.itemsToShow = [];
        for (var i = 0; i <= p.currentPage; i++) {
            for (var j = 0; j < p.pagedItems[i].length; j++) {
                p.itemsToShow.push(p.pagedItems[i][j]);
            }
        }
    }
    init = function () {
        for (var i = 0; i < p.items.length; i++) {
            if (i % p.itemsPerPage === 0) {
                p.pagedItems[Math.floor(i / p.itemsPerPage)] = [p.items[i]];
            } else {
                p.pagedItems[Math.floor(i / p.itemsPerPage)].push(p.items[i]);
            }
        }
        p.itemsToShow.push.apply(p.itemsToShow, p.pagedItems[0]);
    };
    init();
}

function uploadFile(file, editor, welEditable) {
    $.ajax({
        data: file,
        type: "POST",
        url: ApiRoot + "upload/" + file.name,
        cache: false,
        contentType: "multipart/form-data",
        processData: false,
        success: function (url) {
            editor.insertImage(welEditable, url);
        }
    });
}
