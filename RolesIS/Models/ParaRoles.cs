using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolesIS.Models
{
    public class ParaRoles
    {
        public IEnumerable<ApplicationUser> Usuarios { get; set; }
        public bool[] Administrador { get; set; }
        public bool[] Vendedor { get; set; }
        public bool[] Comprador { get; set; }

        public string[] Ids { get; set; }
    }
}