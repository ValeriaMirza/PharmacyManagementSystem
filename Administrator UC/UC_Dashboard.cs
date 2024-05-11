using System;
using System.Data;
using System.Windows.Forms;

namespace PharmacyManagementProject.Administrator_UC
{
    public partial class UC_Dashboard : UserControl, IObserver
    {
        Singleton singleton = Singleton.Instance; // Accessing the Singleton instance
        string query;
        DataSet ds;

        public UC_Dashboard()
        {
            InitializeComponent();
            UC_AddUser.UserAdded += UC_AddUser_UserAdded;
            UC_ViewUser.UserDeleted += UC_ViewUser_UserDeleted;
        }
        private void UC_ViewUser_UserDeleted(object sender, EventArgs e)
        {
            // Handle user deleted event, update dashboard
            UC_Dashboard_Load(this, EventArgs.Empty);
        }
        private void UC_AddUser_UserAdded(object sender, EventArgs e)
        {
            // Handle user added event, update dashboard
            UC_Dashboard_Load(this, EventArgs.Empty);
        }
        public void Update()
        {
            UC_Dashboard_Load(this, EventArgs.Empty);
        }
        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
            query = "select count(userRole) from users where userRole = 'Administrator'";
            ds = singleton.GetData(query); // Using the Singleton instance
            SetLabel(ds, AdminLabel);

            query = "select count(userRole) from users where userRole = 'Pharmacist'";
            ds = singleton.GetData(query); // Using the Singleton instance
            SetLabel(ds, PharmaLabel);
        }

        private void SetLabel(DataSet ds, Label lbl)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                lbl.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                lbl.Text = "0";
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_Dashboard_Load(this, null);
        }
    }
}
