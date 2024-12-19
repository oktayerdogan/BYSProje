using System.Linq;
using System.Web.Mvc;
using BYSProje.Models;

namespace BYSProje.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly ProjeErdoEntities db = new ProjeErdoEntities();

        // Öğrenci giriş işlemi
        [HttpPost]
        public ActionResult Index(string eposta, string sifre)
        {
            if (string.IsNullOrWhiteSpace(eposta) || string.IsNullOrWhiteSpace(sifre))
            {
                ViewBag.HataMesaji = "E-posta ve şifre alanları boş bırakılamaz.";
                return RedirectToAction("Index", "Giris");
            }

            // Öğrenci tablosunda e-posta ve şifre kontrolü
            var ogrenci = db.Ogrenciler.FirstOrDefault(o => o.E_Mail == eposta && o.Sifre == sifre);

            if (ogrenci != null)
            {
                // Giriş başarılı, session bilgilerini ayarla
                Session["KullaniciAdi"] = ogrenci.Isim;
                Session["KullaniciID"] = ogrenci.OgrenciID;
                Session["Rol"] = "Ogrenci";

                // Başarılı girişten sonra Views/Ogrenci/Index.cshtml sayfasına yönlendir
                return RedirectToAction("Index", "Ogrenci");
            }

            // Giriş başarısız
            ViewBag.HataMesaji = "E-posta veya şifre yanlış.";
            return RedirectToAction("Index", "Giris");
        }

        // Öğrenci için özel index sayfası
        public ActionResult Index()
        {
            // Giriş yapılıp yapılmadığını kontrol et
            if (Session["KullaniciID"] == null || Session["Rol"].ToString() != "Ogrenci")
            {
                return RedirectToAction("Index", "Giris");
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
