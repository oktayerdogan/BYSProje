using System.Linq;
using System.Web.Mvc;
using BYSProje.Models;

namespace BYSProje.Controllers
{
    public class AkademisyenController : Controller
    {
        private readonly ProjeErdoEntities db = new ProjeErdoEntities();

        // Akademisyen giriş işlemi
        [HttpPost]
        public ActionResult Index(string eposta, string sifre)
        {
            if (string.IsNullOrWhiteSpace(eposta) || string.IsNullOrWhiteSpace(sifre))
            {
                ViewBag.HataMesaji = "E-posta ve şifre alanları boş bırakılamaz.";
                return RedirectToAction("Index", "Giris");
            }

            // Akademisyen tablosunda e-posta ve şifre kontrolü
            var akademisyen = db.AkademisyenTablosu.FirstOrDefault(a => a.E_Mail == eposta && a.Sifre == sifre);

            if (akademisyen != null)
            {
                // Giriş başarılı, session bilgilerini ayarla
                Session["KullaniciAdi"] = akademisyen.Isim_Soyisim;
                Session["KullaniciID"] = akademisyen.AkademisyenID;
                Session["Rol"] = "Akademisyen";

                // Başarılı girişten sonra Views/Akademisyen/Index.cshtml sayfasına yönlendir
                return RedirectToAction("Index", "Akademisyen");
            }

            // Giriş başarısız
            ViewBag.HataMesaji = "E-posta veya şifre yanlış.";
            return RedirectToAction("Index", "Giris");
        }

        // Akademisyen için özel index sayfası
        public ActionResult Index()
        {
            // Giriş yapılıp yapılmadığını kontrol et
            if (Session["KullaniciID"] == null || Session["Rol"].ToString() != "Akademisyen")
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
