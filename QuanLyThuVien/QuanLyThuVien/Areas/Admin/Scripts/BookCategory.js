//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData(1, "");
});

//Load Data function
function loadData(id, searchString) {
    $.ajax({
        url: "/Admin/BookCategory/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: { "page": id, "searchKey": searchString },
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.data, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.CategoryID + '</td>';
                html += '<td>' + item.CategoryName + '</td>';
                html += '<td>' + item.SeoTitle + '</td>';
                html += '<td>' + item.MetaKeywords + '</td>';
                html += '<td>' + item.MetaDescription + '</td>';
                html += '<td>' + item.Status + '</td>';
                html += '<td>' + item.CreatedDate + '</td>';
                html += '<td>' + item.CreatedBy + '</td>';
                html += '<td>' + item.ShowOnHome + '</td>';
                html += '<td><a class="btn btn-default btn-sm" onclick="return getbyID(' + item.CategoryID + ')"><i class="fa fa-edit"></i> Edit</a>  <a class="btn btn-danger btn-sm" onclick="Delele(' + item.CategoryID + ')"><i class="fa fa-trash"></i>Delete</a></td>';
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

//Add Data Function 
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var categoryObj = {
        CategoryID: $('#CategoryID').val(),
        CategoryName: $('#CategoryName').val(),
        SeoTitle: $('#SeoTitle').val(),
        MetaKeywords: $('#MetaKeywords').val(),
        MetaDescription: $('#MetaDescription').val(),
        Status: $('#Status').val(),
        CreatedDate: $('#CreatedDate').val(),
        CreatedBy: $('#CreatedBy').val(),
        ShowOnHome: $('#ShowOnHome').val(),
    };
    $.ajax({
        url: "/Admin/BookCategory/Add",
        data: JSON.stringify(categoryObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData(1, "");
            $('#myModal').modal('hide');
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

//Function for getting the Data Based upon Employee ID
function getbyID(CategoryID) {
    $('#CategoryName').css('border-color', 'lightgrey');
    $('#SeoTitle').css('border-color', 'lightgrey');
    $('#MetaKeywords').css('border-color', 'lightgrey');
    $('#MetaDescription').css('border-color', 'lightgrey');
    $('#Status').css('border-color', 'lightgrey');
    $('#CreatedDate').css('border-color', 'lightgrey');
    $('#CreatedBy').css('border-color', 'lightgrey');
    $('#ShowOnHome').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Admin/BookCategory/GetbyID/" + CategoryID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#CategoryID').val(result.CategoryID);
            $('#CategoryName').val(result.CategoryName);
            $('#SeoTitle').val(result.SeoTitle);
            $('#MetaKeywords').val(result.MetaKeywords);
            $('#MetaDescription').val(result.MetaDescription);
            $('#Status').val(result.Status);
            $('#CreatedDate').val(result.CreatedDate);
            $('#CreatedBy').val(result.CreatedBy);
            $('#ShowOnHome').val(result.ShowOnHome);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var categoryObj = {
        CategoryID: $('#CategoryID').val(),
        CategoryName: $('#CategoryName').val(),
        SeoTitle: $('#SeoTitle').val(),
        MetaKeywords: $('#MetaKeywords').val(),
        MetaDescription: $('#MetaDescription').val(),
        Status: $('#Status').val(),
        CreatedDate: $('#CreatedDate').val(),
        CreatedBy: $('#CreatedBy').val(),
        ShowOnHome: $('#ShowOnHome').val()
    };

    $.ajax({
        url: "/Admin/BookCategory/Update",
        data: JSON.stringify(categoryObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData(1, "");
            $('#myModal').modal('hide');
            $('#CategoryName').val("");
            $('#SeoTitle').val("");
            $('#MetaKeywords').val("");
            $('#MetaDescription').val("");
            $('#Status').val("");
            $('#CreatedDate').val("");
            $('#CreatedBy').val("");
            $('#ShowOnHome').val("");
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

//function for deleting employee's record
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/BookCategory/Delete/" + ID,
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
function clearTextBox() {
    $('#CategoryID').val("");
    $('#CategoryName').val("");
    $('#SeoTitle').val("");
    $('#MetaKeywords').val("");
    $('#MetaDescription').val("");
    $('#Status').val("");
    $('#CreatedDate').val("");
    $('#CreatedBy').val("");
    $('#ShowOnHome').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#CategoryName').css('border-color', 'lightgrey');
    $('#SeoTitle').css('border-color', 'lightgrey');
    $('#MetaKeywords').css('border-color', 'lightgrey');
    $('#MetaDescription').css('border-color', 'lightgrey');
    $('#Status').css('border-color', 'lightgrey');
    $('#CreatedDate').css('border-color', 'lightgrey');
    $('#CreatedBy').css('border-color', 'lightgrey');
    $('#ShowOnHome').css('border-color', 'lightgrey');
}
//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#CategoryName').val().trim() == "") {
        $('#CategoryName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CategoryName').css('border-color', 'lightgrey');
    }
    return isValid;
}


function SearchClick() {
    var search_value = document.getElementById('inputKeyword').value;
    loadData(1, search_value);
}


