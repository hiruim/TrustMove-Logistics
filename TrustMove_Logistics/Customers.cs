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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCustomers();
            ClearInputs();
        }
        private void ClearInputs()
        {
            Txtdcname.Clear();
            Txtdcphone.Clear();
            Txtdcaddress.Clear();
            Cobgender.SelectedIndex = -1; 
        }

        //show table
        private void ShowCustomers()

        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM CustomersTB", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                customerDGV.DataSource = dt;
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

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\HIRUSHA\Desktop\esoft\A D\TrustMove_Logistics.mdf"";Integrated Security=True;Connect Timeout=30");

        // save detabase
        private void Btnsave_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(Txtdcname.Text) ||
                string.IsNullOrWhiteSpace(Txtdcphone.Text) ||
                string.IsNullOrWhiteSpace(Txtdcaddress.Text) ||
                Cobgender.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO CustomersTB(CustomerName, Gender, PhoneNumber, CustomerAddress) VALUES(@CN, @G, @PN, @CA)", Con);

                cmd.Parameters.AddWithValue("@CN", Txtdcname.Text);
                cmd.Parameters.AddWithValue("@G", Cobgender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@PN", Txtdcphone.Text);
                cmd.Parameters.AddWithValue("@CA", Txtdcaddress.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Recorded Successfully");

                Con.Close();

                ShowCustomers();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //grid viwe
        private void customerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = customerDGV.Rows[e.RowIndex];
                txtcustomerid.Text = row.Cells[1].Value.ToString();
                Txtdcname.Text = row.Cells[2].Value.ToString();      
                Cobgender.SelectedItem = row.Cells[3].Value.ToString(); 
                Txtdcphone.Text = row.Cells[4].Value.ToString();     
                Txtdcaddress.Text = row.Cells[5].Value.ToString();   
            }
        }

        //edit
        private void Btnedit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtcustomerid.Text))
            {
                MessageBox.Show("Please select a customer to edit.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand(@"UPDATE CustomersTB 
            SET CustomerName=@CN, Gender=@G, PhoneNumber=@PN, CustomerAddress=@CA
            WHERE CustomerID=@ID", Con);

                cmd.Parameters.AddWithValue("@CN", Txtdcname.Text);
                cmd.Parameters.AddWithValue("@G", Cobgender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@PN", Txtdcphone.Text);
                cmd.Parameters.AddWithValue("@CA", Txtdcaddress.Text);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtcustomerid.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Updated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
                ShowCustomers(); 
                ClearInputs();   
            }
        }


        
        // Delete customer
        private void Btnfire_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtcustomerid.Text))
            {
                MessageBox.Show("Please select a customer to delete.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM CustomersTB WHERE CustomerID=@ID", Con);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtcustomerid.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Deleted Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
                ShowCustomers();  
                ClearInputs();    
            }
        }






        //side controle
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Users usersForm = new Users();
            usersForm.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard usersForm = new Dashboard();
            usersForm.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bookings usersForm = new Bookings();
            usersForm.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles usersForm = new Vehicles();
            usersForm.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers usersForm = new Drivers();
            usersForm.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }













































































        private void Cobdrivername_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        
    }
}
