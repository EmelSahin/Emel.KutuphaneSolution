using System.Web.Mvc;
using TB.Kutuphane.Data.UnitOfWork;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.WebUI.Controllers
{
    public class YazarController : BaseController
    {
        public YazarController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var getYazar = _unitOfWork.GetRepository<Yazar>().GetAll();
            return View(getYazar);
        }

        [HttpPost]
        public ActionResult Ekle(string yazarAdi, string yazarSoyadi)
        {
            var yazar = new Yazar { Ad = yazarAdi, Soyad = yazarSoyadi };
            var getYazar = _unitOfWork.GetRepository<Yazar>().Add(yazar);
            _unitOfWork.SaveChanges();
            return Json(new { result = new { getYazar.Id, getYazar.Ad, getYazar.Soyad }, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult Guncelle(int yazarId, string yazarAdi, string yazarSoyadi)
        {
            var getYazar = _unitOfWork.GetRepository<Yazar>().GetById(yazarId);
            getYazar.Ad = yazarAdi;
            getYazar.Soyad = yazarSoyadi;
            var result = _unitOfWork.SaveChanges();
            return Json(result > 0 ? "1" : "0");
        }

        [HttpPost]
        public ActionResult Sil(int yazarId)
        {
            _unitOfWork.GetRepository<Yazar>().Delete(yazarId);
            var result = _unitOfWork.SaveChanges();
            return Json(result > 0 ? "1" : "0");
        }
    }
}