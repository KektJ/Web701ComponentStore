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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            UpdateDisplay();
            
        }

        private async void UpdateDisplay()
        {
           
            lstCategorys.DataSource = null;
            try
            {
                lstCategorys.DataSource = await ServiceClient.GetCategoryNameAsync();
            }

            catch
            {
                MessageBox.Show("Could not retrieve Category list from datasouce. Please ensure the store host is running and try again");
                this.Close();
            }
        }

        private void lstCategorys_DoubleClick(object sender, EventArgs e)
        {
            FrmComponent.Run(lstCategorys.SelectedItem as string);
           // this.Hide();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FrmComponent.Run(lstCategorys.SelectedItem as string);
           // this.Hide();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            FrmOrders OrderForm = new FrmOrders();
            OrderForm.ShowDialog();
        }
    }
}
