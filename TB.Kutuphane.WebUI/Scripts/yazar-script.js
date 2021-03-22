
// yazar ekle
$(document).on("click", "#yazarEkle", async function () {
    const { value: formValues } = await Swal.fire({
        title: "Yazar Ekle",
        html: '<input id="yazarAdi" class="swal2-input" placeholder="yazar adı">' + '<input id="yazarSoyadi" class="swal2-input" placeholder="yazar soyadı">',
        focusConfirm: false
    });
    if (formValues) {
        var getYazarAdi = $("#yazarAdi").val();
        var getYazarSoyadi = $("#yazarSoyadi").val();
        if (getYazarAdi != null && getYazarAdi != "" && getYazarSoyadi != null && getYazarSoyadi != "") {
            $.ajax({
                type: "POST",
                url: "/Yazar/Ekle",
                data: { "yazarAdi": getYazarAdi, "yazarSoyadi": getYazarSoyadi },
                dataType: "json",
                success: function (response) {
                    var yazarId = response.result.Id;
                    var yazarAdi = "<td>" + response.result.Ad + "</td>";
                    var yazarSoyadi = "<td>" + response.result.Soyad + "</td>";
                    var kitapAdet = "<td>0</td>"
                    var buttonGuncelle = "<td><button class='yazarGuncelle btn waves-effect waves-light btn-warning btn-sm text-dark' value='" + yazarId + "'><i class='fa fa-edit'></i> Güncelle</button></td>";
                    var buttonSil = "<td><button class='yazarSil btn waves-effect waves-light btn-danger btn-sm' value='" + yazarId + "'><i class='fa fa-trash'></i> Sil</button></td>";
                    $("#example tbody").append("<tr>" + yazarAdi + yazarSoyadi + kitapAdet + buttonGuncelle + buttonSil + "</tr>");
                    Swal.fire({
                        type: "success",
                        icon: "success",
                        title: "Yazar Ekleme",
                        text: response.result.Ad + " " + response.result.Soyad + " başarılı bir şekilde eklendi."
                    });
                }, error: function () {
                    Swal.fire({
                        type: "error",
                        icon: "error",
                        title: "Hata",
                        text: "Bir sorun ile karşılaşıldı."
                    });
                }
            });
        } else {
            Swal.fire({
                type: "warning",
                icon: "warning",
                title: "Yazar Ekleme",
                text: "Yazar alanları boş geçilemez."
            });
        }
    }
});

// yazar guncelle
$(document).on("click", ".yazarGuncelle", async function () {
    var getYazarId = $(this).val();
    var getYazarAdiTd = $(this).parent("td").parent("tr").find("td:first");
    var getYazarSoyadiTd = $(this).parent("td").parent("tr").find("td:nth-child(2)");
    var getYazarAdiTdHtml = getYazarAdiTd.html();
    var getYazarSoyadiTdHtml = getYazarSoyadiTd.html();
    const { value: formValues } = await Swal.fire({
        title: "Kategori Güncelle",
        html: '<input id="yazarAdi" class="swal2-input" value="' + getYazarAdiTdHtml + '">' +
            '<input id="yazarSoyadi" class="swal2-input" value="' + getYazarSoyadiTdHtml + '">'
    });
    if (formValues) {
        getYazarAdiTdHtml = $("#yazarAdi").val();
        getYazarSoyadiTdHtml = $("#yazarSoyadi").val();
        if (getYazarAdiTdHtml != null && getYazarAdiTdHtml != "" && getYazarSoyadiTdHtml != null && getYazarSoyadiTdHtml != "") {
            $.ajax({
                type: "POST",
                url: "/Yazar/Guncelle",
                data: { "yazarId": getYazarId, "yazarAdi": getYazarAdiTdHtml, "yazarSoyadi": getYazarSoyadiTdHtml },
                dataType: "json",
                success: function (response) {
                    if (response == "1") {
                        getYazarAdiTd.html(getYazarAdiTdHtml);
                        getYazarSoyadiTd.html(getYazarSoyadiTdHtml);
                        Swal.fire({
                            type: "success",
                            icon: "success",
                            title: "Güncelleme",
                            text: "Kategori başarılı bir şekilde güncellendi."
                        });
                    } else {
                        Swal.fire({
                            type: "warning",
                            icon: "warning",
                            title: "Hata",
                            text: "Kategori Güncellenemedi."
                        });
                    }
                }, error: function () {
                    Swal.fire({
                        type: "error",
                        icon: "error",
                        title: "Hata",
                        text: "Bir sorun ile karşılaşıldı."
                    });
                }
            });
        } else {
            Swal.fire({
                type: "warning",
                icon: "warning",
                title: "Uyarı",
                text: "Yazar alanları boş geçilemez."
            });
        }
    }
});

// yazar silme
$(document).on("click", ".yazarSil", async function () {
    var tr = $(this).parent("td").parent("tr");
    var getYazarId = $(this).val();
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false,
        confirmButton: "Tamam"
    });

    swalWithBootstrapButtons.fire({
        title: "Silmek istediğinize emin misiniz?",
        text: "Bunu geri alamayacaksın!",
        icon: "info",
        showCancelButton: true,
        confirmButtonText: "Sil !",
        cancelButtonText: "İptal !",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Yazar/Sil",
                data: { "yazarId": getYazarId },
                dataType: "json",
                success: function (response) {
                    if (response == "1") {
                        tr.remove();
                        Swal.fire({
                            type: "success",
                            icon: "success",
                            title: "Yazar Silme",
                            text: "Yazar başarılı bir şekilde silindi."
                        });
                    } else {
                        Swal.fire({
                            type: "error",
                            icon: "error",
                            title: "Kategori Silme",
                            text: "Kategori Silinemedi."
                        });
                    }
                }, error: function () {
                    Swal.fire({
                        type: "warning",
                        icon: "warning",
                        title: "Hata",
                        text: "Bir sorun ile karşılaşıldı."
                    });
                }
            });

        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire("İptal edildi", "Yazar silinmedi güvende :)", "warning");
        }
    });
});