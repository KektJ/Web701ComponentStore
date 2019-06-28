using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComponentStore
{
    public partial class FrmUsed : ComponentStore.FrmProduct
    {
        public static readonly FrmUsed Instance = new FrmUsed();
        public FrmUsed()
        {
            InitializeComponent();
            cbCondition.SelectedIndex = 0;
        }

        public static void Run(clsAllComponents prComponent)
        {
            Instance.SetDetails(prComponent);
        }

        protected override void updateForm()
        {
            base.updateForm();
            cbCondition.SelectedText = _Component.Condition;

        }

        protected override void pushData()
        {
            base.pushData();
            _Component.Condition = cbCondition.SelectedText.ToString();
        }
    }
}
