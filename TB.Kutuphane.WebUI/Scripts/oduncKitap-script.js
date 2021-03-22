//odunc kitap kaydet
$(document).on("click", "#oduncKitapKaydet", function () {
    var degerler = {
        uyeId: $("#uyeler option:selected").attr('data-id'),
        kitapId: $("#odunKitaplar option:selected").attr('data-id'),
        getirecegiTarih: $("#getirecegiTarih").val()
    };

    $.ajax({
        type: "POST",
        url: "/OduncKitap/KitapVerJson",
        data: JSON.stringify(degerler),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            if (response == "1") {
                Swal.fire({
                    icon: "success",
                    title: "Ödünç Kitap",
                    text: "Başarılı bir şekilde kitap verildi."
                });
            } else if (response == "0") {
                Swal.fire({
                    icon: "warning",
                    title: "Ödünç Kitap",
                    text: "Kitap verme işlemimide hata oluştu."
                });
            }
        }, error: function () {
            Swal.fire({
                icon: "error",
                title: "Ödünç Kitap",
                text: "Bir hata oluştu."
            });
        }
    });
});


//odunc kitap guncelle
$(document).on("click", "#verilenKitapGuncelle", function () {
    var degerler = {
        uyeId: $("#uyeler option:selected").attr('data-id'),
        kitapId: $("#odunKitaplar option:selected").attr('data-id'),
        getirecegiTarih: $("#getirecegiTarih").val(),
        id: $(this).attr('data-id')
    };

    $.ajax({
        type: "POST",
        url: "/OduncKitap/VerilenKitapGuncelleJson",
        data: JSON.stringify(degerler),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            if (response == "1") {
                Swal.fire({
                    icon: "success",
                    title: "Ödünç Kitap Güncelleme",
                    text: "Başarılı bir şekilde kitap güncellendi."
                });
            } else if (response == "0") {
                Swal.fire({
                    icon: "warning",
                    title: "Ödünç Kitap Güncelleme",
                    text: "Kitap güncelleme işlemimide hata oluştu."
                });
            }
        }, error: function () {
            Swal.fire({
                icon: "error",
                title: "Ödünç Kitap Güncelleme",
                text: "Bir hata oluştu."
            });
        }
    });
});

// getirdi olarak isaretle
$(document).on("click", ".getirdiOlarakIsaretle", async function () {
    var tr = $(this).parent("td").parent("tr");
    var id = $(this).val();

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: "Kitabi getirdi olarak değiştirmek istiyor musunuz?",
        text: "Bunu geri alamayacaksın!",
        icon: "info",
        showCancelButton: true,
        confirmButtonText: "Tamam",
        cancelButtonText: "İptal",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/OduncKitap/GetirdiOlarakIsaretle",
                data: { "id": id },
                dataType: "json",
                success: function (response) {
                    if (response == "1") {
                        tr.remove();
                        Swal.fire({
                            type: "success",
                            icon: "success",
                            title: "Kitap Getirdi",
                            text: "Kitap başarılı bir şekilde getirildi."
                        });
                    } else {
                        Swal.fire({
                            type: "warning",
                            icon: "warning",
                            title: "Kitap Getirdi",
                            text: "Kitap getirme işleminde hata oluştu."
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
                "İşlem iptal edildi :)",
                "warning"
            );
        }
    });
});