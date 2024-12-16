using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sample_Project
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        // Connection string
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SampleDatabase";

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Products", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (ProductID, ItemName, Design, Color) VALUES (@ProductID, @ItemName, @Design, @Color)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", txtProductId.Text);
                cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text);
                cmd.Parameters.AddWithValue("@Design", txtDesign.Text);
                cmd.Parameters.AddWithValue("@Color", cbColor.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record saved successfully.");
                LoadData();
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Products SET ItemName=@ItemName, Design=@Design, Color=@Color WHERE ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", txtProductId.Text);
                cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text);
                cmd.Parameters.AddWithValue("@Design", txtDesign.Text);
                cmd.Parameters.AddWithValue("@Color", cbColor.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record updated successfully.");
                LoadData();
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", txtProductId.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record deleted successfully.");
                LoadData();
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products WHERE ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", txtProductId.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtItemName.Text = dt.Rows[0]["ItemName"].ToString();
                    txtDesign.Text = dt.Rows[0]["Design"].ToString();
                    cbColor.Text = dt.Rows[0]["Color"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found.");
                }
            }
        }
    }
}
