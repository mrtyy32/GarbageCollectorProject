function clearKayit() {
    $('#pName').val('');
}

function getListe() {
    $.ajax({
        url: "/Model/GetModelHtml",
        success: function (data) {
            $('#tableBody').html("");
            $(document).ready(function () {
                $('#tableBody').fadeOut('fast')
                    .html(data)
                    .fadeIn();
            });
        }
    });
}

function deleteModel() {
    var id = $(this)[0].getAttribute('data-id');
    $.ajax({
        url: "/Model/Delete/" + id,
        type: "POST",
        success: function () {
            getListe();
            toastr.error("Model baþarýyla silindi", "Kayýt Silindi");
        },
        failure: function () {
            getListe();
            toastr.warning("Model silinirken bir sorun oluþtu", "Kayýt Silinemedi");
        }
    });
}

function editDoldur(data) {
    $("#peditId").val((data.ModelID));
    $("#peditName").val((data.ModelAd));
}

function getModel(id) {
    $.ajax({
        url: "/Model/Edit/" + id,
        type: "GET",
        data: {id: id},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            editDoldur(data);
            getDropDownMarkaEdit(data.MarkaID);
        }
    });
}

function getDropDownMarka() {
    var myDropDownList = $('#dropMarka');
    $.ajax({
        type: "GET",
        url: "/Marka/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    myDropDownList.append($('<option></option>').val(this['MarkaID']).html(this['MarkaAd']));
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownMarkaEdit(id) {
    $('#dropMarkaEdit').empty();
    $("#peditName").empty();
    var myDropDownList = $('#dropMarkaEdit');
    $.ajax({
        type: "GET",
        url: "/Marka/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    if (id == this.MarkaID) {
                        myDropDownList.append($('<option selected="selected"></option>').val(this['MarkaID']).html(this['MarkaAd']).attr('selected', 'selected'));
                    }else{
                        myDropDownList.append($('<option></option>').val(this['MarkaID']).html(this['MarkaAd']));
                    }
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

$("#saveBtn").click(function () {
    $.ajax({
        url: "/Model/Create",
        type: "POST",
        data: {
            ModelAd: $("#pName").val(),
            MarkaID: $("#dropMarka").val()
        },
        success: function () {
            getListe();
            toastr.success("Model baþarýyla eklendi", "Kayýt Eklendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Model kaydedilirken bir sorun oluþtu", "Kayýt Eklenemedi");
        }
    });
});

$("#updateBtn").click(function (id, data) {
    $.ajax({
        url: "/Model/Edit/" + id + data,
        type: "POST",
        data: {
            ModelID: $("#peditId").val(),
            ModelAd: $("#peditName").val(),
            MarkaID: $("#dropMarkaEdit").val()
        },
        success: function () {
            getListe();
            toastr.info("Model baþarýyla güncellendi", "Kayýt Güncellendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Model güncellenirken bir sorun oluþtu", "Kayýt Güncellenemedi");
        }
    });
});
getListe();
getDropDownMarka();
