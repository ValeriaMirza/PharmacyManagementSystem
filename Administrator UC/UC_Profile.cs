using System;
using System.Data;
using System.Windows.Forms;

namespace PharmacyManagementProject.Administrator_UC
{
    public partial class UC_Profile : UserControl
    {
        Singleton singleton = Singleton.Instance; // Accessing the Singleton instance
        string query;

        public UC_Profile()
        {
            InitializeComponent();
        }

        public string ID
        {
            set { userNameLabel.Text = value; }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String role = txtUserRole.Text;
            String name = txtName.Text;
            String dob = txtDob.Text;
            Int64 mobile = Int64.Parse(txtMobile.Text);
            String email = txtEmail.Text;
            String username = userNameLabel.Text;
            String pass = txtPassword.Text;

            query = "update users set userRole = '" + role + "',name =  '" + name + "',dob='" + dob + "', mobile=" + mobile + ",email = '" + email + "',pass = '" + pass + "' where username = '" + username + "'";
            singleton.SetData(query, "Update successful!"); // Using the Singleton instance
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            userNameLabel_Click(this, null);
        }

        private void userNameLabel_Click(object sender, EventArgs e)
        {
            query = "select * from users where username ='" + userNameLabel.Text + "'";
            DataSet ds = singleton.GetData(query); // Using the Singleton instance
            txtUserRole.Text = ds.Tables[0].Rows[0][1].ToString();
            txtName.Text = ds.Tables[0].Rows[0][2].ToString();
            txtDob.Text = ds.Tables[0].Rows[0][3].ToString();
            txtMobile.Text = ds.Tables[0].Rows[0][4].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0][5].ToString();
            txtPassword.Text = ds.Tables[0].Rows[0][6].ToString();
        }
    }
}
