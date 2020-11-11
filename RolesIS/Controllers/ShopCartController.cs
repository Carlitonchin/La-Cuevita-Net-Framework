using RolesIS.Models;
using RolesIS.Services.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RolesIS.Controllers
{
    public class ShopCartController : Controller
    {
        public ActionResult Index()
        {
            var user = Cache.GetUser(u => u.UserName == User.Identity.Name);
            var inCartCompras = user.Compras.Where(p => p.Estado == ShopStatus.InCart).ToList();

            return View(inCartCompras);
        }

        public ActionResult Buy()
        {
            var user = Cache.GetUser(u => u.UserName == User.Identity.Name);
            var inCartCompras = user.Compras.Where(p => p.Estado == ShopStatus.InCart).ToList();
            Cache.FromCartToPaid(inCartCompras);

            return RedirectToAction("Index");
        }
    }
}