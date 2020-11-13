using RolesIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolesIS.Services.ClientManager
{
    public static class ClientManager
    {
        public static void CreateCompra(int idProducto, int cantidad, string cuenta, decimal importe, string idComprador)
        {
            var compra = new Compra
            {
                Cantidad = cantidad,
                Cuenta = cuenta,
                ProductoID = idProducto,
                Importe = importe,
                Id = idComprador,
                Estado = ShopStatus.InCart
            };

            Cache.Cache.AddOrRemoveCompra(true, compra);
            Cache.Cache.DecreaseProducto(idProducto, cantidad);
        }
    }
}