using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolesIS.Models
{
    public class SubastaViewModel
    {
        public Subasta subasta;
        public List<Producto> productos;
        public List<Producto> productosSeleccionados;
        public List<int> cantidades;
    }
}