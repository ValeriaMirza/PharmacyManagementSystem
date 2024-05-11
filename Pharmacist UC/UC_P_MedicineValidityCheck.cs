using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementProject.Pharmacist_UC
{
    public partial class UC_P_MedicineValidityCheck : UserControl
    {
        Singleton singleton = Singleton.Instance; // Accessing the Singleton instance
        IMedicineState currentState;

        public UC_P_MedicineValidityCheck()
        {
            InitializeComponent();
            currentState = new ValidState();
        }

        private void txtCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCheck.SelectedIndex == 0)
            {
                currentState = new ValidState();
            }
            else if (txtCheck.SelectedIndex == 1)
            {
                currentState = new ExpiredState();
            }
            else if (txtCheck.SelectedIndex == 2)
            {
                currentState = null; // Reset state
                string query = "select * from medicine";
                SetDataGridView(query, "All Medicines.", Color.Black);
            }

            if (currentState != null)
            {
                currentState.HandleState(this);
            }
        }

        public void SetDataGridView(string query, string labelName, Color col)
        {
            DataSet ds = singleton.GetData(query); // Using the Singleton instance
            guna2DataGridView1.DataSource = ds.Tables[0];
            setLabel.Text = labelName;
            setLabel.ForeColor = col;
        }

        private void UC_P_MedicineValidityCheck_Load(object sender, EventArgs e)
        {
            setLabel.Text = "";
        }
    }
}
