using BYSProje.Models;
using System.Net.Mail;
using System.Web.Mvc;
using System.Linq;



namespace BYSProje.Controllers
{
    public class GirisController : Controller
    {
        ProjeErdoEntities entity = new ProjeErdoEntities(); //Bu entity sayesinde veri tabanına bağlantı kurulur.
        // GET: Giris

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string akademisyenEposta,string akademisyenSifre,string ogrenciEposta,string ogrenciSifre)
        {
            var akademisyen = (from a in entity.AkademisyenTablosu where a.E_Mail==akademisyenEposta && a.Sifre==akademisyenSifre select a);
            var ogrenci = (from o in entity.Ogrenciler where o.E_Mail == ogrenciEposta && o.Sifre == ogrenciSifre select o);
            return View();
        }
        
    }
}