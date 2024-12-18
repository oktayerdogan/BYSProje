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
        public ActionResult Index(string eposta, string sifre, string kullaniciTuru)
        {
            if (string.IsNullOrWhiteSpace(eposta) || string.IsNullOrWhiteSpace(sifre))
            {
                ViewBag.HataMesaji = "E-posta ve şifre alanları boş bırakılamaz.";
                return View();
            }

            // Kullanıcı türüne göre doğru tabloyu sorgulama
            if (kullaniciTuru == "Akademisyen")
            {
                var akademisyen = db.AkademisyenTablosu.FirstOrDefault(a => a.E_Mail == eposta && a.Sifre == sifre);

                if (akademisyen != null)
                {
                    // Akademisyen için session verilerini ayarlıyoruz
                    Session["KullaniciAdi"] = akademisyen.Isim_Soyisim;
                    Session["KullaniciID"] = akademisyen.AkademisyenID;
                    Session["Rol"] = "Akademisyen";

                    ViewBag.HataMesaji = "Girdiğiniz bilgiler başarılı (Akademisyen).";
                    return RedirectToAction("AkademisyenDashboard", "Home"); // Akademisyen yönlendirmesi
                }
                else
                {
                    ViewBag.HataMesaji = "E-posta veya şifre yanlış (Akademisyen).";
                }
            }
            else if (kullaniciTuru == "Ogrenci")
            {
                var ogrenci = db.Ogrenciler.FirstOrDefault(o => o.E_Mail == eposta && o.Sifre == sifre);

                if (ogrenci != null)
                {
                    // Öğrenci için session verilerini ayarlıyoruz
                    Session["KullaniciAdi"] = ogrenci.Isim;
                    Session["KullaniciID"] = ogrenci.OgrenciID;
                    Session["Rol"] = "Ogrenci";

                    ViewBag.HataMesaji = "Girdiğiniz bilgiler başarılı (Öğrenci).";
                    return RedirectToAction("OgrenciDashboard", "Home"); // Öğrenci yönlendirmesi
                }
                else
                {
                    ViewBag.HataMesaji = "E-posta veya şifre yanlış (Öğrenci).";
                }
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
