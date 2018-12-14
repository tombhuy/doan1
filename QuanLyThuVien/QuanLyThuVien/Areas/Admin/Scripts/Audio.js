//Add Data Function 
function AddAudio() {
    //var res = validateAudio();
    //if (res == false) {
    //    return false;
    //}

    //var nameebookselected = document.getElementById("ddlNameBook");

    //var audioObj = {
    //    BookID: nameebookselected[nameebookselected.selectedIndex].value,
    //    AudioID: $('#AudioID').val(),
    //    AudioName: $('#AudioName').val(),
    //    Alias: $('#Alias').val(),
    //};

    var nameebookselected = document.getElementById("ddlNameBook");
    var bookid=nameebookselected[nameebookselected.selectedIndex].value;
    var model = new FormData();
    model.append("File", $('#fileupload')[0].files[0]);
    model.append("BookID",bookid);
    model.append("AudioID", $('#AudioID').val());
    model.append("AudioName", $('#AudioName').val());
    model.append("Alias", $('#Alias').val());
    $.ajax({
        url: '/Admin/Audio/UploadAudio2',
        type: "POST",
        data: model,
        contentType: false,
        processData: false,
        success : function (result) {
            $('#modalAddOrEditAudioBook').modal('hide');
        },
        error: function (result) {

        }
    });
}

//Function for getting the Data Based upon Employee ID
function getbyID(UsrID) {
    $('#UserName').css('border-color', 'lightgrey');
    $('#Password').css('border-color', 'lightgrey');
    $('#Name').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#Phone').css('border-color', 'lightgrey');
    $('#CreatedDate').css('border-color', 'lightgrey');
    $('#Status').css('border-color', 'lightgrey');
    $('#Type').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Admin/User/GetbyID/" + UsrID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#UserID').val(result.UserID);
            $('#UserName').val(result.UserName);
            $('#Password').val(result.Password);
            $('#Name').val(result.Name);
            $('#Address').val(result.Address);
            $('#Email').val(result.Email);
            $('#Phone').val(result.Phone);
            $('#CreatedDate').val(result.CreatedDate);
            $('#Status').val(result.Status);
            $('#Type').val(result.Type);

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
function UpdateAudio() {
    var res = validateAudio();
    if (res == false) {
        return false;
    }

    var audioObj = {
        UserID: $('#UserID').val(),
        UserName: $('#UserName').val(),
        Password: $('#Password').val(),
        Name: $('#Name').val(),
        Address: $('#Address').val(),
        Email: $('#Email').val(),
        Phone: $('#Phone').val(),
        CreatedDate: $('#CreatedDate').val(),
        Status: $('#Status').val(),
        Type: $('#Type').val()
    };

    $.ajax({
        url: "/Admin/User/Update",
        data: JSON.stringify(usrObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData(1, "");
            $('#myModal').modal('hide');
            $('#UserName').val("");
            $('#Password').val("");
            $('#Name').val("");
            $('#Address').val("");
            $('#Email').val("");
            $('#Phone').val("");
            $('#CreatedDate').val("");
            $('#Status').val("");
            $('#Type').val("");
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
            url: "/Admin/User/Delete/" + ID,
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
function clearTextBoxAddAudioBook() {
    $('#AudioID').val("");
    $('#AudioName').val("");
    $('#Alias').val("");
    $('#AudioID').css('border-color', 'lightgrey');
    $('#AudioName').css('border-color', 'lightgrey');
    $('#Alias').css('border-color', 'lightgrey');
}
//Valdidation using jquery
function validateAudio() {
    var isValid = true;
    if ($('#AudioID').val().trim() == "") {
        $('#AudioID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AudioID').css('border-color', 'lightgrey');
    }

    if ($('#Alias').val().trim() == "") {
        $('#Alias').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Alias').css('border-color', 'lightgrey');
    }

    if ($('#AudioName').val().trim() == "") {
        $('#AudioName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AudioName').css('border-color', 'lightgrey');
    }
    return isValid;
}


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



function getListEbook() {
    var url = "/Admin/Audio/GetListBook";
    var listtypeebookname = $('#ddlNameBook');
    $.getJSON(url, function (response) {
        listtypeebookname.empty();
        $.each(response, function (index, item) {
            listtypeebookname.append($('<option></option>').text(item.nameBook).val(item.idBook));
        });
    });
}
