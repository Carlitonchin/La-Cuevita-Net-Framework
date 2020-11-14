using RolesIS.Models;
using RolesIS.Services.Cache;
using RolesIS.Services.ClientManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RolesIS.Controllers
{
    
    public class TodosProductosController : Controller
    {
        // GET: TodosProductos
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var productos = db.Productoes.Where(p => p.Estado == ProductState.OnSale).ToList();
            return View(productos);
        }

        public ActionResult IndexReportados()
        {
            if (!User.IsInRole("Admin"))
                return Content("No tienes permiso para entrar aqui");
            var productos = db.Productoes.Where(p => p.Estado == ProductState.Suspended).ToList();
            return View(productos);
        }

        public ActionResult Comprar(int? ProductoId)
        {
            if (ProductoId == null)
                return Content("Error");
            var producto = Cache.GetProducto(p => p.ProductoID == ProductoId);
            if (producto == null)
                return Content("Producto no existente, habla con el proveedor");

            return View(producto);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AnadirACarrito (int? idProducto, int? cant)
        {
            if (idProducto == null || cant == null)
                return Content("Error");

            var producto = Cache.GetProducto(p => p. ProductoID == idProducto);

            if (producto == null)
                return Content("Producto inexistente");

            if (cant > producto.Cantidad)
                return Content("No se dispone de esa cantidad de " + producto.Nombre);


            var user = Cache.GetUser(u => u.UserName == User.Identity.Name);
            ClientManager.CreateCompra(idProducto.Value, cant.Value, "", producto.Precio * cant.Value, user.Id);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
         public ActionResult ConfirmarCompra(int? idProducto, int? cantidad, string cuenta, decimal? importe)
         {
            if (idProducto == null || cantidad == null || cuenta == null || importe == null)
                return Content("Error");

            var producto = Cache.GetProducto(p => p.ProductoID == idProducto);

            if (producto == null)
                return Content("Producto inexistente");

            if (cantidad > producto.Cantidad)
                return Content("No se dispone de esa cantidad de " + producto.Nombre);

            var idComprador = Cache.GetUser(u => u.UserName == User.Identity.Name).Id;
            ClientManager.CreateCompra((int)idProducto, (int)cantidad, cuenta, (decimal)importe, idComprador);
           
            return RedirectToAction("Index", "Compras");
         }

        public ActionResult Reportar(int? idProducto)
        {
            if (idProducto == null)
                return Content("Producto no encontrado");

            var producto = db.Productoes.SingleOrDefault(p => p.ProductoID == idProducto.Value);
            if (producto == null)
                return Content("Producto no encontrado");

            producto.Estado = ProductState.Suspended;
            producto.UsuarioQueReporta = User.Identity.Name;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Autorizar(int? idProducto)
        {
            if (!User.IsInRole("Admin"))
                return Content("No tienes permiso para entrar aqui");

            if (idProducto == null)
                return Content("Producto no encontrado");

            var producto = db.Productoes.SingleOrDefault(p => p.ProductoID == idProducto.Value);
            if (producto == null)
                return Content("Producto no encontrado");

            producto.Estado = ProductState.OnSale;
            producto.UsuarioQueReporta = "";
            db.SaveChanges();

            return RedirectToAction("IndexReportados");
        }
    }
}