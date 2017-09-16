function clearKayit() {
    $('#eAciklama').val('');
};

function getListe() {
    $.ajax({
        url: "/Unvan/GetUnvanHtml",
        success: function (data) {
            $(document).ready(function () {
                $('#tableBody').html("");
                $('#tableBody').fadeOut('fast')
                    .html(data)
                    .fadeIn();
            });
        }
    });
}

function deleteUnvan() {
    var id = $(this)[0].getAttribute('data-id');
    $.ajax({
        url: "/Unvan/Delete/" + id,
        type: "POST",
        success: function () {
            getListe();
            toastr.error("Ünvan baþarýyla silindi", "Kayýt Silindi");
        },
        failure: function () {
            getListe();
            toastr.warning("Ünvan silinirken bir sorun oluþtu", "Kayýt Silinemedi");
        }
    });
}

function editDoldur(data) {
    $("#peditId").val((data.UnvanID));
    $("#peditAciklama").val((data.UnvanAd));
}

function getUnvan(id) {
    $.ajax({
        url: "/Unvan/Edit/" + id,
        type: "GET",
        data: {id: id},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            editDoldur(data);
        }
    });
}

$("#saveBtn").click(function () {
    $.ajax({
        url: "/Unvan/Create",
        type: "POST",
        data: {
            UnvanAd: $("#uAciklama").val()
        },
        success: function () {
            getListe();
            toastr.success("Ünvan baþarýyla eklendi", "Kayýt Eklendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Ünvan kaydedilirken bir sorun oluþtu", "Kayýt Eklenemedi");
        }
    });
});
$("#updateBtn").click(function (id, data) {
    $.ajax({
        url: "/Unvan/Edit/" + id + data,
        type: "POST",
        data: {
            UnvanID: $("#peditId").val(),
            UnvanAd: $("#peditAciklama").val()
        },
        success: function () {
            getListe();
            toastr.info("Ünvan baþarýyla güncellendi", "Kayýt Güncellendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Ünvan güncellenirken bir sorun oluþtu", "Kayýt Güncellenemedi");
        }
    });
});
    getListe();
