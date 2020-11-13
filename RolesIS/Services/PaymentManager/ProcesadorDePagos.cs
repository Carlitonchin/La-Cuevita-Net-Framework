using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolesIS.Utils
{
    public class ProcesadorDePagos
    {
        public ProcesadorDePagos()
        {

        }

        public void CrearCanalSeguro()
        {

        }

        public bool RealizarPago(Object peticion)
        {
            Random r = new Random();
            int n = r.Next(5);
            if (n == 0)
                return false;
            return true;
        }
    }
}