function clearKayit() {
	var choose = "Seçiniz";
    $('#vAciklama').val('');
    $('#dropEgitim').empty().append("<option>" + choose +"</option>");
    $('#dropEgitimEdit').empty().append("<option>" + choose +"</option>");
    $('#dropVardiya').empty().append("<option>" + choose +"</option>");
    $('#dropVardiyaEdit').empty().append("<option>" + choose +"</option>");
    $('#dropArac').empty().append("<option>" + choose +"</option>");
    $('#dropAracEdit').empty().append("<option>" + choose +"</option>");
    $('#dropUnvan').empty().append("<option>" + choose +"</option>");
    $('#dropUnvanEdit').empty().append("<option>" + choose +"</option>");
    $('#dropAmir').empty().append("<option>" + choose +"</option>");
    $('#dropAmirEdit').empty().append("<option>" + choose +"</option>");
}

function getListe() {
    $.ajax({
        url: "/Personel/GetPersonelHtml",
        success: function (data) {
            $('#tableBody').html("");
            $(document).ready(function () {
                $('#tableBody').fadeOut('fast').html(data).fadeIn();
            });
        }
    });
}

function deletePersonel() {
    var id = $(this)[0].getAttribute('data-id');
    $.ajax({
        url: "/Personel/Delete/" + id,
        type: "POST",
        success: function () {
            getListe();
            toastr.error("Personel başarıyla silindi", "Kayıt Silindi");
        },
        failure: function () {
            getListe();
            toastr.warning("Personel silinirken bir sorun oluştu", "Kayıt Silinemedi");
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
    if (data.CalismaDurumu === true) {
        $("#editcalisma").prop('checked', true);
        $("#editcalisma").attr('checked', 'checked');
        $("#editcalisma").attr('checked', true);
    }
    if (data.AmirMi === true) {
        $("#editamir").prop('checked', true);
        $("#editamir").attr('checked', 'checked');
        $("#editamir").attr('checked', true);
    }
    getDropDownUnvanEdit(data.UnvanID);
    getDropDownEgitimEdit(data.EgitimID);
    getDropDownVardiyaEdit(data.VardiyaID);
    getDropDownAracEdit(data.AracID);
    if (data.AmirID != null) {
        getDropDownAmirEdit(data.AmirID);
    } else { getDropDownAmirEdit(null); }
    $("#oldAracId").val(data.AracID);
    $("#peditId").val((data.PersonelID));
    $("#peditName").val((data.PersonelAd));
    $("#peditLastName").val((data.PersonelSoyad));
    $("#peditMaas").val((data.Maas));
    $("#editdogumTarihi").val(dateConvert(data.DogumTarihi));
    $("#editgirisTarihi").val(dateConvert(data.GirisTarihi));
    $("#editcikisTarihi").val(dateConvert(data.CikisTarihi));
    $("#editizinTarihi").val(dateConvert(data.izinTarihi));

}

function getPersonel(id) {
    $.ajax({
        url: "/Personel/Edit/" + id,
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
        url: "/Personel/Create",
        type: "POST",
        data: {
            PersonelAd: $("#pName").val(),
            PersonelSoyAd: $("#pLastName").val(),
            UnvanID: $("#dropUnvan").val(),
            EgitimID: $("#dropEgitim").val(),
            VardiyaID: $("#dropVardiya").val(),
            AracID: $("#dropArac").val(),
            AmirID: $("#dropAmir").val(),
            Maas: $("#pMaas").val(),
            DogumTarihi: $("#dogumTarihi").val(),
            GirisTarihi: $("#girisTarihi").val(),
            CikisTarihi: $("#cikisTarihi").val(),
            izinTarihi: $("#izinTarihi").val(),
            CalismaDurumu: $("#calisma:checked").val(),
            AmirMi: $("#amir:checked").val()
        },
        success: function () {
            getListe();
            toastr.success("Personel başarıyla eklendi", "Kayıt Eklendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Personel kaydedilirken bir sorun oluştu", "Kayıt Eklenemedi");
        }
    });
});

$("#updateBtn").click(function () {
    $.ajax({
        url: "/Personel/Edit/" + $("#oldAracId").val(),
        type: "POST",
        data: {
            PersonelID: $("#peditId").val(),
            PersonelAd: $("#peditName").val(),
            PersonelSoyad: $("#peditLastName").val(),
            UnvanID: $("#dropUnvanEdit").val(),
            EgitimID: $("#dropEgitimEdit").val(),
            VardiyaID: $("#dropVardiyaEdit").val(),
            AracID: $("#dropAracEdit").val(),
            AmirID: $("#dropAmirEdit").val(),
            Maas: $("#peditMaas").val(),
            DogumTarihi: $("#editdogumTarihi").val(),
            GirisTarihi: $("#editgirisTarihi").val(),
            CikisTarihi: $("#editcikisTarihi").val(),
            izinTarihi: $("#editizinTarihi").val(),
            CalismaDurumu: $("#editcalisma").val(),
            AmirMi: $("#editamir:checked").val()
        },
        success: function () {
            getListe();
            toastr.info("Personel başarıyla güncellendi", "Kayıt Güncellendi");
        },
        failure: function () {
            getListe();
            toastr.warning("Personel güncellenirken bir sorun oluştu", "Kayıt Güncellenemedi");
        }
    });
});

//Dropdowns
function getDropDownAmir() {
    var myDropDownList = $('#dropAmir');
    $.ajax({
        type: "GET",
        url: "/Personel/amirDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    var adsoyad = this.PersonelAd + " " + this.PersonelSoyad;
                    myDropDownList.append($('<option></option>').val(this['PersonelID']).html(adsoyad));
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownAmirEdit(id) {
    var myDropDownList = $('#dropAmirEdit');

    $.ajax({
        type: "GET",
        url: "/Personel/amirDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    var adsoyad = this.PersonelAd + " " + this.PersonelSoyad;
                    if (id != null && id === this.Amir.PersonelID) {
                        myDropDownList.append($('<option selected="selected"></option>').val(this['PersonelID']).html(adsoyad).attr('selected', 'selected'));
                    } else {
                        myDropDownList.append($('<option></option>').val(this['PersonelID']).html(adsoyad));
                    }
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownUnvan() {
    var myDropDownList = $('#dropUnvan');

    $.ajax({
        type: "GET",
        url: "/Unvan/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    myDropDownList.append($('<option></option>').val(this['UnvanID']).html(this['UnvanAd']));
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownUnvanEdit(id) {
    var myDropDownList = $('#dropUnvanEdit');
    $('#dropUnvanEdit').empty();

    $.ajax({
        type: "GET",
        url: "/Unvan/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    if (id == this.UnvanID) {
                        myDropDownList.append($('<option selected="selected"></option>').val(this['UnvanID']).html(this['UnvanAd'])
                            .attr('selected', 'selected'));
                    } else { myDropDownList.append($('<option></option>').val(this['UnvanID']).html(this['UnvanAd'])); }

                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownEgitimEdit(id) {
    var myDropDownList = $('#dropEgitimEdit');
    $('#dropEgitimEdit').empty();

    $.ajax({
        type: "GET",
        url: "/Egitim/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    if (id == this.EgitimID) {
                        myDropDownList.append($('<option selected="selected"></option>').val(this['EgitimID']).html(this['EgitimAd'])
                            .attr('selected', 'selected'));
                    } else {
                        myDropDownList.append($('<option></option>').val(this['EgitimID']).html(this['EgitimAd']));
                    }
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownEgitim() {
    var myDropDownList = $('#dropEgitim');

    $.ajax({
        type: "GET",
        url: "/Egitim/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    myDropDownList.append($('<option></option>').val(this['EgitimID']).html(this['EgitimAd']));
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownArac() {
    var myDropDownList = $('#dropArac');

    $.ajax({
        type: "GET",
        url: "/Araclar/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    myDropDownList.append($('<option></option>').val(this['AracID']).html(this['AracPlaka']));
                });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function getDropDownAracEdit(id) {
    var myDropDownList = $('#dropAracEdit');

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

function getDropDownVardiya() {
    var myDropDownList = $('#dropVardiya');

    $.ajax({
        type: "GET",
        url: "/Vardiya/forDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each((data),
                function () {
                    myDropDownList.append($('<option></option>').val(this['VardiyaID']).html(this['VardiyaAd']));
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

getListe();

getDropDownVardiya();
getDropDownEgitim();
getDropDownUnvan();
getDropDownArac();
getDropDownAmir();
