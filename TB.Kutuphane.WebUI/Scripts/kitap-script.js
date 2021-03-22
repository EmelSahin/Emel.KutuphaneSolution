
//kitaplara kategori ekleme
$(document).on("click", "#kitapKategoriEkle", function () {
    var secilenKategoriAd = $("#kategoriler").val();
    var secilenId = $("#kategoriler option:selected").attr("data-id");
    if (secilenId > 0) {
        $("#eklenenKategoriler").append("<span id='" + secilenId + "' class='badge badge-warning badgeKategori' style='margin: 5px;'>" + secilenKategoriAd + "</span > ");
        $("#kategoriler option:selected").remove();
    } else {
        Swal.fire({
            icon: "error",
            title: "Kategori Ekleme",
            text: "Listede eklenecek kategori kalmadı."
        });
    }
});

//kitaplara kategori eklemeyi geri alma
$(document).on("click", ".badgeKategori", function () {

    var id = $(this).attr("id");
    var kategoriAdi = $(this).html();

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: "Kategori Ekleme",
        text: kategoriAdi + " -  listeye geri eklemek istediğinize emin misiniz?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet",
        cancelButtonText: "Hayır",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $("#kategoriler").append('<option data-id="' + id + '">' + kategoriAdi + "</option>");
            $(this).remove();
            swalWithBootstrapButtons.fire(
                "Eklendi!",
                kategoriAdi + " - başarılı bir şekide listeye geri eklendi.",
                "success"
            );
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                "İptal Edildi",
                "İşlem iptal edildi :)",
                "error"
            );
        }
    });
});

//kitapKaydet
$(document).on("click", "#kkitapKaydet", function () {
    var degerler = {
        kategoriler: [],
        yazar: $("#yazarlar option:selected").attr("data-id"),
        kitapAd: $("#kkitapAdi").val(),
        kitapAdet: $("#kkitapAdet").val(),
        siraNo: $("#ksiraNo").val()
    };

    $("#eklenenKategoriler span").each(function () {
        var id = $(this).attr("id");
        degerler.kategoriler.push(id);
    });

    $.ajax({
        type: "POST",
        url: "/Kitap/EkleJson",
        data: JSON.stringify(degerler),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            if (response == "1") {
                Swal.fire({
                    icon: "success",
                    title: "Kitap Ekleme",
                    text: "Başarılı bir şekilde ekleme yapıldı."
                });
            } else if (response == "0") {
                Swal.fire({
                    icon: "warning",
                    title: "Kitap Ekleme",
                    text: "Kaydetme sirasinda bir hata meydana geldi."
                });
            } else if (response == "bosAlan") {
                Swal.fire({
                    icon: "information",
                    title: "Kitap Ekleme",
                    text: "Zorunlu alanlari doldurunuz."
                });
            }
        }, error: function () {
            Swal.fire({
                icon: "error",
                title: "Kitap Ekleme",
                text: "Bir hata oluştu."
            });
        }
    });
});

// kitap silme
$(document).on("click", ".kitapSil", async function () {
    var tr = $(this).parent("td").parent("tr");
    var getKitapId = $(this).val();

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
                url: "/Kitap/SilJson",
                data: { "kitapId": getKitapId },
                dataType: "json",
                success: function (response) {
                    if (response == "1") {
                        tr.remove();
                        Swal.fire({
                            type: "success",
                            icon: "success",
                            title: "Kitap Silme",
                            text: "Kitap başarılı bir şekilde silindi."
                        });
                    } else {
                        Swal.fire({
                            type: "warning",
                            icon: "warning",
                            title: "Kitap Silme",
                            text: "Kitap Silinemedi.Bir hata oluştu."
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
                "Kitap silinmedi güvende :)",
                "warning"
            );
        }
    });
});

//kitapGuncelle
$(document).on("click", "#kkitapGuncelle", function () {
    var degerler = {
        kategoriler: [],
        yazar: $("#yazarlar option:selected").attr("data-id"),
        kitapAd: $("#kkitapAdi").val(),
        kitapAdet: $("#kkitapAdet").val(),
        siraNo: $("#ksiraNo").val(),
        kitapId: $(this).attr('data-id')
    };

    $("#eklenenKategoriler span").each(function () {
        var id = $(this).attr("id");
        degerler.kategoriler.push(id);
    });

    $.ajax({
        type: "POST",
        url: "/Kitap/GuncelleJson",
        data: JSON.stringify(degerler),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            if (response == "1") {
                Swal.fire({
                    icon: "success",
                    title: "Kitap Güncelleme",
                    text: "Başarılı bir şekilde güncelleme yapıldı."
                });
            } else if (response == "0") {
                Swal.fire({
                    icon: "warning",
                    title: "Kitap Güncelleme",
                    text: "Güncelleme sirasinda bir hata meydana geldi."
                });
            } else if (response == "bosAlan") {
                Swal.fire({
                    icon: "information",
                    title: "Kitap Güncelleme",
                    text: "Zorunlu alanlari doldurunuz."
                });
            }
        }, error: function () {
            Swal.fire({
                icon: "error",
                title: "Kitap Güncelleme",
                text: "Bir hata oluştu."
            });
        }
    });
});

