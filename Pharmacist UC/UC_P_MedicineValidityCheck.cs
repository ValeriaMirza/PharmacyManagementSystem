using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementProject.Pharmacist_UC
{
    public partial class UC_P_MedicineValidityCheck : UserControl
    {
        Singleton singleton = Singleton.Instance; // Accessing the Singleton instance
        string query;

        public UC_P_MedicineValidityCheck()
        {
            InitializeComponent();
        }

        private void txtCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCheck.SelectedIndex == 0)
            {
                query = "select * from medicine where eDate >= getdate()";
                SetDataGridView(query, "Valid medicines.", Color.Green);
            }
            else if (txtCheck.SelectedIndex == 1)
            {
                query = "select * from medicine where eDate <= getdate()";
                SetDataGridView(query, "Expired medicines.", Color.Red);

            }
            else if (txtCheck.SelectedIndex == 2)
            {
                query = "select * from medicine";
                SetDataGridView(query, "All Medicines.", Color.Black);

            }
        }

        private void SetDataGridView(string query, string labelName, Color col)
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
