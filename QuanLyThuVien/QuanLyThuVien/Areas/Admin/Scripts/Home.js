
$(document).ready(function () {
    //loadListChapeterByIDbOOK(1,6);
    //loadListChapterByID(6, 1);
});

//function loadListChapeterByIDbOOK(id, page) {
//    $.ajax({
//        url: "/Admin/Book/GetAllChapterByID",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        data: { "page": page, "id": id },
//        dataType: "json",
//        success: function (result) {
//            var html = '';
//            $.each(result.data, function (key, item) {
//                html += '<tr>';
//                html += '<td>' + item.ID + '</td>';
//                html += '<td>' + item.NameChapter + '</td>';
//                html += '<td><a class="btn btn-default btn-sm" onclick="return getbyID(' + item.ID + ')"><i class="fa fa-edit"></i> Edit</a>  <a class="btn btn-danger btn-sm" onclick="Delele(' + item.ID + ')"><i class="fa fa-trash"></i>Delete</a></td>';
//                html += '</tr>';
//            });
//            $('.tablePhuLucbody').html(html);

//            var paging = '';
//            if (result.pageNumber == 1) {
//                paging += '<li class="paginate_button previous disabled" ><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
//                paging += '<li class="paginate_button active" ><a  href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>';
//                paging += '<li class="paginate_button next disabled"><a  href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
//            }
//            else {
//                if (page == 1) {
//                    paging += '<li class="paginate_button previous disabled"><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
//                    paging += '<li class="paginate_button active"><a href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>';
//                }
//                else {
//                    paging += '<li class="paginate_button previous btn"><a  onclick="return loadListChapeterByIDbOOK(' + (page - 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">Previous</a></li>';
//                    paging += '<li class="paginate_button btn"><a onclick="return loadListChapeterByIDbOOK(' + (page - 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">' + (id - 1) + '</a></li>';
//                    paging += '<li class="paginate_button active"><a onclick="" aria-controls="example1" data-dt-idx="0" tabindex="0">' + page + '</a></li>';
//                }
//                if (id + 1 <= result.pageNumber) {
//                    paging += '<li class="paginate_button btn"><a  onclick="return loadListChapeterByIDbOOK(' + (page + 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="0" tabindex="0">' + (id + 1) + '</a></li>';
//                    paging += '<li class="paginate_button next btn" id="example1_next"><a onclick="return loadListChapeterByIDbOOK(' + (page + 1) + ',' + result.idbook + ')" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
//                } else {
//                    paging += '<li class="paginate_button next disabled" id="example1_next"><a href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Next</a></li>';
//                }

//            }
//            $('#phanTrangDocOnline').html(paging);
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}





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
                html += '<td><a class="btn btn-default btn-sm" href="/Admin/Book/Edit/' + item.BookID + '"><i class="fa fa-edit"></i> Edit</a>  <a class="btn btn-danger btn-sm" onclick="DeleteBook(' + item.BookID + ')"><i class="fa fa-trash"></i>Delete</a></td>';
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
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


