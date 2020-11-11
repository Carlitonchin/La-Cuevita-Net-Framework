using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RolesIS.Models;
using RolesIS.Services.Cache;

namespace RolesIS.Controllers
{
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var user = Cache.GetUser(u => u.UserName == User.Identity.Name);
            if (user == null)
                return Content("Error");

            ViewBag.usuario = User.Identity.Name;

            return View(user.Compras.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompraID,Cantidad,Cuenta,ProductoID,Id")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                Cache.AddOrRemoveCompra(true, compra);

                return RedirectToAction("Index", "Compras");
            }

            return Content("Error");
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var compra = Cache.GetCompra(c => c.CompraID == id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var compra = Cache.GetCompra(c => c.CompraID == id);
            Cache.AddOrRemoveCompra(false, compra);

            return RedirectToAction("Index");
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
