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
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();
            ShowDrivers();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\HIRUSHA\Desktop\esoft\A D\TrustMove_Logistics.mdf"";Integrated Security=True;Connect Timeout=30");
        
        //clear
        private void ClearInputs()
        {
            Txtdname.Clear();
            Txtdphone.Clear();
            Txtdaddress.Clear();
            Dtpdob.Value = DateTime.Today;
            Dtpjoind.Value = DateTime.Today;
            Cobgender.SelectedIndex = -1;
        }

        //show table
        private void ShowDrivers()

        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM DriversTB", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                driverDGV.DataSource = dt;
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
            if (string.IsNullOrWhiteSpace(Txtdname.Text) ||
                string.IsNullOrWhiteSpace(Txtdphone.Text) ||
                string.IsNullOrWhiteSpace(Txtdaddress.Text) ||
                Cobgender.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO DriversTB (DriverName,PhoneNumber,Address,DateOfBirth,JoinDate,Gender) " +
                                                "VALUES (@DN, @DP, @DA, @DOB, @JD, @G)", Con);

                cmd.Parameters.AddWithValue("@DN", Txtdname.Text);
                cmd.Parameters.AddWithValue("@DP", Txtdphone.Text);
                cmd.Parameters.AddWithValue("@DA", Txtdaddress.Text);
                cmd.Parameters.AddWithValue("@DOB", Dtpdob.Value.Date);
                cmd.Parameters.AddWithValue("@JD", Dtpjoind.Value.Date);
                cmd.Parameters.AddWithValue("@G", Cobgender.SelectedItem.ToString());

                cmd.ExecuteNonQuery();
                MessageBox.Show("Driver Recorded Successfully!");

                Con.Close();
                ShowDrivers(); 
                ClearInputs();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //grid viwe
        private void driverDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = driverDGV.Rows[e.RowIndex];

                txtdriverid.Text = row.Cells["DriverID"].Value.ToString();
                Txtdname.Text = row.Cells["Drivers Name"].Value.ToString();
                Txtdphone.Text = row.Cells["Drivers Phone"].Value.ToString();
                Txtdaddress.Text = row.Cells["Drivers Address"].Value.ToString();

                if (row.Cells["Drivers D O B"].Value != DBNull.Value)
                    Dtpdob.Value = Convert.ToDateTime(row.Cells["Drivers D O B"].Value);

                if (row.Cells["Join Date"].Value != DBNull.Value)
                    Dtpjoind.Value = Convert.ToDateTime(row.Cells["Join Date"].Value);

                Cobgender.SelectedItem = row.Cells["Gender"].Value.ToString();
            }
        }

        //edit
        private void Btnedit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtdriverid.Text))
            {
                MessageBox.Show("Please select a driver to edit.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand(@"UPDATE DriversTB 
            SET DriverName=@DN, PhoneNumber=@DP, Address=@DA, 
                DateOfBirth=@DOB, JoinDate=@JD, Gender=@G
            WHERE DriverID=@ID", Con);

                cmd.Parameters.AddWithValue("@DN", Txtdname.Text);
                cmd.Parameters.AddWithValue("@DP", Txtdphone.Text);
                cmd.Parameters.AddWithValue("@DA", Txtdaddress.Text);
                cmd.Parameters.AddWithValue("@DOB", Dtpdob.Value.Date);
                cmd.Parameters.AddWithValue("@JD", Dtpjoind.Value.Date);
                cmd.Parameters.AddWithValue("@G", Cobgender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtdriverid.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Driver Updated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
                ShowDrivers();
                ClearInputs();
            }
        }

        // delete
        private void Btnfire_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtdriverid.Text))
            {
                MessageBox.Show("Please select a driver to delete.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM DriversTB WHERE DriverID=@ID", Con);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtdriverid.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Driver Deleted Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
                ShowDrivers();
                ClearInputs();
            }
        }



        // side controles
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Customers customersForm = new Customers();
            customersForm.Show();
            this.Hide();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles customersForm = new Vehicles();
            customersForm.Show();
            this.Hide();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bookings customersForm = new Bookings();
            customersForm.Show();
            this.Hide();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard customersForm = new Dashboard();
            customersForm.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Users usersForm = new Users();
            usersForm.Show();
            this.Hide();
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

































        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void Txtdname_TextChanged(object sender, EventArgs e)
        {

        }

        






        private void Cobgender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Dtpdob_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Txtdaddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Cobdrivername_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Txtdphone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }



        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }



        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void txtdriverid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
