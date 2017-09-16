function clearKayit() {
    $('#vAciklama').val('');
    $('#baslamaDrop').empty();
    $('#baslamaDropEdit').empty();
	$('#bitirmeDrop').empty();
    $('#bitirmeDropEdit').empty();

}

function getListe() {
    $.ajax({
        url: "/Vardiya/GetVardiyaHtml",
        success: function (data) {
            $('#tableBody').html("");
	        $(document).ready(function() {
		        $('#tableBody').fadeOut('fast')
			        .html(data)
			        .fadeIn();
	        });
        }
    });
}

function deleteVardiya() {
    var id = $(this)[0].getAttribute('data-id');
    $.ajax({
        url: "/Vardiya/Delete/" + id,
        type: "POST",
        success: function () {
            getListe();
            toastr.error("Vardiya baþarýyla silindi", "Kayýt Silindi");
        },
        failure: function () {
            getListe();
            toastr.warning("Vardiya silinirken bir sorun oluþtu", "Kayýt Silinemedi");
        }
    });
}

function editDoldur(data) {
    $("#peditId").val((data.VardiyaID));
    $("#peditAciklama").val((data.VardiyaAd));
    vardiyaSaatleriEdit(data);
}

function getVardiya(id) {
	clearKayit();
    $.ajax({
        url: "/Vardiya/Edit/" + id,
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
        url: "/Vardiya/Create",
        type: "POST",
        data: {
            VardiyaAd: $("#vAciklama").val(),
            BaslamaSaati: $("#baslamaDrop").val(),
            BitirmeSaati: $("#bitirmeDrop").val()
        },
        success: function () {
            getListe();
            toastr.success("Vardiya baþarýyla eklendi", "Kayýt Eklendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Vardiya kaydedilirken bir sorun oluþtu", "Kayýt Eklenemedi");
        }
    });
});

$("#updateBtn").click(function (id, data) {

    $.ajax({
        url: "/Vardiya/Edit/" + id + data,
        type: "POST",
        data: {
            VardiyaID: $("#peditId").val(),
            Aciklama: $("#peditAciklama").val(),
            BaslamaSaati: $("#veditBaslama").val(),
            BitirmeSaati: $("#veditBitirme").val()
        },
        success: function () {
            getListe();
            toastr.info("Vardiya baþarýyla güncellendi", "Kayýt Güncellendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Vardiya güncellenirken bir sorun oluþtu", "Kayýt Güncellenemedi");
        }
    });
});

//Dropdowns
function vardiyaSaatleri() {
    var myDropDownList = $('#baslamaDrop');
    $.ajax({
        type: "GET",
        url: "/Vardiya/Hours",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            for (var i = 0; i < 24; i++) {
                myDropDownList.append($('<option></option>').val(data[i]).html(data[i]));
            }

        },
        failure: function (response) {
            alert(response.d);
        }
    });

    var myDropDownList2 = $('#bitirmeDrop');
    $.ajax({
        type: "GET",
        url: "/Vardiya/Hours",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            for (var i = 0; i < 24; i++) {
                myDropDownList2.append($('<option></option>').val(data[i]).html(data[i]));
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function vardiyaSaatleriEdit(vardiya) {
    var myDropDownList = $('#baslamaDropEdit');
    $.ajax({
        type: "GET",
        url: "/Vardiya/Hours",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            for (var i = 0; i < 24; i++) {
                if (vardiya.BaslamaSaati === data[i]) {
                    myDropDownList.append($('<option></option>').val(data[i]).html(data[i]).attr('selected', 'selected'));
                } else { myDropDownList.append($('<option></option>').val(data[i]).html(data[i]));}
                
            }
           },
        failure: function (response) {
            alert(response.d);
        }
    });

    var myDropDownList2 = $('#bitirmeDropEdit');
    $.ajax({
        type: "GET",
        url: "/Vardiya/Hours",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            for (var i = 0; i < 24; i++) {
                if (vardiya.BitirmeSaati === data[i]) {
                    myDropDownList2.append($('<option></option>').val(data[i]).html(data[i]).attr('selected', 'selected'));
                } else { myDropDownList2.append($('<option></option>').val(data[i]).html(data[i]));}
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}
getListe();
vardiyaSaatleri();
