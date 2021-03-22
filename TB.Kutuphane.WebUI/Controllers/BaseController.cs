using System.Web.Mvc;
using TB.Kutuphane.Common;
using TB.Kutuphane.Data.UnitOfWork;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected UnitOfWork _unitOfWork;
        protected ServiceResponse _serviceResponse;

        public BaseController()
        {
            _unitOfWork = new UnitOfWork();

            ViewBag.KategoriSayisi = _unitOfWork.GetRepository<Kategori>().GetAll().Count;
            ViewBag.YazarSayisi = _unitOfWork.GetRepository<Yazar>().GetAll().Count;
            ViewBag.KitapSayisi = _unitOfWork.GetRepository<Kitap>().GetAll().Count;
            ViewBag.UyeSayisi = _unitOfWork.GetRepository<Uye>().GetAll().Count;
            ViewBag.UyelikSayisi = _unitOfWork.GetRepository<Uye>().GetAll(x => x.Yetki != null).Count;
        }
    }
}