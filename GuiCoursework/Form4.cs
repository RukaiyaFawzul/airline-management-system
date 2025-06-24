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
using System.Text.RegularExpressions;

namespace GuiCoursework
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
           
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            try
            {
               
                con.Open();

               
                string sql = "SELECT * FROM Flights"; 
                SqlCommand com = new SqlCommand(sql, con);

               
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);

               
                dataGridView1.DataSource = dt; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               
                con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {

        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
          
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            
                
                string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);

                try
                {
                    
                    con.Open();

                    
                    string sql = "SELECT * FROM Flights WHERE FlightID=@FlightID";
                    SqlCommand com = new SqlCommand(sql, con);
                    com.Parameters.AddWithValue("@FlightID", this.txtsearch.Text.Trim()); 

                    
                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.Read() == true)
                    {
                        // Populate the relevant fields
                        this.txtFlightId.Text = dr.GetValue(0).ToString(); 
                        this.txtflightNo.Text = dr.GetValue(1).ToString();   
                        this.txtaircraft.Text = dr.GetValue(2).ToString();    
                        this.txtManufacturer.Text = dr.GetValue(3).ToString(); 
                        this.numericUpDown1.Text = dr.GetValue(4).ToString();   
                        this.numericUpDown2.Text = dr.GetValue(5).ToString();
                         txtsearch.Text = "";
                }
                    else
                    {
                        
                        MessageBox.Show("No records found for the given Flight ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtsearch.Text = "";
                }
                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                   
                    con.Close();
                }
            

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            con.Open();

            // no field is empty validation
            if (string.IsNullOrWhiteSpace(txtFlightId.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtflightNo.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtaircraft.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtManufacturer.Text.Trim()) ||
                numericUpDown1.Value <= 0 ||
                numericUpDown2.Value <= 0)
            {
                MessageBox.Show("All fields must be filled in, and numeric values must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate FlightID format
            if (!Regex.IsMatch(txtFlightId.Text.Trim(), @"^F\d{3}$"))
            {
                MessageBox.Show("Flight ID must start with 'F' along with digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate FlightNo format
            if (!Regex.IsMatch(txtflightNo.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Flight Number must contain only numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate AircraftModel
            if (string.IsNullOrWhiteSpace(txtaircraft.Text.Trim()))
            {
                MessageBox.Show("Aircraft Model cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Manufacturer
            if (string.IsNullOrWhiteSpace(txtManufacturer.Text.Trim()))
            {
                MessageBox.Show("Manufacturer cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate CrewCapacity
            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("Crew Capacity must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate SeatingCapacity
            if (numericUpDown2.Value <= 0)
            {
                MessageBox.Show("Seating Capacity must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            string sql = "UPDATE Flights SET FlightNo=@FlightNo, AircraftModel=@AircraftModel, Manufacturer=@Manufacturer, " +
                         "CrewCapacity=@CrewCapacity, SeatingCapacity=@SeatingCapacity WHERE FlightID=@FlightID";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@FlightID", txtFlightId.Text.Trim());
            com.Parameters.AddWithValue("@FlightNo", txtflightNo.Text.Trim());
            com.Parameters.AddWithValue("@AircraftModel", txtaircraft.Text.Trim());
            com.Parameters.AddWithValue("@Manufacturer", txtManufacturer.Text.Trim());
            com.Parameters.AddWithValue("@CrewCapacity", Convert.ToInt32(numericUpDown1.Text.Trim()));
            com.Parameters.AddWithValue("@SeatingCapacity", Convert.ToInt32(numericUpDown2.Text.Trim()));

            int rowsUpdated = com.ExecuteNonQuery();

           
            if (rowsUpdated > 0)
            {
                MessageBox.Show($"Number of records updated: {rowsUpdated}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Clear the text fields after adding
                txtFlightId.Text = "";
                txtflightNo.Text = "";
                txtaircraft.Text = "";
                txtManufacturer.Text = "";
                numericUpDown1.Text = "";
                numericUpDown2.Text = "";
            }
            else
            {
                MessageBox.Show("No records were updated. Please check the Flight ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
            con.Close();

           
            LoadData();



        }

        private void btnadd_Click_1(object sender, EventArgs e)
        {

            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            con.Open();

            //  no field is empty
            if (string.IsNullOrWhiteSpace(txtFlightId.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtflightNo.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtaircraft.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtManufacturer.Text.Trim()) ||
                numericUpDown1.Value <= 0 ||
                numericUpDown2.Value <= 0)
            {
                MessageBox.Show("All fields must be filled in, and numeric values must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate FlightID
            if (!Regex.IsMatch(txtFlightId.Text.Trim(), @"^F\d{3}$"))
            {
                MessageBox.Show("Flight ID must start with 'F' along with digits (e.g., F001).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check uniqueness of FlightID
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Flights WHERE FlightID = @FlightID", con);
            checkCmd.Parameters.AddWithValue("@FlightID", txtFlightId.Text.Trim());
            int flightIdExists = (int)checkCmd.ExecuteScalar();
            if (flightIdExists > 0)
            {
                MessageBox.Show("Flight ID already exists. Please enter a unique ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate FlightNo
            if (!Regex.IsMatch(txtflightNo.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Flight Number must contain only numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate AircraftModel
            if (string.IsNullOrWhiteSpace(txtaircraft.Text.Trim()))
            {
                MessageBox.Show("Aircraft Model cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Manufacturer
            if (string.IsNullOrWhiteSpace(txtManufacturer.Text.Trim()))
            {
                MessageBox.Show("Manufacturer cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate CrewCapacity
            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("Crew Capacity must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate SeatingCapacity
            if (numericUpDown2.Value <= 0)
            {
                MessageBox.Show("Seating Capacity must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            string sql = "INSERT INTO Flights (FlightID, FlightNo, AircraftModel, Manufacturer, CrewCapacity, SeatingCapacity) " +
                         "VALUES (@FlightID, @FlightNo, @AircraftModel, @Manufacturer, @CrewCapacity, @SeatingCapacity)";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@FlightID", txtFlightId.Text.Trim());
            com.Parameters.AddWithValue("@FlightNo", txtflightNo.Text.Trim());
            com.Parameters.AddWithValue("@AircraftModel", txtaircraft.Text.Trim());
            com.Parameters.AddWithValue("@Manufacturer", txtManufacturer.Text.Trim());
            com.Parameters.AddWithValue("@CrewCapacity", Convert.ToInt32(numericUpDown1.Text.Trim()));
            com.Parameters.AddWithValue("@SeatingCapacity", Convert.ToInt32(numericUpDown2.Text.Trim()));

            int rowsInserted = com.ExecuteNonQuery();

            LoadData();

            if (rowsInserted > 0)
            {
                MessageBox.Show("Flight details added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the text fields after adding
                txtFlightId.Text = "";
                txtflightNo.Text = "";
                txtaircraft.Text = "";
                txtManufacturer.Text = "";
                numericUpDown1.Text = "";
                numericUpDown2.Text = "";
            }
            else
            {
                MessageBox.Show("Failed to add flight details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();



        }

        private void btndlt_Click(object sender, EventArgs e)
        {

            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            con.Open();

           
            if (string.IsNullOrWhiteSpace(txtFlightId.Text.Trim()))
            {
                MessageBox.Show("Please select Flight ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            string sql = "DELETE FROM Flights WHERE FlightID=@FlightID";
            SqlCommand com = new SqlCommand(sql, con);

            // Add parameters for FlightID
            com.Parameters.AddWithValue("@FlightID", txtFlightId.Text.Trim());

         
            DialogResult msgret = MessageBox.Show("Are you sure you want to delete this flight record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (msgret == DialogResult.Yes)
            {
               
                int ret = com.ExecuteNonQuery();
                LoadData(); 

                if (ret > 0)
                {
                    MessageBox.Show("Flight record deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No record found with the given Flight ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

          
            txtFlightId.Text = "";
            txtflightNo.Text = "";
            txtaircraft.Text = "";
            txtManufacturer.Text = "";
            numericUpDown1.Text = "";
            numericUpDown2.Text = "";

           
            con.Close();



        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnback_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }
    }
}
