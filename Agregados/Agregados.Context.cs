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
        public virtual DbSet<Denominaciones> Denominaciones { get; set; }
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
    
        public virtual ObjectResult<SPCierreCajaPend_Result> SPCierreCajaPend()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPCierreCajaPend_Result>("SPCierreCajaPend");
        }
    
        public virtual ObjectResult<SPCierreCajaPorFecha_Result> SPCierreCajaPorFecha(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPCierreCajaPorFecha_Result>("SPCierreCajaPorFecha", fechaInicioParameter, fechaFinParameter);
        }
    
        public virtual ObjectResult<SPCierreCajaPorId_Result> SPCierreCajaPorId(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPCierreCajaPorId_Result>("SPCierreCajaPorId", iDParameter);
        }
    
        public virtual ObjectResult<SPFactGenerada_Result> SPFactGenerada(Nullable<int> consecutivo)
        {
            var consecutivoParameter = consecutivo.HasValue ?
                new ObjectParameter("Consecutivo", consecutivo) :
                new ObjectParameter("Consecutivo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFactGenerada_Result>("SPFactGenerada", consecutivoParameter);
        }
    
        public virtual ObjectResult<SPFactPendAll_Result> SPFactPendAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFactPendAll_Result>("SPFactPendAll");
        }
    
        public virtual ObjectResult<SPFactPendDetalles_Result> SPFactPendDetalles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFactPendDetalles_Result>("SPFactPendDetalles");
        }
    
        public virtual ObjectResult<SPFactPendSinDetalle_Result> SPFactPendSinDetalle()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFactPendSinDetalle_Result>("SPFactPendSinDetalle");
        }
    
        public virtual ObjectResult<SPFactPorRangoFechaAll_Result> SPFactPorRangoFechaAll(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFactPorRangoFechaAll_Result>("SPFactPorRangoFechaAll", fechaInicioParameter, fechaFinParameter);
        }
    
        public virtual ObjectResult<SPFactPorRangoFechaDetalles_Result> SPFactPorRangoFechaDetalles(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFactPorRangoFechaDetalles_Result>("SPFactPorRangoFechaDetalles", fechaInicioParameter, fechaFinParameter);
        }
    
        public virtual ObjectResult<SPFactPorRangoFechaSinDetalles_Result> SPFactPorRangoFechaSinDetalles(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFactPorRangoFechaSinDetalles_Result>("SPFactPorRangoFechaSinDetalles", fechaInicioParameter, fechaFinParameter);
        }
    
        public virtual ObjectResult<SPFactReversadasAll_Result> SPFactReversadasAll(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFactReversadasAll_Result>("SPFactReversadasAll", fechaInicioParameter, fechaFinParameter);
        }
    
        public virtual ObjectResult<SPTicketGenerado_Result> SPTicketGenerado(Nullable<int> consecutivo)
        {
            var consecutivoParameter = consecutivo.HasValue ?
                new ObjectParameter("Consecutivo", consecutivo) :
                new ObjectParameter("Consecutivo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPTicketGenerado_Result>("SPTicketGenerado", consecutivoParameter);
        }
    
        public virtual ObjectResult<SPTicketPendAll_Result> SPTicketPendAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPTicketPendAll_Result>("SPTicketPendAll");
        }
    
        public virtual ObjectResult<SPTicketPorRangoFechaAll_Result> SPTicketPorRangoFechaAll(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPTicketPorRangoFechaAll_Result>("SPTicketPorRangoFechaAll", fechaInicioParameter, fechaFinParameter);
        }
    
        public virtual ObjectResult<SPTicketReversadasAll_Result> SPTicketReversadasAll(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPTicketReversadasAll_Result>("SPTicketReversadasAll", fechaInicioParameter, fechaFinParameter);
        }
    }
}
