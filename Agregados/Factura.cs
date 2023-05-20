//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agregados
{
    using System;
    using System.Collections.Generic;
    
    public partial class Factura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Factura()
        {
            this.DetalleFacts = new HashSet<DetalleFact>();
        }
    
        public int IdFactura { get; set; }
        public decimal CostoTransporte { get; set; }
        public decimal IVA { get; set; }
        public decimal CostoTotal { get; set; }
        public System.DateTime FechaFactura { get; set; }
        public Nullable<System.DateTime> FechaLimiteP { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipo { get; set; }
        public int IdEstado { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdProveedor { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleFact> DetalleFacts { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Provedor Provedor { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual TipoFactura TipoFactura { get; set; }
    }
}
