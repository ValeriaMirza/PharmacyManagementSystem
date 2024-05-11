using System;
using System.Data;
using System.Windows.Forms;

namespace PharmacyManagementProject.Administrator_UC
{
    public partial class UC_ViewUser : UserControl,IObserver
    {
        Singleton singleton = Singleton.Instance; // Accessing the Singleton instance
        string query;
        string currentUser = "";
        public static event EventHandler UserAdded;
        public static event EventHandler UserDeleted;

        public UC_ViewUser()
        {
            InitializeComponent();
            UC_AddUser.UserAdded += UC_AddUser_UserAdded;

        }
        private void UC_AddUser_UserAdded(object sender, EventArgs e)
        {
            // Handle user added event, update dashboard
            UC_ViewUser_Load(this, EventArgs.Empty);
        }

        public string ID
        {
            set { currentUser = value; }
        }

        private void UC_ViewUser_Load(object sender, EventArgs e)
        {
            query = "select * from users";
            DataSet ds = singleton.GetData(query); // Using the Singleton instance
            guna2DataGridView1.DataSource = ds.Tables[0];
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            query = "select * from users where username like '" + txtUserName.Text + "%'";
            DataSet ds = singleton.GetData(query); // Using the Singleton instance
            guna2DataGridView1.DataSource = ds.Tables[0];
        }

        String userName;

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                userName = guna2DataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (currentUser != userName)
                {
                    query = "delete from users where username = '" + userName + "'";
                    singleton.SetData(query, "User Record Deleted"); // Using the Singleton instance
                    //OnUserAdded(EventArgs.Empty);
                    OnUserDeleted(EventArgs.Empty);
                    UC_ViewUser_Load(this, null);
                }
                else
                {
                    MessageBox.Show("You are trying to delete your own profile!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void Update()
        {
            UC_ViewUser_Load(this, EventArgs.Empty);
        }
        protected virtual void OnUserAdded(EventArgs e)
        {
            UserAdded?.Invoke(this, e);
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_ViewUser_Load(this, null);
        }
        protected virtual void OnUserDeleted(EventArgs e)
        {
            UserDeleted?.Invoke(this, e);
        }
    }
}
