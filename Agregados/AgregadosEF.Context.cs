﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AgregadosEntities : DbContext
    {
        public AgregadosEntities()
            : base("name=AgregadosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CierreApertCajas> CierreApertCajas { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<CorreoNotificaciones> CorreoNotificaciones { get; set; }
        public virtual DbSet<DetalleFacts> DetalleFacts { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Facturas> Facturas { get; set; }
        public virtual DbSet<Materiales> Materiales { get; set; }
        public virtual DbSet<MetodosPagos> MetodosPagos { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<TipoClientes> TipoClientes { get; set; }
        public virtual DbSet<TiposFacturas> TiposFacturas { get; set; }
        public virtual DbSet<TiposProveedores> TiposProveedores { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }
    
        public virtual ObjectResult<SPFactGeneradaContadoIVA_Result> SPFactGeneradaContadoIVA(Nullable<int> idFactura)
        {
            var idFacturaParameter = idFactura.HasValue ?
                new ObjectParameter("IdFactura", idFactura) :
                new ObjectParameter("IdFactura", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFactGeneradaContadoIVA_Result>("SPFactGeneradaContadoIVA", idFacturaParameter);
        }
    }
}
