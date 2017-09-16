function clearKayit() {
    $('#dropMarka').empty().append('<option>Seçiniz</option>');
    $('#dropModel').empty().append('<option>Seçiniz</option>');
    $('#dropModelEdit').empty().append('<option>Seçiniz</option>');
    $('#dropMarkaEdit').empty().append('<option>Seçiniz</option>');

}

var getListe = function () {
    $.ajax({
        url: "/Araclar/GetAraclarHtml",
        datatype: 'json',
        success: function (data) {
            $('#tableBody').html("");
            $(document).ready(function () {
                $('#tableBody').html("");
                $('#tableBody').fadeOut('fast')
                    .html(data)
                    .fadeIn();
            });
        }
    })
};

function deleteArac() {
    var id = $(this)[0].getAttribute('data-id');
    $.ajax({
        url: "/Araclar/Delete/" + id,
        type: "POST",
        success: function () {
            getListe();
            
            toastr.error("Araç başarıyla silindi", "Kayıt Silindi");
        },
        failure:function() {
            getListe();
            toastr.warning("Araç silinirken bir sorun oluştu", "Kayıt Silinemedi");
        }
    });
}

function dateConvert(dateJ) {
    var dateString = dateJ.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = day + "/" + month + "/" + year;
    return date;
}

function editDoldur(data) {
    clearKayit();
    getDropDownMarkaEdit(data.MarkaID, data.ModelID);
    getDropDownModelEdit(data.MarkaID, data.ModelID);
    $("#peditPlaka").val((data.AracPlaka));
    $("#peditId").val((data.AracID));
    if (data.AktifMi === true) {
        $("#editaktif").prop('checked', true);
        $("#editaktif").attr('checked', 'checked');
        $("#editaktif").attr('checked', true);
    }
    else {
        $("#editaktif").prop("checked", false);
    }
}

var getArac = function (id) {
	$.ajax({
		url: "/Araclar/Edit/" + id,
		type: "GET",
		data: { id: id },
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function(data) {
			editDoldur(data);
		}
	});
};


function getAracDetay(id) {
    $.ajax({
        url: "/Araclar/DetayDokumHtml",
        data: {aracId: id},
        success: function (data) {
            $("#tableDetay").html(data);
        }
    });
}

function getAracGecmis(id) {
    $.ajax({
        url: "/Araclar/GecmisDokumHtml",
        data: { aracId: id },
        success: function (data) {
            $("#tableGecmis").html(data);
        }
    });
}

$("#saveBtn").click(function () {

    $.ajax({
        url: "/Araclar/Create",
        type: "POST",
        data: {
            AracPlaka: $("#pPlaka").val(),
            MarkaID: $("#dropMarka").val(),
            ModelID: $("#dropModel").val(),
            AktifMi: $("#aktif:checked").val()
        },
        success: function () {
            getListe();
          
            toastr.success("Araç başarıyla eklendi", "Kayıt Eklendi");
        },
        failure: function() {
            getListe();
            toastr.warning("Araç kaydedilirken bir sorun oluştu", "Kayıt Eklenemedi");
        }
    })
});


$('#detayKayit').on('show.bs.modal',
    function (e) {
        var id = $(e.relatedTarget).data('book-id');
        $(e.currentTarget).find('input[id="aracId"]').val(id);
    });

$("#savedetayBtn").click(function () {
    $.ajax({
        url: "/Araclar/DetayCreate",
        type: "POST",
        data: {
            AracID: $("#aracId").val(),
            AlinisTarihi: $("#alinisTarihi").val(),
            BakimTarihi: $("#bakimTarihi").val(),
            MuayeneTarihi: $("#muayeneTarihi").val()
        },
        success: function () {
            getListe();

            toastr.success("Araç detayı başarıyla eklendi", "Kayıt Eklendi");
        },
        failure: function() {
            getListe();
            toastr.warning("Araç detay kaydedilirken bir sorun oluştu", "Kayıt Eklenemedi");
        }
    })
});


$("#updateBtn").click(function () {

	$.ajax({
		url: "/Araclar/Edit",
		type: "POST",
		data: {
			AracID: $('#peditId').val(),
			AracPlaka: $("#peditPlaka").val(),
			MarkaID: $("#dropMarkaEdit").val(),
			ModelID: $("#dropModelEdit").val(),
			AktifMi: $('#editaktif:checked').val()
		},
		success: function() {
			getListe();

			toastr.info("Araç başarıyla güncellendi", "Kayıt Güncellendi");
		},
		failure: function() {
			getListe();
			toastr.warning("Araç güncellenirken bir sorun oluştu", "Kayıt Güncellenemedi");
		}
	});
});

//Dropdowns

$('#dropMarka').on('change',
    function () {
        $('#dropModel').empty();
        var markaId = $(this).val();
        getDropDownModel(markaId);
    });

$('#dropMarkaEdit').on('change',
    function () {
        $('#dropModelEdit').children().remove();
        var markaId = $(this).val();
        var myDropDownList = $('#dropModelEdit').empty();

        $.ajax({
            type: "GET",
            url: "/Model/forDropDown?markaId=" + markaId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $.each((data),
                    function () {
                        myDropDownList.append($('<option></option>').val(this['ModelID']).html(this['ModelAd']));
                    });
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    });

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

function getDropDownMarkaEdit(id, id2) {
    var myDropDownList = $('#dropMarkaEdit');
    myDropDownList.empty();
    $.ajax({
        type: "GET",
        url: "/Marka/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    if (id === this.MarkaID) {
                        myDropDownList.append($('<option selected="selected"></option>').val(this['MarkaID']).html(this['MarkaAd'])
                            .attr('selected', 'selected'));
                    }else{
                        myDropDownList.append($('<option></option>').val(this['MarkaID']).html(this['MarkaAd']));
                    }
                });
            getDropDownModelEdit(this.MarkaID, id2);
        },
        failure: function (response) {
            alert(response.d);
        }

    });
}

function getDropDownModelEdit(id, id2) {
    var myDropDownList = $('#dropModelEdit').empty();

    $.ajax({
        type: "GET",
        url: "/Model/forDropDown?markaId=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    if (id2 === this.ModelID) {
                        myDropDownList.append($('<option selected="selected"></option>').val(this['ModelID']).html(this['ModelAd'])
                            .attr('selected', 'selected'));
                    }else{
                        myDropDownList.append($('<option></option>').val(this['ModelID']).html(this['ModelAd']));
                    }
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownModel(id) {
    var myDropDownList = $('#dropModel');

    $.ajax({
        type: "GET",
        url: "/Model/forDropDown?markaID=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    myDropDownList.append($('<option></option>').val(this['ModelID']).html(this['ModelAd']));
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

getListe();
getDropDownMarka();