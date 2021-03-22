//uye kaydet
$(document).on("click", "#uyeKaydet", function () {
    var degerler = {
        uyeAd: $("#uyeAd").val(),
        uyeSoyad: $("#uyeSoyad").val(),
        uyeTc: $("#uyeTC").val(),
        uyeMail: $("#uyeMail").val(),
        uyeTelefon: $("#uyeTelefon").val()
    };

    $.ajax({
        type: "POST",
        url: "/Uye/EkleJson",
        data: JSON.stringify(degerler),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            if (response == "1") {
                Swal.fire({
                    icon: "success",
                    title: "Üye Ekleme",
                    text: "Başarılı bir şekilde ekleme yapıldı."
                });
            } else if (response == "0") {
                Swal.fire({
                    icon: "warning",
                    title: "Üye Ekleme",
                    text: "Kaydetme sirasinda bir hata meydana geldi."
                });
            } else if (response == "bosAlan") {
                Swal.fire({
                    icon: "information",
                    title: "Üye Ekleme",
                    text: "Zorunlu alanlari doldurunuz."
                });
            }
        }, error: function () {
            Swal.fire({
                icon: "error",
                title: "Üye Ekleme",
                text: "Bir hata oluştu."
            });
        }
    });
});

// uye silme
$(document).on("click", ".uyeSil", async function () {
    var tr = $(this).parent("td").parent("tr");
    var getUyeId = $(this).val();

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
                url: "/Uye/SilJson",
                data: { "uyeId": getUyeId },
                dataType: "json",
                success: function (response) {
                    if (response == "1") {
                        tr.remove();
                        Swal.fire({
                            type: "success",
                            icon: "success",
                            title: "Üye Silme",
                            text: "Üye başarılı bir şekilde silindi."
                        });
                    } else {
                        Swal.fire({
                            type: "warning",
                            icon: "warning",
                            title: "Üye Silme",
                            text: "Üye Silinemedi.Bir hata oluştu."
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
                "Üye silinmedi güvende :)",
                "warning"
            );
        }
    });
});

//uye Guncelle
$(document).on("click", "#uyeGuncelle", function () {
    var degerler = {
        uyeAd: $("#uyeAd").val(),
        uyeSoyad: $("#uyeSoyad").val(),
        uyeTc: $("#uyeTC").val(),
        uyeMail: $("#uyeMail").val(),
        uyeTelefon: $("#uyeTelefon").val(),
        uyeId: $(this).attr("data-id")
    };

    $.ajax({
        type: "POST",
        url: "/Uye/GuncelleJson",
        data: JSON.stringify(degerler),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            if (response == "1") {
                Swal.fire({
                    icon: "success",
                    title: "Üye Güncelleme",
                    text: "Başarılı bir şekilde güncelleme yapıldı."
                });
            } else if (response == "0") {
                Swal.fire({
                    icon: "warning",
                    title: "Üye Güncelleme",
                    text: "Güncelleme sirasinda bir hata meydana geldi."
                });
            } else if (response == "bosAlan") {
                Swal.fire({
                    icon: "information",
                    title: "Üye Güncelleme",
                    text: "Zorunlu alanlari doldurunuz."
                });
            }
        }, error: function () {
            Swal.fire({
                icon: "error",
                title: "Üye Güncelleme",
                text: "Bir hata oluştu."
            });
        }
    });
});