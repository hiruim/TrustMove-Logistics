using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrustMove_Logistics
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            ShowUsers();
        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\HIRUSHA\Desktop\esoft\A D\TrustMove_Logistics.mdf"";Integrated Security=True;Connect Timeout=30");


        private void ClearUsers()
        {
            Txtduname.Clear();
            Txtduphone.Clear();
            Txtupass.Clear();
        }

        //show table
        private void ShowUsers()

        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM UsersTB", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                UserDGV.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        //save detabase
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txtduname.Text) ||
                string.IsNullOrWhiteSpace(Txtduphone.Text) ||
                string.IsNullOrWhiteSpace(Txtupass.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO UsersTB(UserName, PhoneNumber, Password) 
                                          VALUES(@UN, @PN, @PW)", Con);

                cmd.Parameters.AddWithValue("@UN", Txtduname.Text.Trim());
                cmd.Parameters.AddWithValue("@PN", Txtduphone.Text.Trim());
                cmd.Parameters.AddWithValue("@PW", Txtupass.Text.Trim());

                cmd.ExecuteNonQuery();
                MessageBox.Show("User Recorded Successfully");

                Con.Close();

                ShowUsers(); 
                ClearUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        //grid viwe
        
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = UserDGV.Rows[e.RowIndex];

                txtuserid.Text = row.Cells[0].Value.ToString();   
                Txtduname.Text = row.Cells[1].Value.ToString();   
                Txtduphone.Text = row.Cells[2].Value.ToString();  
                Txtupass.Text = row.Cells[3].Value.ToString(); 
            }
        }

        //edit
        private void Btnedit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtuserid.Text))
            {
                MessageBox.Show("Please select a user to edit.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand(@"UPDATE UsersTB 
            SET UserName=@UN, PhoneNumber=@PN, Password=@PW
            WHERE UserID=@ID", Con);

                cmd.Parameters.AddWithValue("@UN", Txtduname.Text.Trim());
                cmd.Parameters.AddWithValue("@PN", Txtduphone.Text.Trim());
                cmd.Parameters.AddWithValue("@PW", Txtupass.Text.Trim());
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtuserid.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("User Updated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
                ShowUsers();   
                ClearUsers();  
            }
        }

        //delete
        private void Btnfire_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtuserid.Text))
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM UsersTB WHERE UserID=@ID", Con);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtuserid.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("User Deleted Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
                ShowUsers();   
                ClearUsers();  
            }
        }




        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard dashboardForm = new Dashboard();
            dashboardForm.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Vehicles driversForm = new Vehicles();
            driversForm.Show();   
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
