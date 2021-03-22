using System;
using System.Web.Mvc;
using TB.Kutuphane.Data.UnitOfWork;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.WebUI.Controllers
{
    public class OduncKitapController : BaseController
    {
        public OduncKitapController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [HttpGet]
        public ActionResult VerilenKitap()
        {
            var getOduncKitap = _unitOfWork.GetRepository<OduncKitap>().GetAll(x => x.GetirdigiTarih == null);
            return View(getOduncKitap);
        }

        [HttpGet]
        public ActionResult TeslipEdilenKitap()
        {
            var getKitap = _unitOfWork.GetRepository<OduncKitap>().GetAll(x => x.GetirdigiTarih != null);
            return View(getKitap);
        }

        [HttpGet]
        public ActionResult KitapVer()
        {
            ViewBag.KitaplarListesi = _unitOfWork.GetRepository<Kitap>().GetAll(x => x.Adet > 0);
            ViewBag.UyelerListesi = _unitOfWork.GetRepository<Uye>().GetAll();
            return View();
        }

        [HttpPost]
        public JsonResult KitapVerJson(int uyeId, int kitapId, DateTime getirecegiTarih)
        {
            OduncKitap oduncKitap = new OduncKitap
            {
                AlisTarih = DateTime.Now,
                GetirecegiTarih = getirecegiTarih,
                KitapId = kitapId,
                UyeId = uyeId
            };
            _unitOfWork.GetRepository<OduncKitap>().Add(oduncKitap);
            int result = _unitOfWork.SaveChanges();
            return Json(result > 0 ? "1" : "0");
        }

        public ActionResult VerilenKitapGuncelle(int id)
        {
            ViewBag.KitaplarListesi = _unitOfWork.GetRepository<Kitap>().GetAll(x => x.Adet > 0);
            ViewBag.UyelerListesi = _unitOfWork.GetRepository<Uye>().GetAll();
            var getKitap = _unitOfWork.GetRepository<OduncKitap>().GetById(id);
            return View(getKitap);
        }

        [HttpPost]
        public ActionResult VerilenKitapGuncelleJson(int id, int uyeId, int kitapId, DateTime getirecegiTarih)
        {
            var oduncKitap = _unitOfWork.GetRepository<OduncKitap>().GetById(id);
            oduncKitap.GetirecegiTarih = getirecegiTarih;
            oduncKitap.KitapId = kitapId;
            oduncKitap.UyeId = uyeId;
            _unitOfWork.GetRepository<OduncKitap>().Update(oduncKitap);
            int result = _unitOfWork.SaveChanges();
            return Json(result > 0 ? "1" : "0");
        }

        [HttpPost]
        public JsonResult GetirdiOlarakIsaretle(int id)
        {
            var oduncKitap = _unitOfWork.GetRepository<OduncKitap>().GetById(id);
            oduncKitap.GetirdiMi = true;
            oduncKitap.GetirdigiTarih = DateTime.Now;
            _unitOfWork.GetRepository<OduncKitap>().Update(oduncKitap);
            var result = _unitOfWork.SaveChanges();
            return Json(result > 0 ? "1" : "0");
        }
    }
}