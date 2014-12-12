﻿function initPager(p) {
    p.pagedItems = [];
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
    init = function () {
        for (var i = 0; i < p.items.length; i++) {
            if (i % p.itemsPerPage === 0) {
                p.pagedItems[Math.floor(i / p.itemsPerPage)] = [p.items[i]];
            } else {
                p.pagedItems[Math.floor(i / p.itemsPerPage)].push(p.items[i]);
            }
        }
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
