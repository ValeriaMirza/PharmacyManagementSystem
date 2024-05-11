using DGVPrinterHelper;
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
    public partial class UC_P_SellMedicine : UserControl
    {
        Singleton singleton = Singleton.Instance;
        String query;
        DataSet ds;
        public UC_P_SellMedicine()
        {
            InitializeComponent();
        }

        private void UC_P_SellMedicine_Load(object sender, EventArgs e)
        {
            listBoxMedicines.Items.Clear();
            query = "select mname from medicine where eDate >= getdate() and quantity>'0'";
            ds = singleton.GetData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count;i++)
            {
                listBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_P_SellMedicine_Load(this,null);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listBoxMedicines.Items.Clear();
            query = "select mname from medicine where mname like '"+txtSearch.Text+"%' and eDate >= getdate() and quantity > '0'";
            ds = singleton.GetData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void listBoxMedicines_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNoOfUnits.Clear();
            String name = listBoxMedicines.GetItemText(listBoxMedicines.SelectedItem);
            txtMedName.Text = name;
            query = "select mid, eDate,perUnit from medicine where mname = '"+name+"'";
            ds = singleton.GetData(query);
            txtMedID.Text = ds.Tables[0].Rows[0][0].ToString();
            txtExpireDate.Text = ds.Tables[0].Rows[0][1].ToString();
            txtPricePerUnit.Text = ds.Tables[0].Rows[0][2].ToString();
        }

        private void txtNoOfUnits_TextChanged(object sender, EventArgs e)
        {
            if(txtNoOfUnits.Text != "")
            {
                Int64 unitPrice = Int64.Parse(txtPricePerUnit.Text);
                Int64 noOfUnit = Int64.Parse(txtNoOfUnits.Text);
                Int64 totalAmount = unitPrice * noOfUnit;
                txtTotalPrice.Text = totalAmount.ToString();
            }
            else
            {
                txtTotalPrice.Clear();
            }
        }
        protected int n, totalAmount = 0;
        protected Int64 quantity, newQuantity;

        

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (txtMedID.Text != "")
            {
                query = "select quantity from medicine where mid = '" + txtMedID.Text + "'";
                ds = singleton.GetData(query);

                quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                newQuantity = quantity - Int64.Parse(txtNoOfUnits.Text);

                if (newQuantity >= 0)
                {
                    n = guna2DataGridView1.Rows.Add();
                    guna2DataGridView1.Rows[n].Cells[0].Value = txtMedID.Text;
                    guna2DataGridView1.Rows[n].Cells[1].Value = txtMedName.Text;
                    guna2DataGridView1.Rows[n].Cells[2].Value = txtExpireDate.Text;
                    guna2DataGridView1.Rows[n].Cells[3].Value = txtPricePerUnit.Text;
                    guna2DataGridView1.Rows[n].Cells[4].Value = txtNoOfUnits.Text;
                    guna2DataGridView1.Rows[n].Cells[5].Value = txtTotalPrice.Text;

                    totalAmount = totalAmount + int.Parse(txtTotalPrice.Text);
                    totalLabel.Text = "MDL: " + totalAmount.ToString();

                    query = "update medicine set quantity = '"+newQuantity+"' where mid = '"+txtMedID.Text+"'";
                    singleton.SetData(query, "Medicine Added");
                }
                else
                {
                    MessageBox.Show("Medicine is out of stock.\n Only '" + quantity + "' units left ", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                ClearAll();
                UC_P_SellMedicine_Load(this, null);
            }
            else
            {
                MessageBox.Show("Select Medicine First", "Information",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }         

        }
        int valueAmount;
        String valueId;
        protected Int64 noOfUnit;

       

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                valueAmount = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                valueId = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                noOfUnit = Int64.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
            catch(Exception)
            {

            }

        }

        private void btnPurchasePrint_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "Medicine Bill";
            print.SubTitle = String.Format("Date:- {0}", DateTime.Now.Date);
            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = true;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;
            print.Footer = "Total Payable Amount: " + totalLabel.Text;
            print.FooterSpacing = 15;
            print.PrintDataGridView(guna2DataGridView1);

            totalAmount = 0;
            totalLabel.Text = "MDL: 00";
            guna2DataGridView1.DataSource = 0;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(valueId!=null)
            {
                try
                {
                    guna2DataGridView1.Rows.RemoveAt(this.guna2DataGridView1.SelectedRows[0].Index);


                }
                catch
                {

                }
                finally
                {
                    query  ="select quantity from medicine where mid ='"+valueId+"'";
                    ds = singleton.GetData(query);
                    quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    newQuantity = quantity + noOfUnit;
                    query = "update medicine set quantity = '"+newQuantity+"' where mid = '"+valueId+"'";
                    singleton.SetData(query, "Medicine removed from cart!");
                    totalAmount = totalAmount - valueAmount;
                    totalLabel.Text = "MDL:" + totalAmount.ToString();

                }
                UC_P_SellMedicine_Load(this, null);
            }

        }
        private void ClearAll()
        {
            txtMedID.Clear();
            txtMedName.Clear();
            txtPricePerUnit.Clear();
            txtNoOfUnits.Clear();
            txtExpireDate.ResetText();

        }


    }
}
