using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolesIS.Services.PaymentManager
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
            return true;
        }

        public bool FondosSuficientes(Object peticion)
        {
            Random r = new Random();
            int n = r.Next(5);
            if (n == 0)
                return false;
            return true;
        }
    }
}