using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RolesIS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Admin()
        {
            if (!User.IsInRole("Admin"))
                return Content("Solo Administradores");

            return Content("Eres Administrador, entra");
        }

        [Authorize]
        public ActionResult Vendedor()
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Vendedor"))
                return Content("Solo para vendedores o administradores");

            return Content("Eres Administrador o Vendedor, entra");
        }

        [Authorize]
        public ActionResult Comprador()
        {
            return Content("Eres Comprador");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}