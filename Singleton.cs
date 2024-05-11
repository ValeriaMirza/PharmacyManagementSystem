using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class Singleton
{
    private static Singleton instance;
    private static readonly object lockObject = new object();

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }

    protected SqlConnection GetConnection()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = "data source = DESKTOP-D3V5DUN\\SQLEXPRESS; database = pharmacy; integrated security = True ";
        return con;
    }

    public DataSet GetData(string query)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }

    public void SetData(string query, string msg)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
