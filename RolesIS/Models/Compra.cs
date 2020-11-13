using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RolesIS.Models
{
    public class Compra
    {
        [Key]
        public int CompraID { get; set; }
        public int Cantidad { get; set; }
        [Display(Name = "Cuenta Bancaria")]
        public string Cuenta { get; set; }
        public decimal Importe { get; set; }
        public int ProductoID { get; set; }
        public virtual Producto Producto { get; set; }
        public string Id { get; set; }

        public virtual ApplicationUser Comprador { get; set; }
        
    }
}