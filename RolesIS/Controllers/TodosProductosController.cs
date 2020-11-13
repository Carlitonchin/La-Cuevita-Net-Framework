using RolesIS.Models;
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
            return View(db.Productoes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return Content("Error");
            Producto p = db.Productoes.Find(id);
            if (p == null)
                return Content("Producto no existente, habla con el proveedor");
            return View(p);
        }

        [Authorize]
        public ActionResult Comprar(int? idProducto)
        {
            if (idProducto == null)
                return Content("requiere producto");

            Producto p = db.Productoes.Find(idProducto);

            if (p == null)
                return Content("Producto inexistente");

            return View(p);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Comprar(int? idProducto, int? cant)
        {
            if (idProducto == null || cant == null)
                return Content("Error");

            Producto p = db.Productoes.Find(idProducto);

            if (p == null)
                return Content("Producto inexistente");

            if (cant > p.Cantidad)
                return Content("No se dispone de esa cantidad de " + p.Nombre);

            ViewBag.Cantidad = cant;

            return View("Cuenta", p);
        }

        [Authorize]
        [HttpPost]
     public ActionResult ConfirmarCompra(int? idProducto, int? cantidad, string cuenta, decimal? importe)
     {
            if (idProducto == null || cantidad == null || cuenta == null || importe == null)
                return Content("Error");

            Producto p = db.Productoes.Find(idProducto);

            if (p == null)
                return Content("Producto inexistente");

            if (cantidad > p.Cantidad)
                return Content("No se dispone de esa cantidad de " + p.Nombre);

            Compra compra = new Compra();
            compra.Cantidad = (int)cantidad;
            compra.Id = db.Users.First(u => u.UserName == User.Identity.Name).Id;
            compra.ProductoID = (int)idProducto;
            compra.Cuenta = cuenta;
            compra.Importe = (decimal)importe;
            ProductosController pController = new ProductosController();
            
            new ComprasController().Create(compra);

           
                p.Cantidad -= (int)cantidad;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
           
            return RedirectToAction("Index", "Compras");
        }

        public ActionResult Carrito(Producto p, List<int> idProductos)
        {
            if(p != null)
            {
                if (idProductos == null)
                    idProductos = new List<int>();

            }
            ViewData["asd"] = "asddas";
            return View();
        }
    }
}