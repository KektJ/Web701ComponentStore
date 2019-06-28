using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComponentStore
{
    public partial class FrmProduct : Form
    {
        protected clsAllComponents _Component;
        public FrmProduct()
        {
            InitializeComponent();
        }

        public void SetDetails(clsAllComponents prComponent)
        {
            
            _Component = prComponent;
            txtType.Enabled = false;
            updateForm();
            ShowDialog();
        }

        protected virtual void updateForm()
        {
           
            try
            {
                txtName.Enabled = string.IsNullOrEmpty(_Component.Name);
                txtName.Text = _Component.Name;
                NumPrice.Value = Convert.ToDecimal(_Component.Price);
                NumQuantity.Value = Convert.ToDecimal(_Component.Quantity);
                txtBrand.Text = _Component.Brand.ToString();
                txtType.Text = _Component.Type.ToString();
                DTPModified.Value = _Component.Modified;

            }

            catch {

            }

        }

        public delegate void LoadWorkFormDelegate(clsAllComponents prComponent);
        public static Dictionary<string, Delegate> _ComponentForm = new Dictionary<string, Delegate>
        {
            { "NEW", new LoadWorkFormDelegate(FrmNew.Run)},
            { "USED", new LoadWorkFormDelegate(FrmUsed.Run)}
        };
        public static void DispatchComponentForm(clsAllComponents prComponent)
        {
            _ComponentForm[prComponent.Type].DynamicInvoke(prComponent);
        }


        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (isValid() == true)
            {
                if (MessageBox.Show("Component will be saved with the entered attributes, do you wish to continue", "Add component", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    pushData();
                    if (txtName.Enabled)
                        MessageBox.Show(await ServiceClient.InsertComponentAsync(_Component));
                    else
                        MessageBox.Show(await ServiceClient.UpdateComponentAsnc(_Component));
                }
            }

            else
            {
                MessageBox.Show("Please ensure all fields are Filled out before saving");
            }

        }
        protected virtual void pushData()
        {
            _Component.Name = txtName.Text;
            _Component.Price = Convert.ToSingle(NumPrice.Value);
            _Component.Quantity = Convert.ToSingle(NumQuantity.Value);
            _Component.Brand = txtBrand.Text;
            _Component.Type = txtType.Text;
            _Component.Modified = DateTime.Now;

        }

        private Boolean isValid()
        {
            string lcName = txtName.Text;
            string lcBrand = txtBrand.Text;
            if (String.IsNullOrEmpty(lcName)
                )
            {
                return false;
            }
            if (String.IsNullOrEmpty(lcBrand))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBrand_KeyPress(object sender, KeyPressEventArgs e)
        {
    //            var regex = new Regex(@"[^a-zA-Z0-9\s-\._[\b]]");
    //        if (regex.IsMatch(e.KeyChar.ToString()))
    //{
    //    e.Handled = true;
    
        }
    }
}
