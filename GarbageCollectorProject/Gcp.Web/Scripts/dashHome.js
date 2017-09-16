$(document).ready(function () {
    getIslem();
	getAraclar();
    getKurumlar();
    personelCount();
    giderCount();
    firmaCount();
    aracCount();
    getGunluk();
});

function getDropDownAracEdit(id) {
    var myDropDownList = $('#dropAracEdit');
    $('#dropAracEdit').empty();
    $.ajax({
        type: "GET",
        url: "/Araclar/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    if (id == this.AracID) {
                        myDropDownList.append($('<option selected="selected"></option>').val(this['AracID']).html(this['AracPlaka']).attr('selected', 'selected'));
                    } else {
                        myDropDownList.append($('<option></option>').val(this['AracID']).html(this['AracPlaka']));
                    }
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownVardiyaEdit(id) {
    var myDropDownList = $('#dropVardiyaEdit');
    $('#dropVardiyaEdit').empty();

    $.ajax({
        type: "GET",
        url: "/Vardiya/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    if (id == this.VardiyaID) {
                        myDropDownList.append($('<option selected="selected"></option>').val(this['VardiyaID']).html(this['VardiyaAd']).attr('selected', 'selected'));
                    } else {
                        myDropDownList.append($('<option></option>').val(this['VardiyaID']).html(this['VardiyaAd']));
                    }
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function deleteGecmis() {
    var id = $(this)[0].getAttribute('data-id');
    $.ajax({
        url: "/IslemDetay/Delete/" + id,
        type: "POST",
        success: function () {
            getIslem();
            toastr.error("Detay geçmişten başarıyla silindi", "Kayıt Silindi");
        },
        failure: function () {
            getIslem();
            toastr.warning("Detay silinirken bir sorun oluştu", "Kayıt Silinemedi");
        }
    });
}

function getIslem() {

    $.ajax({
        url: "/IslemDetay/GetIslemDetayHtml",
        datatype: 'json',
        success: function (data) {

            $(document).ready(function () {
                $('#tableIslem').html("");
                $('#tableIslem').fadeOut('fast')
                    .html(data)
                    .fadeIn();
                $('#gecmisTablosu').DataTable({
                    "ordering": false
                });
            });
        }

    });
};

function getGunluk() {

    $.ajax({
        url: "/Personel/GetPersonelIslemHtml",
        datatype: 'json',
        success: function (data) {

            $(document).ready(function () {
                $('#tableGunluk').html("");
                $('#tableGunluk').fadeOut('fast')
                    .html(data)
                    .fadeIn();
                //$('#gunlukTablosu').DataTable({
                //	"ordering": false
                //});
            });
        }
    });
};

function getAraclar() {
	$.ajax({
		url: "/Araclar/GetAraclarListe",
		datatype: 'json',
		success: function(data) {
			$('#tableArac').html("");
			$(document).ready(function() {
				$('#tableArac').html("");
				$('#tableArac').fadeOut('fast')
					.html(data)
					.fadeIn();
			});
		}
	});
};

function getKurumlar() {
    $.ajax({
        url: "/Kurumlar/GetKurumlarListe",
        datatype: 'json',
        success: function (data) {
            $('#tableKurum').html("");
            $(document).ready(function () {
                $('#tableKurum').html("");
                $('#tableKurum').fadeOut('fast')
                    .html(data)
                    .fadeIn();
            });
        }
    });
};

function dateConvert(dateJ) {
    var dateString = dateJ.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = day + "/" + month + "/" + year;
    return date;
}

function editVardiya(id) {

    $.ajax({
        url: "/Personel/Edit/",
        type: "GET",
        data: { id: id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            getDropDownVardiyaEdit(data.VardiyaID);
            $("#oldAracId").val(data.AracID);
            $('#readPersonelVardiya').attr("placeholder", data.PersonelAd + " " + data.PersonelSoyad);

            if (data.CalismaDurumu === true) {
                $("#pcalisma").prop('checked', true);
                $("#pcalisma").attr('checked', 'checked');
                $("#pcalisma").attr('checked', true);
            } else { $("#pcalisma").attr('checked', false); }
            if (data.AmirMi === true) {
                $("#pamir").prop('checked', true);
                $("#pamir").attr('checked', 'checked');
                $("#pamir").attr('checked', true);
            } else { $("#pamir").attr('checked', false); }
            $("#pdropUnvan").val(data.UnvanID);
            $("#pdropArac").val(data.AracID);
            $("#pdropEgitim").val(data.EgitimID);
            $("#pdropAmir").val(data.AmirID);
            $("#pId").val(data.PersonelID);
            $("#pName").val(data.PersonelAd);
            $("#pLastName").val(data.PersonelSoyad);
            $("#pMaas").val(data.Maas);
            $("#pdogumTarihi").val(dateConvert(data.DogumTarihi));
            $("#pgirisTarihi").val(dateConvert(data.GirisTarihi));
            $("#pcikisTarihi").val(dateConvert(data.CikisTarihi));
            $("#pizinTarihi").val(dateConvert(data.izinTarihi));
        }
    });
}

function editArac(id) {

    $.ajax({
        url: "/Personel/Edit",
        type: "GET",
        data: { id: id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
	        
            getDropDownAracEdit(data.AracID);
            $('#readPersonelArac').attr("placeholder", data.PersonelAd + " " + data.PersonelSoyad);

            if (data.CalismaDurumu === true) {
                $("#pacalisma").prop('checked', true);
                $("#pacalisma").attr('checked', 'checked');
                $("#pacalisma").attr('checked', true);
            } else { $("#pacalisma").attr('checked', false); }
            if (data.AmirMi === true) {
                $("#paamir").prop('checked', true);
                $("#paamir").attr('checked', 'checked');
                $("#paamir").attr('checked', true);
            } else { $("#paamir").attr('checked', false); }
            $("#padropUnvan").val(data.UnvanID);
            $("#padropVardiya").val(data.VardiyaID);
            $("#padropEgitim").val(data.EgitimID);
            $("#padropAmir").val(data.AmirID);
            $("#paId").val(data.PersonelID);
            $("#paName").val(data.PersonelAd);
            $("#paLastName").val(data.PersonelSoyad);
            $("#paMaas").val(data.Maas);
            $("#padogumTarihi").val(dateConvert(data.DogumTarihi));
            $("#pagirisTarihi").val(dateConvert(data.GirisTarihi));
            $("#pacikisTarihi").val(dateConvert(data.CikisTarihi));
            $("#paizinTarihi").val(dateConvert(data.izinTarihi));
        }
    });
}

$("#saveVardiya").click(function () {
    $.ajax({
        url: "/Personel/Edit",
        type: "POST",
        data: {
            oldAracId: $("#oldAracId").val(),
            PersonelID: $("#pId").val(),
            PersonelAd: $("#pName").val(),
            PersonelSoyAd: $("#pLastName").val(),
            UnvanID: $("#pdropUnvan").val(),
            EgitimID: $("#pdropEgitim").val(),
            VardiyaID: $("#dropVardiyaEdit").val(),
            AracID: $("#pdropArac").val(),
            AmirID: $("#pdropAmir").val(),
            Maas: $("#pMaas").val(),
            DogumTarihi: $("#pdogumTarihi").val(),
            GirisTarihi: $("#pgirisTarihi").val(),
            CikisTarihi: $("#pcikisTarihi").val(),
            izinTarihi: $("#pizinTarihi").val(),
            CalismaDurumu: $("#pcalisma:checked").val(),
            AmirMi: $("#pamir:checked").val()
        },
        success: function () {
            getGunluk();
            toastr.success("Personel vardiyası başarıyla eklendi", "Kayıt Eklendi");
        },
        failure: function () {
            getGunluk();
            toastr.warning("Personel vardiyası kaydedilirken bir sorun oluştu", "Kayıt Eklenemedi");
        }
    });
});

$("#saveArac").click(function () {
    $.ajax({
        url: "/Personel/Edit",
        type: "POST",
        data: {
            oldAracId: $("#oldAracId").val(),
            PersonelID: $("#paId").val(),
            PersonelAd: $("#paName").val(),
            PersonelSoyAd: $("#paLastName").val(),
            UnvanID: $("#padropUnvan").val(),
            EgitimID: $("#padropEgitim").val(),
            VardiyaID: $("#padropVardiya").val(),
            AracID: $("#dropAracEdit").val(),
            AmirID: $("#padropAmir").val(),
            Maas: $("#paMaas").val(),
            DogumTarihi: $("#padogumTarihi").val(),
            GirisTarihi: $("#pagirisTarihi").val(),
            CikisTarihi: $("#pacikisTarihi").val(),
            izinTarihi: $("#paizinTarihi").val(),
            CalismaDurumu: $("#pacalisma:checked").val(),
            AmirMi: $("#paamir:checked").val()
        },
        success: function () {
            getGunluk();
            toastr.success("Personel aracı başarıyla eklendi", "Kayıt Eklendi");
        },
        failure: function () {
            getGunluk();
            toastr.warning("Personel aracı kaydedilirken bir sorun oluştu", "Kayıt Eklenemedi");
        }
    });
});

function cData(divid) {
    $(divid).each(function () {
        var $this = $(this),
            countTo = $this.attr('data-count');

        $({ countNum: $this.text() }).animate({
            countNum: countTo
        },
            {
                duration: 800,
                easing: 'linear',
                step: function () {
                    $this.text(Math.floor(this.countNum));
                },
                complete: function () {
                    $this.text(this.countNum);
                }
            });
    });
}

function personelCount() {
    $.ajax({
        url: "/Personel/Count",
        success: (data) => {
            if (data === 0 || data === null) {
                $('#countpersonel').attr("data-count", 0);
            }
            $('#countpersonel').attr("data-count", data);
            cData('#countpersonel');
        }
    });

};

function giderCount() {
    $.ajax({
        url: "/Personel/aylikGider",
        success: (data) => {
            if (data === 0 || data === null) {
                $('#countaylik').attr("data-count", 0);
            }
            $('#countaylik').attr("data-count", data);
            cData('#countaylik');
        }
    });

};

function firmaCount() {
    $.ajax({
        url: "/Kurumlar/Count",
        success: (data) => {
            if (data === 0 || data === null) {
                $('#countfirma').attr("data-count", 0);
            }
            $('#countfirma').attr("data-count", data);
            cData('#countfirma');
        }
    });

};

function aracCount() {
    $.ajax({
        url: "/Araclar/Count",
        success: (data) => {
            if (data === 0 || data === null) {
                $('#countarac').attr("data-count", 0);
            }
            $('#countarac').attr("data-count", data);
            cData('#countarac');
        }
    });

};

