using RolesIS.Models;
using RolesIS.Services.Cache;
using RolesIS.Services.PaymentManager;
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
            decimal price = 0;
            foreach (var item in user.Compras.Where(p => p.Estado == ShopStatus.InCart).ToList())
            {
                price += item.Importe;
            };

            ViewBag.Price = price;
            return View("ConfirmarCuenta");
        }
        [HttpPost]
        public ActionResult Buy(string cuenta)
        {
            var p = new PasarelaDePagos();
            
            var user = Cache.GetUser(u => u.UserName == User.Identity.Name);
            var inCartCompras = user.Compras.Where(c => c.Estado == ShopStatus.InCart).ToList();
            decimal price = 0;
            foreach (var item in inCartCompras)
                price += item.Importe;

            if (!p.SaldoSuficiente(cuenta, price))
                return Content("No se dispone de saldo suficiente");

            foreach (var item in inCartCompras)
                p.HacerTransferencia(cuenta, item.Producto.Cuenta, item.Importe);

            Cache.FromCartToPaid(inCartCompras, cuenta);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? compraId)
        {
            if (compraId == null)
                return Content("Compra no encontrada");

            var compra = Cache.GetCompra(c => c.CompraID == compraId);

            Cache.IncreaseProducto(compra.ProductoID, compra.Cantidad);
            Cache.AddOrRemoveCompra(false, compra);

            return RedirectToAction("Index");
        }
    }
}