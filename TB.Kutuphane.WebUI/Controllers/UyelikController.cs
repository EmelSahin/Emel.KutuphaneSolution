using System.Web.Mvc;
using TB.Kutuphane.Data.HelperClass;
using TB.Kutuphane.Data.UnitOfWork;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.WebUI.Controllers
{
    public class UyelikController : BaseController
    {
        public UyelikController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var getUyeList = _unitOfWork.GetRepository<Uye>().GetAll(x => x.Yetki != null);
            return View(getUyeList);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            var getUyeYetki = _unitOfWork.GetRepository<Uye>().GetAll(x => x.Yetki == null);
            return View(getUyeYetki);
        }

        [HttpPost]
        public JsonResult EkleJson(int uyeYetkiId, string uyeYetkiParola, string uyeYetkiParolaTekrar, string uyeYetkiMail)
        {
            if (!string.IsNullOrEmpty(uyeYetkiParola) && !string.IsNullOrEmpty(uyeYetkiParolaTekrar) && !string.IsNullOrEmpty(uyeYetkiMail))
            {
                if (uyeYetkiParola == uyeYetkiParolaTekrar)
                {
                    uyeYetkiParola = HashPassword.Passwording(uyeYetkiParola);
                    var uye = _unitOfWork.GetRepository<Uye>().GetById(uyeYetkiId);
                    uye.Sifre = uyeYetkiParola;
                    uye.Mail = uyeYetkiMail;
                    uye.Yetki = "3";
                    _unitOfWork.GetRepository<Uye>().Update(uye);
                    int result = _unitOfWork.SaveChanges();
                    return Json(result > 0 ? "1" : "0");
                }
                else
                {
                    return Json("parolaUyusmazligi");
                }
            }
            else
            {
                return Json("bosOlamaz");
            }
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var getUye = _unitOfWork.GetRepository<Uye>().GetById(id);
            return View(getUye);
        }

        [HttpPost]
        public JsonResult GuncelleJson(int uyeYetkiId, string uyeYetkiParola, string uyeYetkiParolaTekrar, string uyeYetkiMail)
        {
            if (!string.IsNullOrEmpty(uyeYetkiMail))
            {
                if (uyeYetkiParola == uyeYetkiParolaTekrar)
                {
                    uyeYetkiParola = HashPassword.Passwording(uyeYetkiParola);
                    var uye = _unitOfWork.GetRepository<Uye>().GetById(uyeYetkiId);
                    if (!string.IsNullOrEmpty(uyeYetkiParola))
                        uye.Sifre = uyeYetkiParola;
                    uye.Mail = uyeYetkiMail;
                    _unitOfWork.GetRepository<Uye>().Update(uye);
                    _unitOfWork.SaveChanges();
                    return Json("1");
                }
                else
                {
                    return Json("parolaUyusmazligi");
                }
            }
            else
            {
                return Json("mailBosOlamaz");
            }
        }

        [HttpPost]
        public JsonResult SilJson(int uyeYetkiId)
        {
            var getUser = _unitOfWork.GetRepository<Uye>().GetById(uyeYetkiId);
            if (getUser != null)
            {
                _unitOfWork.GetRepository<Uye>().Delete(getUser);
                var result = _unitOfWork.SaveChanges();
                return Json(result > 0 ? "1" : "0");
            }
            else
            {
                return Json("EmptyUser");
            }
        }

        [HttpPost]
        public JsonResult YetkiAtaJson(int uyeId, int yetkiId)
        {
            var getUser = _unitOfWork.GetRepository<Uye>().GetById(uyeId);
            if (getUser != null)
            {
                getUser.Yetki = yetkiId.ToString();
                _unitOfWork.GetRepository<Uye>().Update(getUser);
                var result = _unitOfWork.SaveChanges();
                return Json(result > 0 ? "1" : "0");
            }
            else
            {
                return Json("EmptyUser");
            }
        }
    }
}