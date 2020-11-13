using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RolesIS.Models
{
    public class Cantidades
    {
        [Key]
        public int CantidadesID { get; set; }

        public int valor { get; set; }
        public int SubastaID { get; set; }
        public virtual Subasta Subasta {get ; set;}
    }
}