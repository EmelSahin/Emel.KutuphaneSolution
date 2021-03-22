using System;
using System.Web.Mvc;
using TB.Kutuphane.Data.UnitOfWork;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.WebUI.Controllers
{
    public class UyeController : BaseController
    {
        public UyeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            var getList = _unitOfWork.GetRepository<Uye>().GetAll();
            return View(getList);
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EkleJson(string uyeAd, string uyeSoyad, string uyeTc, string uyeMail, string uyeTelefon)
        {
            if (!string.IsNullOrEmpty(uyeAd) && !string.IsNullOrEmpty(uyeSoyad))
            {
                Uye uye = new Uye();
                uye.Ad = uyeAd;
                uye.Soyad = uyeSoyad;
                uye.TC = uyeTc;
                uye.Mail = uyeMail;
                uye.Telefon = uyeTelefon;
                uye.Ceza = 0;
                uye.KayitTarihi = DateTime.Now;
                _unitOfWork.GetRepository<Uye>().Add(uye);
                var result = _unitOfWork.SaveChanges();
                return Json(result > 0 ? "1" : "0");
            }
            else
            {
                return Json("bosAlan");
            }
        }

        [HttpPost]
        public ActionResult SilJson(int uyeId)
        {
            _unitOfWork.GetRepository<Uye>().Delete(uyeId);
            var result = _unitOfWork.SaveChanges();
            return Json(result > 0 ? "1" : "0");
        }

        [HttpGet]
        public ActionResult Guncelle(int uyeId = 0)
        {
            if (!string.IsNullOrEmpty(uyeId.ToString()) && uyeId > 0)
            {
                var getUye = _unitOfWork.GetRepository<Uye>().GetById(uyeId);
                return View(getUye);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GuncelleJson(int uyeId, string uyeAd, string uyeSoyad, string uyeTc, string uyeMail, string uyeTelefon)
        {
            if (!string.IsNullOrEmpty(uyeAd) && !string.IsNullOrEmpty(uyeSoyad))
            {
                var uye = _unitOfWork.GetRepository<Uye>().GetById(uyeId);
                uye.Ad = uyeAd;
                uye.Soyad = uyeSoyad;
                uye.TC = uyeTc;
                uye.Mail = uyeMail;
                uye.Telefon = uyeTelefon;
                _unitOfWork.GetRepository<Uye>().Update(uye);
                var result = _unitOfWork.SaveChanges();
                return Json(result > 0 ? "1" : "0");
            }
            else
            {
                return Json("bosAlan");
            }
        }
    }
}