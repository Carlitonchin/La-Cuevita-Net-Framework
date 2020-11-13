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

        public static List<Producto> FilterProductos(string text, int? minPrice, int? maxPrice, int? amount)
        {
            var filteredProductos = new List<Producto>();

            foreach (var producto in Cache.Cache.Productos)
                if (producto.Nombre.ToUpper().Contains(text.ToUpper()))
                    filteredProductos.Add(producto);

            if (minPrice != null)
                filteredProductos.RemoveAll(p => p.Precio < minPrice);
            if(maxPrice != null)
                filteredProductos.RemoveAll(p => p.Precio > maxPrice);
            if (amount != null)
                filteredProductos.RemoveAll(p => p.Cantidad < amount);

            return filteredProductos;
        }
    }
}
