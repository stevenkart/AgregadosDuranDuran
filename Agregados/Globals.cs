﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados
{
    public class Globals
    {
        //los objetos seran accesible en su totalidad por la aplicacion ademas 
        //esta clase se instancia automaticamente

        //el form principal se puede invocar desde cualquier lugar 
        //(login en nuestro caso)

        // boolean para dar o no notificaciones al usuario al momento de loguearse en la app, "true" = dar notificaciones && "false" = no dar notifications
        public static bool Notifications = true;

        // boolean para dar o no la indicacion de que el form esta abierto y asi no volverlo abrir
        public static bool AcercaDe = false;

        public static bool Manual = false;

        public static bool Bitacora = false;

        //forms

        public static Form MyPrincipalForm = new Forms.FrmPrincipalMDI();

        public static Usuarios MyGlobalUser = new Agregados.Usuarios();

        public static Forms.Notifications.FrmNotifications MifrmNotifications = new Forms.Notifications.FrmNotifications();

        public static Forms.Help.FrmInfo MifrmInfo = new Forms.Help.FrmInfo();

        public static Forms.Help.FrmManualUser MifrmManualUser = new Forms.Help.FrmManualUser();

        public static Forms.Vehicles.FrmVehiclesManage MifrmVehiclesManage = new Forms.Vehicles.FrmVehiclesManage();

        public static Forms.Vehicles.FrmBitVehiculeList MifrmBitVehiculeList = new Forms.Vehicles.FrmBitVehiculeList(0);

        public static Forms.Users.FrmUserManage MifrmUser = new Forms.Users.FrmUserManage();

        public static Forms.Customers.FrmCustomersManage MifrmCustomers = new Forms.Customers.FrmCustomersManage();

        public static Forms.Providers.FrmProvidersManage MifrmProviders = new Forms.Providers.FrmProvidersManage();

        public static Forms.Materials.FrmMaterialsManage MifrmMaterials = new Forms.Materials.FrmMaterialsManage();

        public static Forms.Bills.FrmBillAdd MifrmBillAdd = new Forms.Bills.FrmBillAdd();

        //public static Forms.Bills.FrmDetalleIVA MifrmDetalleIVA = new Forms.Bills.FrmDetalleIVA();

        public static Forms.Bills.FrmBillProviderAdd MifrmBillProviderAdd = new Forms.Bills.FrmBillProviderAdd();

        public static Forms.Bills.FrmRevFacts MifrmRevFacts = new Forms.Bills.FrmRevFacts();

        public static Forms.Bills.FrmRevBillProvider MifrmRevBillProvider = new Forms.Bills.FrmRevBillProvider();

        public static Forms.Bills.FrmNotaCredito MifrmNotaCredito = new Forms.Bills.FrmNotaCredito();

        public static Forms.Bills.FrmNotaDebito MifrmNotaDebito = new Forms.Bills.FrmNotaDebito();

        public static Forms.Bills.FrmPendBill MifrmPendBill = new Forms.Bills.FrmPendBill();

        public static Forms.Bills.FrmPendTicketProvider MifrmPendTicketProvider = new Forms.Bills.FrmPendTicketProvider();

        public static Forms.Cashiers.FrmCashierOpen MifrmCashierOpen = new Forms.Cashiers.FrmCashierOpen();

        public static Forms.Cashiers.FrmCashierClose MifrmCashierClose = new Forms.Cashiers.FrmCashierClose(1);//caja normal

        public static Forms.Reports.FrmFactsReports MifrmFactsReports = new Forms.Reports.FrmFactsReports();

        public static Forms.Reports.FrmTicketsReports MifrmTicketsReports = new Forms.Reports.FrmTicketsReports();

        public static Forms.Reports.FrmCajaReports MifrmCajaReports = new Forms.Reports.FrmCajaReports();

    }
}
