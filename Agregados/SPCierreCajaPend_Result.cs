//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agregados
{
    using System;
    
    public partial class SPCierreCajaPend_Result
    {
        public int IdCierreApert { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public Nullable<System.DateTime> FechaSalida { get; set; }
        public string HoraSalida { get; set; }
        public string Detalles { get; set; }
        public decimal MontoEfectivoInicio { get; set; }
        public decimal MontoEfectivoUsuarioInicio { get; set; }
        public decimal MontoEfectivoFinal { get; set; }
        public decimal MontoEfectivoUsuarioFin { get; set; }
        public decimal MontoVentaEfectivo { get; set; }
        public decimal MontoCompraEfectivo { get; set; }
        public decimal MontoTransf { get; set; }
        public decimal MontoCompraTransf { get; set; }
        public decimal MontoSinpe { get; set; }
        public decimal MontoCompraSinpe { get; set; }
        public decimal MontoCheque { get; set; }
        public decimal MontoCredito { get; set; }
        public decimal MontoCompraCredito { get; set; }
        public decimal FaltanteInicio { get; set; }
        public decimal SobranteInicio { get; set; }
        public decimal FaltanteFin { get; set; }
        public decimal SobranteFin { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreEmpleado { get; set; }
    }
}
