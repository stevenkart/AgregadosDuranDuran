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
    
        public virtual DbSet<CierreCaja> CierreCajas { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<CorreoNotificacion> CorreoNotificacions { get; set; }
        public virtual DbSet<DetalleFact> DetalleFacts { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Provedor> Provedors { get; set; }
        public virtual DbSet<TipoFactura> TipoFacturas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Vehiculo> Vehiculos { get; set; }
    }
}
