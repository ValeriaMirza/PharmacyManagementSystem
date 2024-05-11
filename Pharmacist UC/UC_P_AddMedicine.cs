using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementProject.Pharmacist_UC
{
    public partial class UC_P_AddMedicine : UserControl
    {
        function fn = new function();
        String query;
        public UC_P_AddMedicine()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtMedID.Text!="" && txtMedName.Text != "" && txtMedNumber.Text != "" && txtPricePerUnit.Text != "" && txtQuantity.Text != "")
            {
                String mid = txtMedID.Text;
                String mname = txtMedName.Text;
                String mnumber = txtMedNumber.Text;
                String mdate = txtManufacturingDate.Text;
                String edate = txtExpireDate.Text;
                Int64 perunit = Int64.Parse(txtPricePerUnit.Text);
                Int64 quantity = Int64.Parse(txtQuantity.Text);

                query = "insert into medicine( mid , mname, mnumber , mDate , eDate , quantity, perUnit) values ('"+mid+"', '"+mname+"', '"+mnumber+"', '"+mdate+"', '"+edate+"', "+quantity+", '"+perunit+"')";
                fn.setData(query, "Medicine added into the database.");
                ClearAll();

            }
            else
            {
                MessageBox.Show("Enter all data!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        public void ClearAll()
        {
            txtMedID.Clear();
            txtMedName.Clear();
            txtMedNumber.Clear();
            txtQuantity.Clear();
            txtPricePerUnit.Clear();
            txtManufacturingDate.ResetText();
            txtExpireDate.ResetText();

        }
    }
}
