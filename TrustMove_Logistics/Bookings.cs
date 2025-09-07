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
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
            LoadVehicleNumbers();
            LoadCustomer();
            LoadDriver();
            ShowBooking();
            ClearInputs();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\HIRUSHA\Desktop\esoft\A D\TrustMove_Logistics.mdf"";Integrated Security=True;Connect Timeout=30");

        //show table
        private void ShowBooking()

        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM BookingsTB", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                bookingDGV.DataSource = dt;
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

        private void ClearInputs()
        {
            txtbookingid.Clear();
            Txtdrange.Clear();
            Txtbcost.Clear();
            txtlorytype.Clear();

            
            if (Cobbcous.Items.Count > 0)
                Cobbcous.SelectedIndex = -1;

            if (CobVehicle.Items.Count > 0)
                CobVehicle.SelectedIndex = -1;

            if (CobDriver.Items.Count > 0)
                CobDriver.SelectedIndex = -1;

            
            Dtpdob.Value = DateTime.Today;
        }
        //cost calculation
        private void btncost_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txtdrange.Text) || string.IsNullOrWhiteSpace(txtlorytype.Text))
            {
                MessageBox.Show("Please enter distance and select a vehicle first.");
                return;
            }

            try
            {
                int distance = Convert.ToInt32(Txtdrange.Text);
                string lorryType = txtlorytype.Text.ToLower();
                int ratePerKm = 0;

                switch (lorryType)
                {
                    case "small":
                        ratePerKm = 6000;
                        break;
                    case "medium":
                        ratePerKm = 8000;
                        break;
                    case "large":
                        ratePerKm = 10000;
                        break;
                    default:
                        MessageBox.Show("Unknown Lorry Type!");
                        return;
                }

                int totalCost = distance * ratePerKm;
                Txtbcost.Text = totalCost.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        // SAVE TO DATABASE
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Cobbcous.Text) ||
                CobVehicle.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(CobDriver.Text) ||
                string.IsNullOrWhiteSpace(Txtdrange.Text) ||
                string.IsNullOrWhiteSpace(Txtbcost.Text) ||
                string.IsNullOrWhiteSpace(txtlorytype.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO BookingsTB
        (CustomerName, VehicleNumber, DriverName, BookingDate, DeliveryRange, TransportCost, LorryType) 
        VALUES (@CN, @VN, @DN, @BD, @DR, @TC, @LT)", Con);

                cmd.Parameters.AddWithValue("@CN", Cobbcous.Text);
                cmd.Parameters.AddWithValue("@VN", CobVehicle.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@DN", CobDriver.Text);
                cmd.Parameters.AddWithValue("@BD", Dtpdob.Value.Date);
                cmd.Parameters.AddWithValue("@DR", Txtdrange.Text);
                cmd.Parameters.AddWithValue("@TC", float.Parse(Txtbcost.Text));

                cmd.Parameters.AddWithValue("@LT", txtlorytype.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Booking Recorded Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving booking: " + ex.Message);
            }
            finally
            {
                Con.Close();
                ShowBooking();
            }
        }

        //edit
        private void Btnedit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbookingid.Text))
            {
                MessageBox.Show("Please select a booking to edit.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand(@"UPDATE BookingsTB 
        SET CustomerName = @CN,
            VehicleNumber = @VN,
            DriverName = @DN,
            BookingDate = @BD,
            DeliveryRange = @DR,
            TransportCost = @TC,
            LorryType = @LT
        WHERE BookingID = @ID", Con);

                cmd.Parameters.AddWithValue("@CN", Cobbcous.Text);
                cmd.Parameters.AddWithValue("@VN", CobVehicle.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@DN", CobDriver.Text);
                cmd.Parameters.AddWithValue("@BD", Dtpdob.Value.Date);
                cmd.Parameters.AddWithValue("@DR", Txtdrange.Text);

                // ✅ Convert Txtbcost to float before updating
                cmd.Parameters.AddWithValue("@TC", float.Parse(Txtbcost.Text));

                cmd.Parameters.AddWithValue("@LT", txtlorytype.Text);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtbookingid.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Booking Updated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
                ShowBooking();
                ClearInputs();
            }
        }

        //delete
        private void Btnremove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbookingid.Text))
            {
                MessageBox.Show("Please select a booking to delete.");
                return;
            }

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM BookingsTB WHERE BookingID=@ID", Con);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtbookingid.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Booking Deleted Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
                ShowBooking();   
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
            Dashboard dashboardForm = new Dashboard();
            dashboardForm.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles dashboardForm = new Vehicles();
            dashboardForm.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Customers dashboardForm = new Customers();
            dashboardForm.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers dashboardForm = new Drivers();
            dashboardForm.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //show a vehicel number plate
        private void LoadVehicleNumbers()
        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT NumberPlate FROM VehiclesTB", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                CobVehicle.DataSource = dt;
                CobVehicle.DisplayMember = "NumberPlate";
                CobVehicle.ValueMember = "NumberPlate";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading vehicles: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
        private void CobVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CobVehicle.SelectedIndex == -1) return;

            try
            {
                string NumberPlate = CobVehicle.SelectedValue.ToString();

                using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT LorryType FROM VehiclesTB WHERE NumberPlate = @NP", con);
                    cmd.Parameters.AddWithValue("@NP", NumberPlate);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        txtlorytype.Text = result.ToString(); // Small / Medium / Large
                    }
                    else
                    {
                        txtlorytype.Text = "Not Found";
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        //show a Customer names
        private void LoadCustomer()
        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT CustomerName FROM CustomersTB", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Cobbcous.DataSource = dt;
                Cobbcous.DisplayMember = "CustomerName";
                Cobbcous.ValueMember = "CustomerName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Customer Names: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
        private void Cobbcous_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cobbcous.SelectedValue == null) return;

            string selectedCustomer = Cobbcous.SelectedValue.ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM CustomersTB WHERE CustomerName = @CN", con);
                    cmd.Parameters.AddWithValue("@CN", selectedCustomer);

                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        //show a Drivers names
        private void LoadDriver()
        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT DriverName FROM DriversTB", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                CobDriver.DataSource = dt;
                CobDriver.DisplayMember = "DriverName";
                CobDriver.ValueMember = "DriverName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Driver Names: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
        private void CobDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CobDriver.SelectedValue == null) return;

            string selectedDriver = CobDriver.SelectedValue.ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM DriversTB WHERE DriverName = @DN", con);
                    cmd.Parameters.AddWithValue("@DN", selectedDriver);

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void txtlorytype_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Txtdname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        
    }
}
