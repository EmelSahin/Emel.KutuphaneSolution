using System.Web.Mvc;
using TB.Kutuphane.Common;
using TB.Kutuphane.Common.Models;
using TB.Kutuphane.Data.UnitOfWork;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.WebUI.Controllers
{
    public class KategoriController : BaseController
    {
        public KategoriController()
        {
            _unitOfWork = new UnitOfWork();
            _serviceResponse = new ServiceResponse();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var getKategori = _unitOfWork.GetRepository<Kategori>().GetAll();
            return View(getKategori);
        }

        [HttpPost]
        public ActionResult Ekle(string kategoriAdi)
        {
            var kategori = new Kategori();
            kategori.KategoriAdi = kategoriAdi;
            var getKategori = _unitOfWork.GetRepository<Kategori>().Add(kategori);
            _unitOfWork.SaveChanges();
            return Json(new { result = new { getKategori.Id, getKategori.KategoriAdi }, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult Guncelle(int kategoriId, string kategoriAdi)
        {
            var json = new JsonDataModel();


            var getKategori = _unitOfWork.GetRepository<Kategori>().GetById(kategoriId);
            getKategori.KategoriAdi = kategoriAdi;
            var result = _unitOfWork.SaveChanges();
            return Json(result > 0 ? "1" : "0");
        }

        [HttpPost]
        public ActionResult Sil(int kategoriId)
        {
            _unitOfWork.GetRepository<Kategori>().Delete(kategoriId);
            var result = _unitOfWork.SaveChanges();
            return Json(result > 0 ? "1" : "0");
        }
    }
}