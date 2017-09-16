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
            toastr.error("�nvan ba�ar�yla silindi", "Kay�t Silindi");
        },
        failure: function () {
            getListe();
            toastr.warning("�nvan silinirken bir sorun olu�tu", "Kay�t Silinemedi");
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
            toastr.success("�nvan ba�ar�yla eklendi", "Kay�t Eklendi");
        },
        failure: function () {
            getListe();
            toastr.warning("�nvan kaydedilirken bir sorun olu�tu", "Kay�t Eklenemedi");
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
            toastr.info("�nvan ba�ar�yla g�ncellendi", "Kay�t G�ncellendi");
        },
        failure: function () {
            getListe();
            toastr.warning("�nvan g�ncellenirken bir sorun olu�tu", "Kay�t G�ncellenemedi");
        }
    });
});
    getListe();
