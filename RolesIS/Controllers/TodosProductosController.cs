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
            return View(Cache.Productos);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return Content("Error");
            var producto = Cache.GetProducto(p => p.ProductoID == id);
            if (producto == null)
                return Content("Producto no existente, habla con el proveedor");

            return View(producto);
        }

        [Authorize]
        public ActionResult Comprar(int? idProducto)
        {
            if (idProducto == null)
                return Content("requiere producto");

            var producto = Cache.GetProducto(p => p. ProductoID == idProducto);

            if (producto == null)
                return Content("Producto inexistente");

            return View(producto);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Comprar(int? idProducto, int? cant)
        {
            if (idProducto == null || cant == null)
                return Content("Error");

            var producto = Cache.GetProducto(p => p. ProductoID == idProducto);

            if (producto == null)
                return Content("Producto inexistente");

            if (cant > producto.Cantidad)
                return Content("No se dispone de esa cantidad de " + producto.Nombre);

            ViewBag.Cantidad = cant;

            return View("Cuenta", producto);
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


            /*Compra compra = new Compra();
            compra.Cantidad = (int)cantidad;
            compra.Id = db.Users.First(u => u.UserName == User.Identity.Name).Id;
            compra.ProductoID = (int)idProducto;
            compra.Cuenta = cuenta;
            compra.Importe = (decimal)importe;
            ProductosController pController = new ProductosController();
            
            new ComprasController().Create(compra);

            producto.Cantidad -= (int)cantidad;
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();*/
           
            return RedirectToAction("Index", "Compras");
         }
    }
}