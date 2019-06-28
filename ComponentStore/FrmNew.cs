using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComponentStore
{
    public partial class FrmNew : ComponentStore.FrmProduct
    {
        public static readonly FrmNew Instance = new FrmNew();
        public FrmNew()
        {
            InitializeComponent();
        }

        public static void Run(clsAllComponents prComponent)
        {
            Instance.SetDetails(prComponent);
        }

        protected override void updateForm()
        {
            base.updateForm();
            NUMWarranty.Value = Convert.ToDecimal(_Component.Warranty);

        }

        protected override void pushData()
        {
            base.pushData();
            //_Component.Warranty = float.Parse(txtWarranty.Text);
            _Component.Warranty = Convert.ToSingle(NUMWarranty.Value);
           // _Component.Warranty = float.Parse(txtWarranty.Text);

            //txtName.Text = _Component.Name;
            //txtPrice.Text = _Component.Price.ToString();
            //txtQuantity.Text = _Component.Price.ToString();
            //txtBrand.Text = _Component.Brand.ToString();
            //txtType.Text = _Component.Type.ToString();
            //txtModified.Text = _Component.Modified.ToString();
            //  _Work.Date = DateTime.Parse(txtCreation.Text);
            // _Work.Value = decimal.Parse(txtValue.Text);
        }
    }
}
