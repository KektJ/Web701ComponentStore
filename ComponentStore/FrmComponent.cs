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
    public partial class FrmComponent : Form
    {


        public clsComponent _Component;
        public static FrmComponent Instance;
        private static Dictionary<string, FrmComponent> _ComponentFormList =
new Dictionary<string, FrmComponent>();
        public FrmComponent()
        {
            InitializeComponent();
            cbType.SelectedIndex = 0;
        }

        public void UpdateComponents()
        {
            lstComponents.DataSource = null;
            if (_Component.ComponentList != null)
                lstComponents.DataSource = _Component.ComponentList;

        }

        public static void Run(string prComponentName)
        {
            FrmComponent lcComponentForm;
            if (string.IsNullOrEmpty(prComponentName) ||
            !_ComponentFormList.TryGetValue(prComponentName, out lcComponentForm))
            {
                lcComponentForm = new FrmComponent();
                if (string.IsNullOrEmpty(prComponentName))
                    lcComponentForm.SetDetails(new clsComponent());
                else
                {
                    _ComponentFormList.Add(prComponentName, lcComponentForm);
                    lcComponentForm.refreshFormFromDB(prComponentName);
                    



                }
            }
            else
            {

                lcComponentForm.ShowDialog();
                lcComponentForm.Activate();
            }


        }


        private async void refreshFormFromDB(string prComponentName)
        {
            SetDetails(await ServiceClient.GetComponentAsync(prComponentName));
        }

        public void SetDetails(clsComponent prComponent)
        {
            _Component = prComponent;
            
            UpdateComponents();
            UpdateForm();

        }


        private void UpdateForm()
        {
            lblComponentName.Text = _Component.Name;

        }

        private void lstComponents_DoubleClick(object sender, EventArgs e)
        {
            FrmProduct.DispatchComponentForm(lstComponents.SelectedValue as clsAllComponents);
        }

        private void FrmComponent_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void FrmComponent_Activated(object sender, EventArgs e)
        {
            UpdateComponents();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clsAllComponents lcComponent = clsAllComponents.NewComponent(cbType.SelectedItem.ToString());
            lcComponent.tblCategoryName = _Component.Name;
           
            FrmProduct.DispatchComponentForm(lcComponent);
            if (!string.IsNullOrEmpty(lcComponent.Name))
            {
                refreshFormFromDB(_Component.Name);
                //UpdateComponents();
            }

        }



        private async void btnDelete_Click(object sender, EventArgs e)
        {
            int lcIndex = lstComponents.SelectedIndex;

            if (lcIndex >= 0 && MessageBox.Show("The selected component will be deleted, are you sure you want to continue?", "Delete Component", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show(await ServiceClient.DeleteComponentAsync(lstComponents.SelectedItem as clsAllComponents));
                refreshFormFromDB(_Component.Name);
                UpdateComponents();

            }


        }

        private void FrmComponent_Enter(object sender, EventArgs e)
        {
            UpdateComponents();
        }
    }
}
