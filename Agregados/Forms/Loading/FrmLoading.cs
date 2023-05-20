using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agregados.Forms.Loading
{
    public partial class FrmLoading : Form
    {
        public Action Action { get; set; }
        public FrmLoading(Action action)
        {
            InitializeComponent();
            if (action == null)
            {
                throw new ArgumentNullException();
            }
            Action = action;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(Action).ContinueWith(s => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
