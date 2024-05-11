using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementProject.Administrator_UC
{
    public partial class UC_AddUser : UserControl
    {
        Singleton singleton = Singleton.Instance; // Accessing the Singleton instance
        string query;

        public UC_AddUser()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            String role = txtUserRole.Text;
            String name = txtName.Text;
            String dob = txtDob.Text;
            Int64 mobile = Int64.Parse(txtMobileNumber.Text);
            String email = txtEmail.Text;
            String username = txtUsername.Text;
            String pass = txtPassword.Text;

            try
            {
                query = "insert into users (userRole, name, dob, mobile, email, username, pass) values ('" + role + "', '" + name + "','" + dob + "', " + mobile + ",'" + email + "','" + username + "','" + pass + "')";
                singleton.SetData(query, "Sign Up Successful!"); // Using the Singleton instance
            }
            catch (Exception)
            {
                MessageBox.Show("Username Already Exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        public void ClearAll()
        {
            txtUserRole.SelectedIndex = -1;
            txtName.Clear();
            txtDob.ResetText();
            txtMobileNumber.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }
    }

}
