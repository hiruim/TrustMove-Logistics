using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrustMove_Logistics
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\HIRUSHA\Desktop\esoft\A D\TrustMove_Logistics.mdf"";Integrated Security=True;Connect Timeout=30");
        

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtLuname.Text) || string.IsNullOrWhiteSpace(TxtLpassword.Text))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            try
            {
                Con.Open();
                string query = "SELECT COUNT(*) FROM UsersTB WHERE UserName = @uname AND Password = @pass";
                SqlCommand cmd = new SqlCommand(query, Con);

                cmd.Parameters.AddWithValue("@uname", TxtLuname.Text.Trim());
                cmd.Parameters.AddWithValue("@pass", TxtLpassword.Text.Trim());

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Login successful!");
                    Dashboard dash = new Dashboard();
                    dash.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }






        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
