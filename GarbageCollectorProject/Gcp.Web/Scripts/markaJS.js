function clearKayit() {
    $('#pName').val('');
}


function getListe() {
    $.ajax({
        url: "/Marka/GetMarkaHtml",
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

function deleteMarka() {
    var id = $(this)[0].getAttribute('data-id');
    $.ajax({
        url: "/Marka/Delete/" + id,
        type: "POST",
        success: function () {
            getListe();
            toastr.error("Marka baþarýyla silindi", "Kayýt Silindi");
        },
        failure: function () {
            getListe();
            toastr.warning("Marka silinirken bir sorun oluþtu", "Kayýt Silinemedi");
        }
    });
}

function editDoldur(data) {
    $("#peditId").val((data.MarkaID));
    $("#peditName").val((data.MarkaAd));
}

function getMarka(id) {
    $.ajax({
        url: "/Marka/Edit/" + id,
        type: "GET",
        data: {id: id},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            editDoldur(data);
        }
    });
}

$("#saveBtn").click(function (e) {
    $.ajax({
        url: "/Marka/Create",
        type: "POST",
        data: {
            MarkaAd: $("#pName").val()
        },
        success: function () {
            getListe();
            toastr.success("Marka baþarýyla eklendi", "Kayýt Eklendi");
            e.preventDefault();
        },
        failure: function () {
            getListe();
            toastr.warning("Marka kaydedilirken bir sorun oluþtu", "Kayýt Eklenemedi");
        }
    });
});

$("#updateBtn").click(function (id, data) {
    $.ajax({
        url: "/Marka/Edit/" + id + data,
        type: "POST",
        data: {
            MarkaID: $("#peditId").val(),
            MarkaAd: $("#peditName").val()
        },
        success: function () {
            getListe();
            toastr.info("Marka baþarýyla güncellendi", "Kayýt Güncellendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Marka güncellenirken bir sorun oluþtu", "Kayýt Güncellenemedi");
        }
    });
});
    getListe();
