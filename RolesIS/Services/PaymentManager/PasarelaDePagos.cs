using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolesIS.Services.PaymentManager
{
    public class PasarelaDePagos
    {
        public bool HacerTransferencia(string cuentaOrigen, string cuentaDestino, decimal monto)
        {
            var p = new ProcesadorDePagos();
            p.CrearCanalSeguro();

            var origenEnc = EncriptarCuenta(cuentaOrigen);
            var destinoEnc = EncriptarCuenta(cuentaDestino);
            var peticionTransf = PeticionTransfSegura(origenEnc, destinoEnc, monto);

            bool transaccionExitosa = p.RealizarPago(peticionTransf);

            return transaccionExitosa;
        }

        public bool SaldoSuficiente(string cuentaOrigen, decimal monto)
        {
            var p = new ProcesadorDePagos();
            p.CrearCanalSeguro();

            var origenEnc = EncriptarCuenta(cuentaOrigen);
            var peticionFondos = PeticionVerifFondosSegura(origenEnc, monto);

            bool fondosSuficientes = p.FondosSuficientes(peticionFondos);

            return fondosSuficientes;
        }

        private string EncriptarCuenta(string cuenta)
        {
            return cuenta;
        }

        private Object PeticionTransfSegura(string origenEncriptado, string destinoEncriptado, decimal monto)
        {
            return new Object();
        }

        private Object PeticionVerifFondosSegura(string origenEncriptado, decimal monto)
        {
            return new Object();
        }
    }
}