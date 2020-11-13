using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RolesIS.Models
{
    public class Subasta
    {
        [Key]
        public int SubastaID { get; set; }
        public string Id { get; set; }
        public virtual ApplicationUser Anunciante { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<Cantidades> Cantidades { get; set; }
        public DateTime? tiempoPublicacion { get; set; }
       
        public decimal PrecioInicial { get; set; }
        public string Id2 { get; set; }
        public virtual ApplicationUser CompradorActual { get; set; }
        public DateTime? tiempoUltimaPuja { get; set; }
        public decimal PrecioActual { get; set; }
        public double TiempoRestanteParaComenzar { get; set; }
        public bool SubastaTerminada { get; set; }

        public string CuentaDelComprador { get; set; }
        public string CuentaDelVendedor { get; set; }
    }
}