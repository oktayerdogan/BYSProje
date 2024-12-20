using System.Linq;
using System.Web.Mvc;
using BYSProje.Models;

namespace BYSProje.Controllers
{
    public class AkademisyenController : Controller
    {
        private readonly ProjeErdoEntities db = new ProjeErdoEntities();

        // Akademisyen için özel index sayfası
        public ActionResult Index()
        {
            // Giriş yapılıp yapılmadığını kontrol et
            if (Session["KullaniciID"] == null || Session["Rol"].ToString() != "Akademisyen")
            {
                return RedirectToAction("Index", "Giris");
            }

            // AkademisyenID'yi session'dan al
            int akademisyenId = (int)Session["KullaniciID"];

            // Akademisyen bilgilerini veritabanından al
            var akademisyenBilgileri = db.AkademisyenTablosu
                .Where(a => a.AkademisyenID == akademisyenId)
                .Select(a => new
                {
                    IsimSoyisim = a.Isim_Soyisim,
                    Baslik = a.Baslık,
                    Departman = a.Departman,
                    Email = a.E_Mail
                })
                .FirstOrDefault();

            // Akademisyene bağlı öğrencileri veritabanından al
            var ogrenciler = db.Ogrenciler
                .Where(o => o.AkademisyenID == akademisyenId)
                .Select(o => new
                {
                    OgrenciID = o.OgrenciID,
                    IsimSoyisim = o.Isim,
                    Soyisim = o.Soyisim,
                    Email = o.E_Mail
                })
                .ToList();

            // Akademisyen bilgilerini ViewBag'e atıyoruz
            ViewBag.IsimSoyisim = akademisyenBilgileri?.IsimSoyisim;
            ViewBag.Baslik = akademisyenBilgileri?.Baslik;
            ViewBag.Departman = akademisyenBilgileri?.Departman;
            ViewBag.Email = akademisyenBilgileri?.Email;

            // Öğrencileri ViewBag'e atıyoruz
            ViewBag.Ogrenciler = ogrenciler;

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
