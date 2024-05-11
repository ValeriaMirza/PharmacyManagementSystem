using System;
using System.Data;
using System.Windows.Forms;

namespace PharmacyManagementProject.Pharmacist_UC
{
    public partial class UC_P_Dashboard : UserControl
    {
        Singleton singleton = Singleton.Instance; // Accessing the Singleton instance
        string query;
        DataSet ds;
        Int64 count;

        public UC_P_Dashboard()
        {
            InitializeComponent();
        }

        private void UC_P_Dashboard_Load(object sender, EventArgs e)
        {
            LoadChart();
        }

        public void LoadChart()
        {
            query = "select count(mname) from medicine where eDate >= getdate()";
            ds = singleton.GetData(query); // Using the Singleton instance
            count = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            this.chart1.Series["Valid Medicines"].Points.AddXY("Medicine Validity Chart", count);

            query = "select count(mname) from medicine where eDate <= getdate()";
            ds = singleton.GetData(query); // Using the Singleton instance
            count = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            this.chart1.Series["Expired Medicines"].Points.AddXY("Medicine Validity Chart", count);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            chart1.Series["Valid Medicines"].Points.Clear();
            chart1.Series["Expired Medicines"].Points.Clear();
            LoadChart();
        }
    }
}
