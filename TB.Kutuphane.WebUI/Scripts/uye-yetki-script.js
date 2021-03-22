//uye kaydet
$(document).on("click", "#uyeYetkiEkle", function () {
    var uyeYetkiId = $("#uyeYetkiId").val();
    var uyeYetkiParola = $("#uyeYetkiParola").val();
    var uyeYetkiParolaTekrar = $("#uyeYetkiParolaTekrar").val();
    var uyeYetkiMail = $('#uyeYetkiMail').val();

    $.ajax({
        type: "POST",
        url: "/Uyelik/EkleJson",
        data: { 'uyeYetkiId': uyeYetkiId, 'uyeYetkiParola': uyeYetkiParola, 'uyeYetkiParolaTekrar': uyeYetkiParolaTekrar, 'uyeYetkiMail': uyeYetkiMail },
        dataType: "JSON",
        success: function (response) {
            if (response == "1") {
                Swal.fire({ icon: "success", title: "Üye Ekleme", text: "Başarılı bir şekilde ekleme yapıldı." });
            } else if (response == "parolaUyusmazligi") {
                Swal.fire({ icon: "warning", title: "Üye Yetki Ekleme", text: "Parola ile parola tekrar uyuşmuyor." });
            } else if (response == "bosOlamaz") {
                Swal.fire({ icon: "warning", title: "Üye Yetki Ekleme", text: "Zorunlu alanlari doldurunuz." });
            } else {
                Swal.fire({ type: "warning", icon: "warning", title: "Üye Yetki Ekleme", text: "Üye Eklenemedi.Bir hata oluştu." });
            }
        }, error: function () {
            Swal.fire({ icon: "error", title: "Üye Ekleme", text: "Bir hata oluştu." });
        }
    });
});

//uye guncelle
$(document).on("click", "#uyeYetkiGuncelle", function () {
    var uyeYetkiId = $("#uyeYetkiId").val();
    var uyeYetkiParola = $("#uyeYetkiParola").val();
    var uyeYetkiParolaTekrar = $("#uyeYetkiParolaTekrar").val();
    var uyeYetkiMail = $('#uyeYetkiMail').val();

    $.ajax({
        type: "POST",
        url: "/Uyelik/GuncelleJson",
        data: { 'uyeYetkiId': uyeYetkiId, 'uyeYetkiParola': uyeYetkiParola, 'uyeYetkiParolaTekrar': uyeYetkiParolaTekrar, 'uyeYetkiMail': uyeYetkiMail },
        dataType: "JSON",
        success: function (response) {
            if (response == "1") {
                Swal.fire({
                    icon: "success",
                    title: "Üye Yetki Güncelleme",
                    text: "Başarılı bir şekilde güncelleme yapıldı."
                });
            } else if (response == "parolaUyusmazligi") {
                Swal.fire({
                    icon: "warning",
                    title: "Üye Yetki Güncelleme",
                    text: "Parola ile parola tekrar uyuşmuyor."
                });
            } else if (response == "mailBosOlamaz") {
                Swal.fire({
                    icon: "warning",
                    title: "Üye Yetki Güncelleme",
                    text: "Mail alanı boş olamaz."
                });
            }
        }, error: function () {
            Swal.fire({
                icon: "error",
                title: "Üye Yetki Güncelleme",
                text: "Bir hata oluştu."
            });
        }
    });
});

//uye sil
$(document).on("click", ".uyeYetkiSil", function () {
    var uyeYetkiId = $(this).val();
    var selectedColumn = $(this).parent('td').parent('tr');

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
                url: "/Uyelik/SilJson",
                data: { "uyeYetkiId": uyeYetkiId },
                dataType: "json",
                success: function (response) {
                    if (response == "1") {
                        selectedColumn.remove();
                        Swal.fire({ type: "success", icon: "success", title: "Üyelik Silme", text: "Üye başarılı bir şekilde silindi." });
                    } else if (response == "EmptyUser") {
                        Swal.fire({ type: "success", icon: "info", title: "Üyelik Silme", text: "Silinecek üye bulunamadı." });
                    } else {
                        Swal.fire({ type: "warning", icon: "warning", title: "Üyelik Silme", text: "Üye Silinemedi.Bir hata oluştu." });
                    }
                }, error: function () {
                    Swal.fire({ type: "error", icon: "error", title: "Hata", text: "Bir sorun ile karşılaşıldı." });
                }
            });

        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire("İptal edildi", "Üyelik silinmedi güvende :)", "warning");
        }
    });
});

//uye yetki degistir
$(document).on('click', '#yetkiAta', async function () {

    var select = '<select class="form-control" id="yetkiId">' +
        '<option value="2">Moderetör</option>' +
        '<option value="3">İzleyici</option>' +
        '</select>';

    const { value: formValues } = await Swal.fire({
        title: "Yetki Ata",
        html: select
    });

    var uyeId = $(this).attr('data-id');
    var yetkiId = $('#yetkiId').val();
    var yetkiAd = $('#yetkiId option:selected').text();
    var button = $(this);

    $.ajax({
        type: "POST",
        url: "/Uyelik/YetkiAtaJson",
        data: { 'uyeId': uyeId, 'yetkiId': yetkiId },
        dataType: "json",
        success: function (response) {
            button.text(yetkiAd);
            if (response == "1") {
                Swal.fire({
                    icon: "success",
                    title: "Yetki Değiştirme",
                    text: "Başarılı bir şekilde yetki değişti."
                });
            } else if (response == "EmptyUser") {
                Swal.fire({
                    type: "warning",
                    icon: "warning",
                    title: "Yetki Değiştirme",
                    text: "Yetki Değiştirme hata oluştu."
                });
            } else {
                Swal.fire({
                    type: "warning",
                    icon: "warning",
                    title: "Yetki Değiştirme",
                    text: "Yetki Değiştirme hata oluştu."
                });
            }
        }, error: function () {
            Swal.fire({
                icon: "error",
                title: "Yetki Değiştirme",
                text: "Bir hata oluştu."
            });
        }
    });
});