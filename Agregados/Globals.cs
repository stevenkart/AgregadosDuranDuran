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


        public static Form MyPrincipalForm = new Forms.FrmPrincipalMDI();

        public static Usuarios MyGlobalUser = new Agregados.Usuarios();

        public static Forms.Notifications.FrmNotifications MifrmNotifications = new Forms.Notifications.FrmNotifications();

        public static Forms.Help.FrmInfo MifrmInfo = new Forms.Help.FrmInfo();

        public static Forms.Vehicles.FrmVehiclesManage MifrmVehiclesManage = new Forms.Vehicles.FrmVehiclesManage();

        public static Forms.Users.FrmUserManage MifrmUser = new Forms.Users.FrmUserManage();

        public static Forms.Customers.FrmCustomersManage MifrmCustomers = new Forms.Customers.FrmCustomersManage();

        public static Forms.Providers.FrmProvidersManage MifrmProviders = new Forms.Providers.FrmProvidersManage();

        public static Forms.Materials.FrmMaterialsManage MifrmMaterials = new Forms.Materials.FrmMaterialsManage();

        public static Forms.Bills.FrmBillAdd MifrmBillAdd = new Forms.Bills.FrmBillAdd();

        public static Forms.Bills.FrmBillProviderAdd MifrmBillProviderAdd = new Forms.Bills.FrmBillProviderAdd();

        public static Forms.Bills.FrmRevFacts MifrmRevFacts = new Forms.Bills.FrmRevFacts();

        public static Forms.Bills.FrmNotaCredito MifrmNotaCredito = new Forms.Bills.FrmNotaCredito();

        public static Forms.Cashiers.FrmCashierOpen MifrmCashierOpen = new Forms.Cashiers.FrmCashierOpen();

        public static Forms.Cashiers.FrmCashierClose MifrmCashierClose = new Forms.Cashiers.FrmCashierClose();

        public static Forms.Reports.FrmFactsReports MifrmFactsReports = new Forms.Reports.FrmFactsReports();

        
    }
}
