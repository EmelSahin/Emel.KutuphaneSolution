// kullanici giris yap
$(document).on('click', '#userGirisYap', function () {
    $(this).html('Kontrol Ediliyor...');
    $(this).prop('disabled', true);

    var degerler = {
        email: $('#userMail').val(),
        sifre: $('#userSifre').val(),
        hatirla: false
    };

    if ($('#customCheckHatirla').is(':checked')) {
        degerler.hatirla = true;
    }

    $.ajax({
        type: "POST",
        url: "/Giris/LoginJson",
        data: JSON.stringify(degerler),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response == 'basarili') {
                Swal.fire({
                    type: "success",
                    icon: "success",
                    title: "Kullanıcı Başarılı",
                    text: "Yönlendiriliyorsunuz..."
                });
                setTimeout(function () {
                    window.location.href = '/Default/Index';
                }, 3000);
            } else if (response == 'bosOlamaz') {
                Swal.fire({
                    type: "warning",
                    icon: "warning",
                    title: "Giriş Başarısız",
                    text: "Email veya Şifre boş olamaz."
                });
            }
            else {
                Swal.fire({
                    type: "warning",
                    icon: "warning",
                    title: "Giriş Başarısız",
                    text: "Email veya Şifre hatalıdır."
                });
            }
        }, error: function () {
            Swal.fire({
                type: "error",
                icon: "error",
                title: "Giriş Başarısız",
                text: "Giriş yapılırken hata oluştu."
            });
        }, complete: function () {
            $('#userGirisYap').html('Giriş Yap');
            $('#userGirisYap').prop('disabled', false);
        }
    });
});

//profil guncelle
$(document).on("click", "#profilGuncelle", function () {
    var profiliId = $("#profiliId").val();
    var profilMail = $('#profilMail').val();
    var profilTc = $('#profilTc').val();
    var profilTelefon = $('#profilTelefon').val();
    var profilParola = $("#profilParola").val();
    var profilParolaTekrar = $("#profilParolaTekrar").val();

    $.ajax({
        type: "POST",
        url: "/Giris/PersonelProfilGuncelleJson",
        data: { 'profiliId': profiliId, 'profilMail': profilMail, 'profilTc': profilTc, 'profilTelefon': profilTelefon, 'profilParola': profilParola, 'profilParolaTekrar': profilParolaTekrar },
        dataType: "JSON",
        success: function (response) {
            if (response == "1") {
                Swal.fire({
                    icon: "success",
                    title: "Profil Güncelleme",
                    text: "Başarılı bir şekilde güncelleme yapıldı."
                });
            } else if (response == "parolaUyusmazligi") {
                Swal.fire({
                    icon: "warning",
                    title: "Profil Güncelleme",
                    text: "Parola ile parola tekrar uyuşmuyor."
                });
            } else if (response == "mailBosOlamaz") {
                Swal.fire({
                    icon: "warning",
                    title: "Profil Güncelleme",
                    text: "Mail Alanı boş olamaz."
                });
            } else {
                Swal.fire({
                    icon: "warning",
                    title: "Profil Güncelleme",
                    text: "Profil kaydetme işleminde hata meydana geldi."
                });
            }
        }, error: function () {
            Swal.fire({
                icon: "error",
                title: "Profil Güncelleme",
                text: "Bir hata oluştu."
            });
        }
    });
});