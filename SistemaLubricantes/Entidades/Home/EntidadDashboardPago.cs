using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Home
{
    public class EntidadDashboardPago
    {
        public EntidadEfectivo objEfectivo { get; set; }
        public EntidadTarjeta objTarjeta { get; set; }
        public EntidadYape objYape { get; set; }
        public EntidadPlin objPlin { get; set; }
        public EntidadCredito objCredito { get; set; }
        public EntidadContado objContado { get; set; }
    }
    public class EntidadEfectivo
    {
        public string codigo { get; set; }
        public string tipoPago { get; set; }
        public decimal montoPago { get; set; }
    }
    public class EntidadTarjeta 
    {
        public string codigo { get; set; }
        public string tipoPago { get; set; }
        public decimal montoPago { get; set; }
    }
    public class EntidadYape
    {
        public string codigo { get; set; }
        public string tipoPago { get; set; }
        public decimal montoPago { get; set; }
    }
    public class EntidadPlin 
    {
        public string codigo { get; set; }
        public string tipoPago { get; set; }
        public decimal montoPago { get; set; }
    }
    public class EntidadCredito
    {
        public string codigo { get; set; }
        public string tipoPago { get; set; }
        public decimal montoPago { get; set; }
    }
    public class EntidadContado
    {
        public string codigo { get; set; }
        public string tipoPago { get; set; }
        public decimal montoPago { get; set; }
    }
}
