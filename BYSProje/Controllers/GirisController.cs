using BYSProje.Models;
using System.Linq;
using System.Web.Mvc;

namespace BYSProje.Controllers
{
    public class GirisController : Controller
    {
        private readonly ProjeErdoEntities db = new ProjeErdoEntities();

        // GET: Giris
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string eposta, string sifre)
        {
            if (string.IsNullOrWhiteSpace(eposta) || string.IsNullOrWhiteSpace(sifre))
            {
                ViewBag.HataMesaji = "E-posta ve şifre alanları boş bırakılamaz.";
                return View();
            }

            // Kullanıcı veritabanında var mı kontrolü
            var ogrenci = db.Ogrenciler.FirstOrDefault(o => o.E_Mail == eposta && o.Sifre == sifre);
            var akademisyen = db.AkademisyenTablosu.FirstOrDefault(a => a.E_Mail == eposta && a.Sifre == sifre);

            if (ogrenci != null)
            {
                ViewBag.HataMesaji = "Girdiğiniz bilgiler başarılı (Öğrenci).";
                return Redirect("https://www.google.com");
            }
            else if (akademisyen != null)
            {
                ViewBag.HataMesaji = "Girdiğiniz bilgiler başarılı (Akademisyen).";
            }
            else
            {
                ViewBag.HataMesaji = "E-posta veya şifre yanlış. Tekrar deneyiniz.";
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
