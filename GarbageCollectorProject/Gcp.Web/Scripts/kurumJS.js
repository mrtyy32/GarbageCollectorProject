function clearKayit() {
    $('#kName').val('');
    $('#kAdress').val('');
    $('#kMail').val('');
    $('#kNo').val('');
    $('#kDaire').val('');
    $('#kDaireNo').val('');
    $('#kvAdress').val('');
}

function getListe() {
    $.ajax({
        url: "/Kurumlar/GetKurumlarHtml",
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

function deleteKurum() {
    var id = $(this)[0].getAttribute('data-id');
    $.ajax({
        url: "/Kurumlar/Delete/" + id,
        type: "POST",
        success: function () {
            getListe();
            toastr.error("Kurum baþarýyla silindi", "Kayýt Silindi");
        },
        failure: function () {
            getListe();
            toastr.warning("Kurum silinirken bir sorun oluþtu", "Kayýt Silinemedi");
        }
    });
}

function editDoldur(data) {

    $("#keditName").val(data.KurumIsmi);
    $("#keditId").val(data.KurumID);
    $("#keditAdress").val(data.KurumAdresi);
    $("#keditKisi").val(data.TemsilciKisi);
    $("#keditNo").val(data.TemsilciKisiNo);
    $('#keditMail').val(data.TemsilciKisiEmail);
    $("#keditDaire").val(data.VergiDairesi);
    $("#keditDaireNo").val(data.VergiNo);
    $("#kveditAdress").val(data.VergiDairesiAdresi);
    if (data.CalismaDurumu === true) {
        $("#editcalisma").prop('checked', true);
        $("#editcalisma").attr('checked', 'checked');
        $("#editcalisma").attr('checked', true);
    } else {
        $("#editcalisma").prop("checked", false);
    }
}

function getKurum(id) {
    $.ajax({
        url: "/Kurumlar/Edit",
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
        url: "/Kurumlar/Create",
        type: "POST",
        data: {
            KurumIsmi: $("#kName").val(),
            KurumAdresi: $("#kAdress").val(),
            TemsilciKisi: $("#kKisi").val(),
            TemsilciKisiNo: $("#kNo").val(),
            TemsilciKisiEmail: $("#kMail").val(),
            VergiDairesi: $("#kDaire").val(),
            VergiNo: $("#kDaireNo").val(),
            VergiDairesiAdresi: $("#kvAdress").val(),
            CalismaDurumu: $("#calisma:checked").val()
        },
        success: function () {
            getListe();
            toastr.success("Kurum baþarýyla eklendi", "Kayýt Eklendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Kurum kaydedilirken bir sorun oluþtu", "Kayýt Eklenemedi");
        }
    });
});


$("#updateBtn").click(function () {
    $.ajax({
        url: "/Kurumlar/Edit",
        type: "POST",
        data: {
            KurumID: $("#keditId").val(),
            KurumIsmi: $("#keditName").val(),
            KurumAdresi: $("#keditAdress").val(),
            TemsilciKisi: $("#keditKisi").val(),
            TemsilciKisiNo: $("#keditNo").val(),
            TemsilciKisiEmail: $("#keditMail").val(),
            CalismaDurumu: $("#editcalisma:checked").val(),
            VergiDairesi: $("#keditDaire").val(),
            VergiNo: $("#keditDaireNo").val(),
            VergiDairesiAdresi: $("#kveditAdress").val()
        },
        success: function () {
            getListe();
            toastr.info("Kurum baþarýyla güncellendi", "Kayýt Güncellendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Kurum güncellenirken bir sorun oluþtu", "Kayýt Güncellenemedi");
        }
    });
});

    getListe();
