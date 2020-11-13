using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolesIS.Utils
{
    public class PasarelaDePagos
    {
        public bool HacerTransferencia(string cuentaOrigen, string cuentaDestino, decimal monto)
        {
            var origenEnc = EncriptarCuenta(cuentaOrigen);
            var destinoEnc = EncriptarCuenta(cuentaDestino);

            var peticionSegura = CrearPeticionSegura(origenEnc, destinoEnc, monto);

            var p = new ProcesadorDePagos();
            p.CrearCanalSeguro();
            
            bool transaccionExitosa = p.RealizarPago(peticionSegura);
            return transaccionExitosa;
        }

        private string EncriptarCuenta(string cuenta)
        {
            return cuenta;
        }

        private Object CrearPeticionSegura(string origenEncriptado, string destinoEncriptado, decimal monto)
        {
            return new Object();
        }
    }
}