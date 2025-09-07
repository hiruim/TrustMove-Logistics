using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TrustMove_Logistics
{
    public partial class Vehicles : Form
    {
        public Vehicles()
        {
            InitializeComponent();
            ShowVehicels();
            Clear();



        }
        

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\HIRUSHA\Desktop\esoft\A D\TrustMove_Logistics.mdf"";Integrated Security=True;Connect Timeout=30");
        
        //show table
        private void ShowVehicels()
        
        {
            try
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM VehiclesTB", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                vehicelDGV.DataSource = dt;
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

        // Clear all input fields
        private void Clear()
        {
            Txtvnum.Clear();
            TxtvDname.Clear();
            CobvLorrytype.SelectedIndex = -1;
            CobvLorrymodel.SelectedIndex = -1;

            // Optionally reset focus
            Txtvnum.Focus();
        }


        //save detabase
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (Txtvnum.Text == "" || TxtvDname.Text == "" || CobvLorrytype.SelectedIndex == -1 || CobvLorrymodel.SelectedIndex == -1 || TxtvDname.Text == "")
            {
                MessageBox.Show("Missing Infomation");
            } else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into VehiclesTB(NumberPlate,DriverName,LorryType,LorryModel) Values(@NP,@DN,@LT,@LM)", Con);
                    cmd.Parameters.AddWithValue("@NP", Txtvnum.Text);
                    cmd.Parameters.AddWithValue("@DN", TxtvDname.Text);
                    cmd.Parameters.AddWithValue("@LT", CobvLorrytype.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LM", CobvLorrymodel.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("vehicle Recorded");
                    Con.Close();
                    
                } catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
                ShowVehicels();
                Clear();

            }
            
        }

        //delete
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM VehiclesTB WHERE NumberPlate = @NP", Con);
                cmd.Parameters.AddWithValue("@NP", Txtvnum.Text);

                int rowsAffected = cmd.ExecuteNonQuery();


                if (rowsAffected > 0)
                {
                    MessageBox.Show("Vehicle Deleted Successfully");
                    

                }
                else
                {
                    MessageBox.Show("No vehicle found with that Number Plate");
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
            ShowVehicels();
        }

        //grid viwe
        private void vehicelDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Txtvnum.Text = vehicelDGV.SelectedRows[0].Cells[0].Value.ToString();
            TxtvDname.Text = vehicelDGV.SelectedRows[0].Cells[1].Value.ToString();
            CobvLorrytype.SelectedItem = vehicelDGV.SelectedRows[0].Cells[2].Value.ToString();
            CobvLorrymodel.SelectedItem = vehicelDGV.SelectedRows[0].Cells[3].Value.ToString();
        }


        // side controles

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers driversForm = new Drivers();
            driversForm.Show();   // Show the new form
            this.Hide();          // Hide current form
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Customers customersForm = new Customers();
            customersForm.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bookings bookingsForm = new Bookings();
            bookingsForm.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard dashboardForm = new Dashboard();
            dashboardForm.Show();
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
            this.Close();  // Exit app or close current form
        }





        //edit
        private void Btnedit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txtvnum.Text))
            {
                MessageBox.Show("Please enter a Number Plate to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtvDname.Text) ||
                CobvLorrytype.SelectedIndex == -1 ||
                CobvLorrymodel.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields before updating.");
                return;
            }

            try
            {
                if (Con.State == ConnectionState.Closed)
                    Con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE VehiclesTB " +
                    "SET DriverName = @DN, LorryType = @LT, LorryModel = @LM " +
                    "WHERE NumberPlate = @NP", Con);

                cmd.Parameters.AddWithValue("@NP", Txtvnum.Text.Trim());
                cmd.Parameters.AddWithValue("@DN", TxtvDname.Text.Trim());
                cmd.Parameters.AddWithValue("@LT", CobvLorrytype.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@LM", CobvLorrymodel.SelectedItem.ToString());

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("✅ Vehicle Updated Successfully");
                }
                else
                {
                    MessageBox.Show("⚠️ No vehicle found with that Number Plate");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error updating record: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                    Con.Close();
            }

            ShowVehicels();
            Clear();
        }











































        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
