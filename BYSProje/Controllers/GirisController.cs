using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BYSProje.Models;


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
    }
}