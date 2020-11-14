using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RolesIS.Models
{
    public class Producto
    {
        [Key]
        public int ProductoID { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Id { get; set; }
        public virtual ApplicationUser Anunciante
        {
            get; set;
        }
        
        public string Cuenta { get; set; }
        
        public virtual ICollection<Compra> Compras { get; set; }

        public ProductState Estado { get; set; }
    }
    public enum ProductState
    {
        OnSale, Suspended
    }
}