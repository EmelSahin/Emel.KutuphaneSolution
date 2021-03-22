// kategori ekle
$(document).on("click", "#kategoriEkle", async function () {
    const { value: formValues } = await Swal.fire({
        title: "Kategori Ekle",
        html: '<input id="kategoriAdi" class="swal2-input" placeholder="kategori adı">',
        focusConfirm: false
    });
    if (formValues) {
        var getKategoriAdi = $("#kategoriAdi").val();
        if (getKategoriAdi != null && getKategoriAdi != "") {
            $.ajax({
                type: "POST",
                url: "/Kategori/Ekle",
                data: { "kategoriAdi": getKategoriAdi },
                dataType: "json",
                success: function (response) {
                    var kategoriId = response.result.Id;
                    var kategoriAdi = "<td>" + response.result.KategoriAdi + "</td>";
                    var kitapAdet = "<td>0</td>";
                    var buttonGuncelle = "<td><button class='kategoriGuncelle btn waves-effect waves-light btn-warning btn-sm text-dark' value='" + kategoriId + "'><i class='fa fa-edit'></i>Güncelle</button></td>";
                    var buttonSil = "<td><button class='kategoriSil btn waves-effect waves-light btn-danger btn-sm' value='" + kategoriId + "'><i class='fa fa-trash'></i> Sil</button></td>";
                    $("#example tbody").append("<tr>" + kategoriAdi + kitapAdet + buttonGuncelle + buttonSil + "</tr>");
                    Swal.fire({
                        type: "success",
                        icon: "success",
                        title: "Kategori Ekleme",
                        text: "Kategori başarılı bir şekilde eklendi."
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
                title: "Kategori Ekleme",
                text: "Kategori alanı boş geçilemez."
            });
        }
    }
});

// kategori guncelle
$(document).on("click", ".kategoriGuncelle", async function () {
    var getKategoriId = $(this).val();
    var getKategoriAdiTd = $(this).parent("td").parent("tr").find("td:first");
    var getKategoriAdTdHtml = getKategoriAdiTd.html();
    const { value: formValues } = await Swal.fire({
        title: "Kategori Güncelle",
        html: '<input id="kategoriAdi" class="swal2-input" value="' + getKategoriAdTdHtml + '" placeholder="kategori adı">',
        focusConfirm: false
    });
    if (formValues) {
        getKategoriAdTdHtml = $("#kategoriAdi").val();
        if (getKategoriAdTdHtml != null && getKategoriAdTdHtml != "") {
            $.ajax({
                type: "POST",
                url: "/Kategori/Guncelle",
                data: { "kategoriId": getKategoriId, "kategoriAdi": getKategoriAdTdHtml },
                dataType: "json",
                success: function (response) {
                    if (response == "1") {
                        getKategoriAdiTd.html(getKategoriAdTdHtml);
                        Swal.fire({
                            type: "success",
                            icon: "success",
                            title: "Kategori Güncelleme",
                            text: "Kategori başarılı bir şekilde güncellendi."
                        });
                    } else {
                        Swal.fire({
                            type: "warning",
                            icon: "warning",
                            title: "Kategori Güncelleme",
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
                title: "Kategori Güncelleme",
                text: "Kategori alanı boş geçilemez."
            });
        }
    }
});

// kategori silme
$(document).on("click", ".kategoriSil", async function () {
    var tr = $(this).parent("td").parent("tr");
    var getKategoriId = $(this).val();

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: "Silmek istediğinize emin misiniz?",
        text: "Bunu geri alamayacaksın!",
        icon: "info",
        showCancelButton: true,
        confirmButtonText: "Sil",
        cancelButtonText: "İptal",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Kategori/Sil",
                data: { "kategoriId": getKategoriId },
                dataType: "json",
                success: function (response) {
                    if (response == "1") {
                        tr.remove();
                        Swal.fire({
                            type: "success",
                            icon: "success",
                            title: "Kategori Silme",
                            text: "Kategori başarılı bir şekilde silindi."
                        });
                    } else {
                        Swal.fire({
                            type: "warning",
                            icon: "warning",
                            title: "Kategori Silme",
                            text: "Kategori Silinemedi."
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

        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                "İptal edildi",
                "Kategoriniz silinmedi güvende :)",
                "warning"
            );
        }
    });
});