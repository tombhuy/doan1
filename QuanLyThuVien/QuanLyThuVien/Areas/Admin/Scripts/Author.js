//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData(1, "");
});

//Load Data function
function loadData(id, searchString) {
    $.ajax({
        url: "/Admin/Author/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: { "page": id, "searchKey": searchString },
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.data, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.AuthorID + '</td>';
                html += '<td>' + item.AuthorName + '</td>';
                html += '<td>' + item.DescriptionAuthor + '</td>';
                html += '<td>' + item.Alias + '</td>';
                html += '<td><a class="btn btn-default btn-sm" onclick="return getbyID(' + item.AuthorID + ')"><i class="fa fa-edit"></i> Edit</a>  <a class="btn btn-danger btn-sm" onclick="Delele(' + item.AuthorID + ')"><i class="fa fa-trash"></i>Delete</a></td>';
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
    var autObj = {
        AuthorID: $('#AuthorID').val(),
        AuthorName: $('#AuthorName').val(),
        DescriptionAuthor: $('#DescriptionAuthor').val(),
        Alias: $('#Alias').val(),
    };
    $.ajax({
        url: "/Admin/Author/Add",
        data: JSON.stringify(autObj),
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
function getbyID(AutID) {
    $('#AuthorName').css('border-color', 'lightgrey');
    $('#DescriptionAuthor').css('border-color', 'lightgrey');
    $('#Alias').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Admin/Author/GetbyID/" + AutID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#AuthorID').val(result.AuthorID);
            $('#AuthorName').val(result.AuthorName);
            $('#DescriptionAuthor').val(result.DescriptionAuthor);
            $('#Alias').val(result.Alias);

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

    var autObj = {
        AuthorID: $('#AuthorID').val(),
        AuthorName: $('#AuthorName').val(),
        DescriptionAuthor: $('#DescriptionAuthor').val(),
        Alias: $('#Alias').val(),
    };

    $.ajax({
        url: "/Admin/Author/Update",
        data: JSON.stringify(autObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData(1, "");
            $('#myModal').modal('hide');
            $('#AuthorName').val("");
            $('#DescriptionAuthor').val("");
            $('#Alias').val("");
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
            url: "/Admin/Author/Delete/" + ID,
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
    $('#AuthorID').val("");
    $('#AuthorName').val("");
    $('#DescriptionAuthor').val("");
    $('#Alias').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#AuthorName').css('border-color', 'lightgrey');
    $('#DescriptionAuthor').css('border-color', 'lightgrey');
    $('#Alias').css('border-color', 'lightgrey');
}
//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#AuthorName').val().trim() == "") {
        $('#AuthorName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AuthorName').css('border-color', 'lightgrey');
    }
    if ($('#Alias').val().trim() == "") {
        $('#Alias').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Alias').css('border-color', 'lightgrey');
    }
    return isValid;
}


function SearchClick() {
    var search_value = document.getElementById('inputKeyword').value;
    loadData(1, search_value);
}


