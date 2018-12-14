
$(document).ready(function () {
    loadData(1, "");
});

//Load Data function
function loadData(id, searchString) {
    $.ajax({
        url: "/Admin/Book/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: { "page": id, "searchKey": searchString },
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.data, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.BookID + '</td>';
                html += '<td>' + item.BookName + '</td>';
                html += '<td>' + item.Description + '</td>';
                html += '<td>' + item.Alias + '</td>';
                html += '<td>' + item.AuthorID + '</td>';
                html += '<td>' + item.CategoryID + '</td>';
                html += '<td>' + item.ViewCount + '</td>';
                html += '<td><img src="' + item.MoreImages + '" alt="anhbook" class="img-thumbnail" width="180" height="260"></td>'; //181 257
                var dt = convertDateTime(item.CreatedDate);
                html += '<td>' + dt + '</td>';
                html += '<td>' + item.CreatedBy + '</td>';
                html += '<td>' + item.TopHot + '</td>';
                html += '<td><a class="btn btn-default btn-sm" href="/Admin/Book/Edit/'+item.BookID+'"><i class="fa fa-edit"></i> Edit</a>  <a class="btn btn-danger btn-sm" onclick="DeleteBook(' + item.BookID + ')"><i class="fa fa-trash"></i>Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);

            var paging = '';
            if (result.pageNumber == 1) {
                paging += '<li class="paginate_button previous disabled" ><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
                paging += '<li class="paginate_button active" ><a  href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>';
                paging += '<li class="paginate_button next disabled"><a  href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
            }
            else {
                if (id == 1) {
                    paging += '<li class="paginate_button previous disabled"><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
                    paging += '<li class="paginate_button active"><a href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>';
                }
                else {
                    paging += '<li class="paginate_button previous btn"><a  onclick="return loadData(' + (id - 1) + ',' + result.keyword + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
                    paging += '<li class="paginate_button btn"><a onclick="return loadData(' + (id - 1) + ',' + result.keyword + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">' + (id - 1) + '</a></li>';
                    paging += '<li class="paginate_button active"><a onclick="" aria-controls="example1" data-dt-idx="0" tabindex="0">' + id + '</a></li>';
                }
                if (id + 1 <= result.pageNumber) {
                    paging += '<li class="paginate_button btn"><a  onclick="return loadData(' + (id + 1) + ',' + result.keyword + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">' + (id + 1) + '</a></li>';
                    paging += '<li class="paginate_button next btn" id="example1_next"><a onclick="return loadData(' + (id + 1) + ',' + result.keyword + ')" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
                } else {
                    paging += '<li class="paginate_button next disabled" id="example1_next"><a href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
                }

            }
            $('#phantrang').html(paging);
            $("#searchString").val(result.keyword);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


//Lấy dữ liệu để trả về trang Edit

function DeleteBook(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/Book/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData(1, "");
                $.notify(result.message, {
                    globalPosition: "top center",
                    className: "success"
                });
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes
//function clearTextBox() {
//    $('#UserID').val("");
//    $('#UserName').val("");
//    $('#Password').val("");
//    $('#Name').val("");
//    $('#Address').val("");
//    $('#Email').val("");
//    $('#Phone').val("");
//    $('#CreatedDate').val("");
//    $('#Status').val("");
//    $('#Type').val("");
//    $('#btnUpdate').hide();
//    $('#btnAdd').show();
//    $('#UserName').css('border-color', 'lightgrey');
//    $('#Password').css('border-color', 'lightgrey');
//    $('#Name').css('border-color', 'lightgrey');
//    $('#Address').css('border-color', 'lightgrey');
//    $('#Email').css('border-color', 'lightgrey');
//    $('#Phone').css('border-color', 'lightgrey');
//    $('#CreatedDate').css('border-color', 'lightgrey');
//    $('#Status').css('border-color', 'lightgrey');
//    $('#Type').css('border-color', 'lightgrey');
//}
//Valdidation using jquery
//function validate() {
//    var isValid = true;
//    if ($('#UserName').val().trim() == "") {
//        $('#UserName').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#UserName').css('border-color', 'lightgrey');
//    }
//    if ($('#Password').val().trim() == "") {
//        $('#Password').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Password').css('border-color', 'lightgrey');
//    }
//    if ($('#Name').val().trim() == "") {
//        $('#Name').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Name').css('border-color', 'lightgrey');
//    }
//    if ($('#Address').val().trim() == "") {
//        $('#Address').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Address').css('border-color', 'lightgrey');
//    }
//    return isValid;
//}


function SearchClick() {
    var search_value = document.getElementById('inputKeyword').value;
    loadData(1, search_value);
}

function convertDateTime(datetimeString) {
    if (datetimeString != null) {
        var dateString = datetimeString.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        var hour = currentTime.getHours()
        var minute = currentTime.getMinutes()
        if (minute < 10) {
            minute = "0" + minute;
        }
        var second = currentTime.getSeconds()
        if (second < 10) {
            second = "0" + second;
        }
        var result = day + "/" + month + "/" + year + " " + hour + ":" + minute + ":" + second;
    }
    else {
        result = "Null";
    }
    return result;
}



//Trang Admin/Edit/IDBook
//Thao tác trên bảng ĐỌC ONLINE

function loadListChapterByID(id, page) {
    $.ajax({
        url: "/Admin/Book/GetAllChapterByID",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: { "id": id, "page": page },
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.data, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.NameChapter + '</td>';
                html += '<td><a class="btn btn-default btn-sm" href="/Admin/ChapterDetail/Edit?idbook=' + result.idbook + '&idchapter=' + item.ID + '"><i class="fa fa-edit"></i> Edit</a>  <a class="btn btn-danger btn-sm" onclick="DeleteChapter(' + result.idbook + ',' + item.ID + ')"><i class="fa fa-trash"></i>Delete</a></td>';
                html += '</tr>';
            });
            $('.tablePhuLucbody').html(html);

            var paging = '';
            if (result.pageNumber == 1) {
                paging += '<li class="paginate_button previous disabled" ><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
                paging += '<li class="paginate_button active" ><a  href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>';
                paging += '<li class="paginate_button next disabled"><a  href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
            }
            else {
                if (id == 1) {
                    paging += '<li class="paginate_button previous disabled"><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
                    paging += '<li class="paginate_button active"><a href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>';
                }
                else {
                    paging += '<li class="paginate_button previous btn"><a  onclick="return loadListChapterByID(' + (id - 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
                    paging += '<li class="paginate_button btn"><a onclick="return loadListChapterByID(' + (id - 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">' + (id - 1) + '</a></li>';
                    paging += '<li class="paginate_button active"><a onclick="" aria-controls="example1" data-dt-idx="0" tabindex="0">' + id + '</a></li>';
                }
                if (id + 1 <= result.pageNumber) {
                    paging += '<li class="paginate_button btn"><a  onclick="return loadListChapterByID(' + (id + 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">' + (id + 1) + '</a></li>';
                    paging += '<li class="paginate_button next btn" id="example1_next"><a onclick="return loadListChapterByID(' + (id + 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
                } else {
                    paging += '<li class="paginate_button next disabled" id="example1_next"><a href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
                }

            }

            $('#phanTrangDocOnline').html(paging);

            //Input count chapter
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function getLastChapter() {
    var search_value = document.getElementById('idChapterNew').value;
    return search_value;
}



function DeleteChapter(bookID, chapterID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/ChapterDetail/Delete?bookid=" + bookID + "&chapterid=" + chapterID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadListChapterByID(result.idbook,1)
                $.notify(result.message, {
                    globalPosition: "top center",
                    className: "success"
                });
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}



//Thao tác trên bảng ebook


function loadListEbookByBook(id, page) {
    $.ajax({
        url: "/Admin/Book/GetAllEbookByIDBook",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: { "idbook": id, "page": page },
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.data, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.TypeEbookID + '</td>';
                html += '<td>' + item.TypeEbookName + '</td>';
                html += '<td>' + item.Link + '</td>';
                html += '<td><a class="btn btn-default btn-sm" onclick="getEbookByID(' + result.idbook + ',' + item.TypeEbookID + ')"><i class="fa fa-edit"></i> Edit</a>  <a class="btn btn-danger btn-sm" onclick="DeleteEbook(' + result.idbook + ',' + item.TypeEbookID + ')"><i class="fa fa-trash"></i>Delete</a></td>';
                html += '</tr>';
            });
            $('.tableEbook').html(html);

            var paging = '';
            if (result.pageNumber == 1) {
                paging += '<li class="paginate_button previous disabled" ><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
                paging += '<li class="paginate_button active" ><a  href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>';
                paging += '<li class="paginate_button next disabled"><a  href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
            }
            else {
                if (page == 1) {
                    paging += '<li class="paginate_button previous disabled"><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
                    paging += '<li class="paginate_button active"><a href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>';
                }
                else {
                    paging += '<li class="paginate_button previous btn"><a  onclick="return loadListEbookByBook(' + (page - 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
                    paging += '<li class="paginate_button btn"><a onclick="return loadListEbookByBook(' + (page - 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">' + (id - 1) + '</a></li>';
                    paging += '<li class="paginate_button active"><a onclick="" aria-controls="example1" data-dt-idx="0" tabindex="0">' + id + '</a></li>';
                }
                if (page + 1 <= result.pageNumber) {
                    paging += '<li class="paginate_button btn"><a  onclick="return loadListEbookByBook(' + (page + 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">' + (id + 1) + '</a></li>';
                    paging += '<li class="paginate_button next btn" id="example1_next"><a onclick="return loadListEbookByBook(' + (page + 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
                } else {
                    paging += '<li class="paginate_button next disabled" id="example1_next"><a href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
                }

            }

            $('#phanTrangEbook').html(paging);

            //Input count chapter
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}



//Thêm một ebook
function AddEbook() {
    var res = validateEbook();
    if (res == false) {
        return false;
    }

    var typeebookselected = document.getElementById("ddlTypename");

    var ebookObj = {
        BookID: $('#bookID').val(),
        TypeEbook: typeebookselected[typeebookselected.selectedIndex].value,
        Link: $('#linkDownload').val(),
    };
    $.ajax({
        url: "/Admin/Book/AddEbook",
        data: JSON.stringify(ebookObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //truyền id của book vào
            loadListEbookByBook(result.idbook, 1);
            $('#modalAddOrEditEbook').modal('hide');
            $.notify(result.message, {
                globalPosition: "top center",
                className: "success"
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Lấy ebook theo id
function getEbookByID(idbook,typeid) {
    //$('#typeEbook').css('border-color', 'lightgrey');
    //$('#linkDownload').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Admin/Book/GetEbookByID?idbook=" + idbook + "&typeid=" + typeid,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#bookID').val(result.BookID);
            $('#typeEbook').val(result.TypeEbookID);
            $('#linkDownload').val(result.LinkDownload);
            $('#modalAddOrEditEbook').modal('show');
            $('#btnUpdateEbook').show();
            $('#btnAddEbook').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//chỉnh sữa một ebook
function UpdateEbook() {
    var res = validateEbook();
    if (res == false) {
        return false;
    }

    var typeebookselected = document.getElementById("ddlTypename");

    var ebookObj = {
        BookID: $('#bookID').val(),
        TypeEbook: typeebookselected[typeebookselected.selectedIndex].value,
        Link: $('#linkDownload').val(),
    };

    $.ajax({
        url: "/Admin/Book/UpdateEbook",
        data: JSON.stringify(ebookObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadListEbookByBook(result.idbook, 1);
            $('#modalAddOrEditEbook').modal('hide');
            $('#bookID').val("");
            $('#typeEbook').val("");
            $('#linkDownload').val("");
            $.notify(result.message, {
                globalPosition: "top center",
                className: "success"
            });

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Xoá một ebook
function DeleteEbook(idbook,typeid ) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/Book/DeleteEbook?idbook=" + idbook + "&typeid=" + typeid,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadListEbookByBook(result.id, 1);
                $.notify(result.message, {
                    globalPosition: "top center",
                    className: "success"
                });
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}





function validateEbook() {
    var isValid = true;
    if ($('#linkDownload').val().trim() == "") {
        $('#linkDownload').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#linkDownload').css('border-color', 'lightgrey');
    }
    return isValid;
}


function clearModalEbook() {
    getListTypeEbook();
    $('#linkDownload').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#linkDownload').css('border-color', 'lightgrey');
}

//Load name type ebook lên droplist

function getListTypeEbook()
{
    var url = "/Admin/Book/GetAllListTypeEbook";
    var listtypeebookname = $('#ddlTypename');
    $.getJSON(url, function (response) {
        listtypeebookname.empty();
        $.each(response, function (index, item) {
            listtypeebookname.append($('<option></option>').text(item.TypeEbookName).val(item.TypeEbookID));
        });
    });
}


