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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountVehicel();
            CountCustomers();
            CountBookings();
            CountDrivers();
            CountUsers();
            TotalIncome();

        }



        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\HIRUSHA\Desktop\esoft\A D\TrustMove_Logistics.mdf"";Integrated Security=True;Connect Timeout=30");


        private void CountVehicel()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM VehiclesTB", Con);
                int count = (int)cmd.ExecuteScalar();
                LblVnum.Text = count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting vehicles: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
        private void CountCustomers()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CustomersTB", Con);
                int count = (int)cmd.ExecuteScalar();
                LblCnum.Text = count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting customers: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void CountDrivers()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM DriversTB", Con);
                int count = (int)cmd.ExecuteScalar();
                LblDnum.Text = count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting drivers: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void CountBookings()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM BookingsTB", Con);
                int count = (int)cmd.ExecuteScalar();
                LblBnum.Text = count.ToString(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting bookings: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void CountUsers()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM UsersTB", Con);
                int count = (int)cmd.ExecuteScalar();
                LblUnum.Text = count.ToString(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting users: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void TotalIncome()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(TransportCost) FROM BookingsTB", Con);
                object result = cmd.ExecuteScalar();
                double totalIncome = 0;

                if (result != DBNull.Value)
                {
                    totalIncome = Convert.ToDouble(result);
                }

                
                LblInum.Text = totalIncome.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating total income: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }





        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Users customersForm = new Users();
            customersForm.Show();
            this.Hide();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers customersForm = new Drivers();
            customersForm.Show();
            this.Hide();
        }

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

        private void panel19_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Users customersForm = new Users();
            customersForm.Show();
            this.Hide();
        }
    }
}
