using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComponentStore
{
    public partial class FrmOrders : Form
    {
        public FrmOrders()
        {
            InitializeComponent();
            UpdateDisplay();
        }

        private async void UpdateDisplay()
        {

            lstOrders.DataSource = null;
            try
            {
                lstOrders.DataSource = await ServiceClient.GetOrdersAsync();
            }

            catch
            {
                MessageBox.Show("Could not retrieve Category list from datasouce. Please ensure the store host is running and try again");
                this.Close();
            }
        }

        private void lstOrders_Click(object sender, EventArgs e)
        {
            UpdateOrderLabels();


        }

        private async void UpdateOrderLabels()
        {
            clsOrder Order = await ServiceClient.GetOrderDetailsAsync(Convert.ToInt32(lstOrders.SelectedValue));
            lblCustomerName.Text = Order.CustomerName.ToString();
            lblCustomerEmail.Text = Order.CustomerEmail.ToString();
            lblQuantity.Text = Order.Quantity.ToString();
            lblTotal.Text = Order.Total.ToString();

            clsAllComponents lccomponent = await ServiceClient.GetComponentDetails(Order.tblProductID);
            lblProductName.Text = lccomponent.Name;
            lblBrand.Text = lccomponent.Brand;
           // lblWarranty.Text = Convert.ToString(lccomponent.Warranty);
            lblType.Text = lccomponent.Type;
            string warranty = Convert.ToString(lccomponent.Warranty);
            string condition = lccomponent.Condition;
            if (lblType.Text == "NEW")
            {
                
                lblVariable.Text = "Warranty Length";
                lblVariableContent.Text = warranty;
            }
            if (lblType.Text == "USED")
            {
                lblVariable.Text = "Condition";
                lblVariableContent.Text = condition;
            }



            //clsComponent lccomponent = await ServiceClient.GetComponentAsync(Convert.ToString((lstOrders.SelectedValue)));
            //lccomponent.ComponentList.
            //lblProductName.Text = lccomponent.

        }


    }
}
