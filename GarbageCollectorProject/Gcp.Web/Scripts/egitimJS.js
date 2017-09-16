function clearKayit() {
    $('#eAciklama').val('');
}

function getListe() {
    $.ajax({
        url: "/Egitim/GetEgitimHtml",
        success: function (data) {
            $('#tableBody').html("");
            $(document).ready(function () {
                $('#tableBody').html("");
                $('#tableBody').fadeOut('fast')
                    .html(data)
                    .fadeIn();
            });
        }
    });
}

function deleteEgitim() {
    var id = $(this)[0].getAttribute('data-id');
    $.ajax({
        url: "/Egitim/Delete/" + id,
        type: "POST",
        success: function () {
            getListe();
            toastr.error("Eðitim baþarýyla silindi", "Kayýt Silindi");
        },
        failure: function () {
            getListe();
            toastr.warning("Eðitim silinirken bir sorun oluþtu", "Kayýt Silinemedi");
        }
    });
}

function editDoldur(data) {
    $("#peditId").val((data.EgitimID));
    $("#peditAciklama").val((data.EgitimAd));
}

function getEgitim(id) {

    $.ajax({
        url: "/Egitim/Edit/" + id,
        type: "GET",
        data: { id: id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            editDoldur(data);
        }
    });
}

$("#saveBtn").click(function () {
    $.ajax({
        url: "/Egitim/Create",
        type: "POST",
        data: {
            EgitimAd: $("#eAciklama").val()
        },
        success: function () {
            getListe();
            toastr.success("Eðitim baþarýyla eklendi", "Kayýt Eklendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Eðitim kaydedilirken bir sorun oluþtu", "Kayýt Eklenemedi");
        }
    });
});

$("#updateBtn").click(function (id, data) {
    $.ajax({
        url: "/Egitim/Edit/" + id + data,
        type: "POST",
        data: {
            EgitimID: $("#peditId").val(),
            EgitimAd: $("#peditAciklama").val()
        },
        success: function () {
            getListe();
            toastr.success("Eðitim baþarýyla güncellendi", "Kayýt Güncellendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Eðitim güncellenirken bir sorun oluþtu", "Kayýt Güncellenemedi");
        }
    });
});
    getListe();
