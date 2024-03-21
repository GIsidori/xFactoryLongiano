using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace XFactoryNET.Module.Win
{
    public partial class frmFormatConditions : Form
    {
        public frmFormatConditions(GridView view)
        {
            InitializeComponent();
            expressionConditionsEditor1.Init(view);
        }
    }
}
