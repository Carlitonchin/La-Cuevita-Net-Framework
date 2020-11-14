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
    [Authorize]
    public class ProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (!User.IsInRole("Vendedor"))
                return Content("No tienes permisos de vendedor");

            var user = Cache.GetUser(u => u.UserName == User.Identity.Name);

            return View(user);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var producto = Cache.GetProducto(p => p.ProductoID == id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoID,Nombre,Descripcion,Cantidad,Precio,Id,Cuenta")] Producto producto)
        {
            var user = Cache.GetUser(u => u.UserName == User.Identity.Name);
            producto.Id = user.Id;
            producto.Estado = ProductState.OnSale;
            if (ModelState.IsValid)
            {
                Cache.AddOrRemoveProducto(true, producto);
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "Email", producto.Id);

            return View(producto);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var producto = Cache.GetProducto(p => p.ProductoID == id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", producto.Id);

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoID,Nombre,Descripcion,Cantidad,Precio,Id,Cuenta")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", producto.Id);

            return View(producto);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var producto = Cache.GetProducto(p => p.ProductoID == id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var producto = Cache.GetProducto(p => p.ProductoID == id);
            Cache.AddOrRemoveProducto(false, producto);

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
