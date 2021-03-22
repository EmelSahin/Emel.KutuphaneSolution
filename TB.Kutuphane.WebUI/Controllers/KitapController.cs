using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TB.Kutuphane.Data.UnitOfWork;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.WebUI.Controllers
{
    public class KitapController : BaseController
    {
        public KitapController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            var getKitapList = _unitOfWork.GetRepository<Kitap>().GetAll();
            return View(getKitapList);
        }

        public ActionResult Ekle()
        {
            ViewBag.KategoriList = _unitOfWork.GetRepository<Kategori>().GetAll();
            ViewBag.YazarList = _unitOfWork.GetRepository<Yazar>().GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult EkleJson(string[] kategoriler, string yazar, string kitapAd, string kitapAdet, string siraNo)
        {
            if (kategoriler != null &&
                !string.IsNullOrEmpty(yazar) &&
                !string.IsNullOrEmpty(kitapAd) &&
                !string.IsNullOrEmpty(kitapAdet) &&
                !string.IsNullOrEmpty(siraNo))
            {
                //var getKategori = kategoriler
                //    .Select(item => Convert.ToInt32(item))
                //    .Select(kategoriId => _unitOfWork.GetRepository<Kategori>()
                //        .GetById(kategoriId))
                //    .ToList();
                var getKategori = new List<Kategori>();
                foreach (var item in kategoriler)
                {
                    var kategoriId = Convert.ToInt32(item);
                    var kategori = _unitOfWork.GetRepository<Kategori>().GetById(kategoriId);
                    getKategori.Add(kategori);
                }

                var kitap = new Kitap
                {
                    Ad = kitapAd,
                    Adet = Convert.ToInt32(kitapAdet),
                    SiraNo = siraNo,
                    EklemeTarihi = DateTime.Now,
                    YazarId = Convert.ToInt32(yazar),
                    Kategoriler = getKategori
                };
                _unitOfWork.GetRepository<Kitap>().Add(kitap);
                var result = _unitOfWork.SaveChanges();
                return Json(result > 0 ? "1" : "0");
            }
            else
            {
                return Json("bosAlan");
            }
        }

        [HttpPost]
        public ActionResult SilJson(int kitapId)
        {
            _unitOfWork.GetRepository<Kitap>().Delete(kitapId);
            var result = _unitOfWork.SaveChanges();
            return Json(result > 0 ? "1" : "0");
        }

        [HttpGet]
        public ActionResult Guncelle(int kitapId = 0)
        {
            ViewBag.KategoriList = _unitOfWork.GetRepository<Kategori>().GetAll();
            ViewBag.YazarList = _unitOfWork.GetRepository<Yazar>().GetAll();

            if (!string.IsNullOrEmpty(kitapId.ToString()) && kitapId > 0)
            {
                var getKitap = _unitOfWork.GetRepository<Kitap>().GetById(kitapId);
                return View(getKitap);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GuncelleJson(int kitapId, string[] kategoriler, string yazar, string kitapAd, string kitapAdet, string siraNo)
        {
            if (kategoriler != null &&
                !string.IsNullOrEmpty(yazar) &&
                !string.IsNullOrEmpty(kitapAd) &&
                !string.IsNullOrEmpty(kitapAdet) &&
                !string.IsNullOrEmpty(siraNo) &&
                !string.IsNullOrEmpty(kitapId.ToString()))
            {
                var getKategori = new List<Kategori>();
                foreach (var item in kategoriler)
                {
                    var kategoriId = Convert.ToInt32(item);
                    var kategori = _unitOfWork.GetRepository<Kategori>().GetById(kategoriId);
                    getKategori.Add(kategori);
                }

                var getKitap = _unitOfWork.GetRepository<Kitap>().GetById(kitapId);
                getKitap.Kategoriler.Clear();
                getKitap.Kategoriler = getKategori;
                getKitap.Ad = kitapAd;
                getKitap.Adet = Convert.ToInt32(kitapAdet);
                getKitap.SiraNo = siraNo;
                getKitap.YazarId = Convert.ToInt32(yazar);
                _unitOfWork.GetRepository<Kitap>().Update(getKitap);
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