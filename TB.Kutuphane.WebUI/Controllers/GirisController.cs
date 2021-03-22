using System;
using System.Web;
using System.Web.Mvc;
using TB.Kutuphane.Data.HelperClass;
using TB.Kutuphane.Data.UnitOfWork;
using TB.Kutuphane.Entity;
using TB.Kutuphane.Common;

namespace TB.Kutuphane.WebUI.Controllers
{
    public class GirisController : BaseController
    {
        public GirisController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Request.Cookies["uye"] != null)
                return RedirectToAction("Index", "Default");
            return View();
        }

        [HttpPost]
        public JsonResult LoginJson(string email, string sifre, bool hatirla)
        {
            email = email.Trim();
            sifre = sifre.Trim();
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(sifre))
                return Json("bosOlamaz");
            sifre = sifre.Passwording();
            Uye uye = new Uye();
            try
            {
                uye = _unitOfWork.GetRepository<Uye>().Get(x => x.Mail == email && x.Sifre == sifre);
            }
            catch { }

            if (uye != null)
            {
                HttpCookie cookie = new HttpCookie("uye");
                cookie.Values.Add("Id", uye.Id.ToString());
                cookie.Values.Add("Ad", uye.Ad);
                cookie.Values.Add("Soyad", uye.Soyad);
                cookie.Values.Add("YetkiId", uye.Yetki);
                if (hatirla)
                    cookie.Expires = DateTime.Now.AddDays(5);
                Response.Cookies.Add(cookie);
                return Json("basarili");
            }
            else
            {
                return Json("hata");
            }
        }

        public ActionResult CikisYap()
        {
            var cookie = Response.Cookies["uye"];
            if (cookie != null)
                cookie.Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Login", "Giris");
        }

        public ActionResult PersonelProfil()
        {
            int profilId = Convert.ToInt32(Request.Cookies["uye"]["Id"]);
            var getUser1 = SessionManager.CurrentSession.Id.ToString();
            var getUser = _unitOfWork.GetRepository<Uye>().GetById(profilId);
            return View(getUser);
        }

        [HttpPost]
        public JsonResult PersonelProfilGuncelleJson(int profiliId, string profilMail, string profilTc, string profilTelefon, string profilParola, string profilParolaTekrar)
        {
            profilMail = profilMail.Trim();
            profilTc = profilTc.Trim();
            profilTelefon = profilTelefon.Trim();
            profilParola = profilParola.Trim();
            profilParolaTekrar = profilParolaTekrar.Trim();

            if (string.IsNullOrEmpty(profilMail))
                return Json("mailBosOlamaz");
            if (profilParola == profilParolaTekrar)
            {
                var profilId = Convert.ToInt32(Request.Cookies["uye"]["Id"]);
                var getSessionId = SessionManager.CurrentSession.Id;
                var getUser = _unitOfWork.GetRepository<Uye>().GetById(profilId);

                getUser.Mail = profilMail;
                getUser.TC = profilTc;
                getUser.Telefon = profilTelefon;
                if (!string.IsNullOrEmpty(profilParola))
                {
                    profilParola = HashPassword.Passwording(profilParola);
                    getUser.Sifre = profilParola;
                }
                _unitOfWork.GetRepository<Uye>().Update(getUser);
                int result = _unitOfWork.SaveChanges();
                return Json(result > 0 ? "1" : "0");
            }
            else
            {
                return Json("parolaUyusmazligi");
            }
        }
    }
}