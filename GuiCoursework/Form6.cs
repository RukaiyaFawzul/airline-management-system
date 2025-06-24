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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void LoadData()
        {
           
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            try
            {
               
                con.Open();

                
                string sql = "SELECT * FROM Tickets";
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


        private void Form6_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {


            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            con.Open();

           

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(txtTicketId.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtpassengerid.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtflightid.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtdeplocation.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtarrivallocation.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtseat.Text.Trim()) ||
                comboBox1.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtTicketprice.Text.Trim()))
            {
                MessageBox.Show("All fields must be filled in.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate TicketID format
            if (!Regex.IsMatch(txtTicketId.Text.Trim(), @"^T\d+$"))
            {
                MessageBox.Show("Ticket ID must start with 'T' followed by numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if PassengerID exists in the database
            SqlCommand checkPassengerCmd = new SqlCommand("SELECT COUNT(*) FROM Passengers WHERE PassengerID = @PassengerID", con);
            checkPassengerCmd.Parameters.AddWithValue("@PassengerID", txtpassengerid.Text.Trim());
            int passengerExists = (int)checkPassengerCmd.ExecuteScalar();
            if (passengerExists == 0)
            {
                MessageBox.Show("Passenger ID does not exist. Please enter a valid Passenger ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if FlightID exists in the database
            SqlCommand checkFlightCmd = new SqlCommand("SELECT COUNT(*) FROM Flights WHERE FlightID = @FlightID", con);
            checkFlightCmd.Parameters.AddWithValue("@FlightID", txtflightid.Text.Trim());
            int flightExists = (int)checkFlightCmd.ExecuteScalar();
            if (flightExists == 0)
            {
                MessageBox.Show("Flight ID does not exist. Please enter a valid Flight ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Ticket Price (must be numeric and greater than 0)
            if (!decimal.TryParse(txtTicketprice.Text.Trim(), out decimal ticketPrice) || ticketPrice <= 0)
            {
                MessageBox.Show("Ticket Price must be a numeric value greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Departure and Arrival Dates
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                MessageBox.Show("Arrival Date/Time must be after the Departure Date/Time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Payment Status
            string paymentStatus = "";
            if (radioButton1.Checked)
            {
                paymentStatus = "Paid";
            }
            else if (radioButton2.Checked)
            {
                paymentStatus = "Pending";
            }
            else
            {
                MessageBox.Show("Please select a payment status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            string sql = "INSERT INTO Tickets (TicketID, PassengerID, FlightID, DepartureLocation, ArrivalDestination, DepartureDate, ArrivalDate, SeatNumber, SeatClass, TicketPrice, PaymentStatus) " +
                         "VALUES (@TicketID, @PassengerID, @FlightID, @DepartureLocation, @ArrivalDestination, @DepartureDate, @ArrivalDate, @SeatNumber, @SeatClass, @TicketPrice, @PaymentStatus)";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@TicketID", txtTicketId.Text.Trim());
            com.Parameters.AddWithValue("@PassengerID", txtpassengerid.Text.Trim());
            com.Parameters.AddWithValue("@FlightID", txtflightid.Text.Trim());
            com.Parameters.AddWithValue("@DepartureLocation", txtdeplocation.Text.Trim());
            com.Parameters.AddWithValue("@ArrivalDestination", txtarrivallocation.Text.Trim());
            com.Parameters.AddWithValue("@DepartureDate", dateTimePicker1.Value);
            com.Parameters.AddWithValue("@ArrivalDate", dateTimePicker2.Value);
            com.Parameters.AddWithValue("@SeatNumber", txtseat.Text.Trim());
            com.Parameters.AddWithValue("@SeatClass", comboBox1.SelectedItem.ToString().Trim());
            com.Parameters.AddWithValue("@TicketPrice", ticketPrice);
            com.Parameters.AddWithValue("@PaymentStatus", paymentStatus);

            int rowsInserted = com.ExecuteNonQuery();
            LoadData();

            
            if (rowsInserted > 0)
            {
                MessageBox.Show("Ticket details added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields after successful insertion
                txtTicketId.Text = "";
                txtpassengerid.Text = "";
                txtflightid.Text = "";
                txtdeplocation.Text = "";
                txtarrivallocation.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                txtseat.Text = "";
                comboBox1.SelectedIndex = -1;
                txtTicketprice.Text = "";

                radioButton1.Checked = false;
                radioButton2.Checked = false;
            }
            else
            {
                MessageBox.Show("Failed to add ticket details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
            con.Close();



        }

        private void btnupdate_Click(object sender, EventArgs e)
        {


            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            con.Open();

          

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(txtTicketId.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtpassengerid.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtflightid.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtdeplocation.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtarrivallocation.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtseat.Text.Trim()) ||
                comboBox1.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtTicketprice.Text.Trim()))
            {
                MessageBox.Show("All fields must be filled in.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate TicketID format
            if (!Regex.IsMatch(txtTicketId.Text.Trim(), @"^T\d+$"))
            {
                MessageBox.Show("Ticket ID must start with 'T' followed by numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if PassengerID exists in the database
            SqlCommand checkPassengerCmd = new SqlCommand("SELECT COUNT(*) FROM Passengers WHERE PassengerID = @PassengerID", con);
            checkPassengerCmd.Parameters.AddWithValue("@PassengerID", txtpassengerid.Text.Trim());
            int passengerExists = (int)checkPassengerCmd.ExecuteScalar();
            if (passengerExists == 0)
            {
                MessageBox.Show("Passenger ID does not exist. Please enter a valid Passenger ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if FlightID exists in the database
            SqlCommand checkFlightCmd = new SqlCommand("SELECT COUNT(*) FROM Flights WHERE FlightID = @FlightID", con);
            checkFlightCmd.Parameters.AddWithValue("@FlightID", txtflightid.Text.Trim());
            int flightExists = (int)checkFlightCmd.ExecuteScalar();
            if (flightExists == 0)
            {
                MessageBox.Show("Flight ID does not exist. Please enter a valid Flight ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Ticket Price (must be numeric and greater than 0)
            if (!decimal.TryParse(txtTicketprice.Text.Trim(), out decimal ticketPrice) || ticketPrice <= 0)
            {
                MessageBox.Show("Ticket Price must be a numeric value greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Departure and Arrival Dates
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                MessageBox.Show("Arrival Date/Time must be after the Departure Date/Time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Payment Status
            string paymentStatus = "";
            if (radioButton1.Checked)
            {
                paymentStatus = "Paid";
            }
            else if (radioButton2.Checked)
            {
                paymentStatus = "Pending";
            }
            else
            {
                MessageBox.Show("Please select a payment status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            string sql = "UPDATE Tickets SET PassengerID=@PassengerID, FlightID=@FlightID, DepartureLocation=@DepartureLocation, " +
                         "ArrivalDestination=@ArrivalDestination, DepartureDate=@DepartureDate, ArrivalDate=@ArrivalDate, SeatNumber=@SeatNumber, " +
                         "SeatClass=@SeatClass, TicketPrice=@TicketPrice, PaymentStatus=@PaymentStatus WHERE TicketID=@TicketID";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@TicketID", txtTicketId.Text.Trim());
            com.Parameters.AddWithValue("@PassengerID", txtpassengerid.Text.Trim());
            com.Parameters.AddWithValue("@FlightID", txtflightid.Text.Trim());
            com.Parameters.AddWithValue("@DepartureLocation", txtdeplocation.Text.Trim());
            com.Parameters.AddWithValue("@ArrivalDestination", txtarrivallocation.Text.Trim());
            com.Parameters.AddWithValue("@DepartureDate", dateTimePicker1.Value);
            com.Parameters.AddWithValue("@ArrivalDate", dateTimePicker2.Value);
            com.Parameters.AddWithValue("@SeatNumber", txtseat.Text.Trim());
            com.Parameters.AddWithValue("@SeatClass", comboBox1.SelectedItem?.ToString()?.Trim());
            com.Parameters.AddWithValue("@TicketPrice", ticketPrice);
            com.Parameters.AddWithValue("@PaymentStatus", paymentStatus);

           
            int rowsUpdated = com.ExecuteNonQuery();

           
            if (rowsUpdated > 0)
            {
                MessageBox.Show($"Number of records updated: {rowsUpdated}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields 
                txtTicketId.Text = "";
                txtpassengerid.Text = "";
                txtflightid.Text = "";
                txtdeplocation.Text = "";
                txtarrivallocation.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                txtseat.Text = "";
                comboBox1.SelectedIndex = -1;
                txtTicketprice.Text = "";
                radioButton1.Checked = false;
                radioButton2.Checked = false;

                LoadData();
            }
            else
            {
                MessageBox.Show("No records were updated. Please check the Ticket ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

          
            con.Close();


        }

        private void btndlt_Click(object sender, EventArgs e)
        {
           
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            try
            {
                
                con.Open();

               
                string sql = "DELETE FROM Tickets WHERE TicketID=@TicketID";
                SqlCommand com = new SqlCommand(sql, con);

              
                com.Parameters.AddWithValue("@TicketID", txtTicketId.Text.Trim());

                
                DialogResult msgret = MessageBox.Show("Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (msgret == DialogResult.Yes)
                {
                   
                    int ret = com.ExecuteNonQuery();
                    LoadData(); 

                   
                    if (ret > 0)
                    {
                        MessageBox.Show("Record deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the fields after deletion
                        txtTicketId.Text = "";
                        txtpassengerid.Text = "";
                        txtflightid.Text = "";
                        txtdeplocation.Text = "";
                        txtarrivallocation.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker2.Value = DateTime.Now;
                        txtseat.Text = "";
                        comboBox1.SelectedIndex = -1;
                        txtTicketprice.Text = "";
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                    }
                    else
                    {
                        MessageBox.Show("No record found with the given Ticket ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                con.Close();
            }

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

           
            con.Open();

            
            string sql = "SELECT * FROM Tickets WHERE TicketID=@TicketID";
            SqlCommand com = new SqlCommand(sql, con);
            com.Parameters.AddWithValue("@TicketID", this.txtsearch.Text.Trim()); 
           
            SqlDataReader dr = com.ExecuteReader();

            if (dr.Read() == true)
            {
               
                    // Populate the relevant fields
                    this.txtTicketId.Text = dr.GetValue(0).ToString(); 
                    this.txtpassengerid.Text = dr.GetValue(1).ToString(); 
                    this.txtflightid.Text = dr.GetValue(2).ToString(); 
                    this.txtdeplocation.Text = dr.GetValue(3).ToString(); 
                    this.txtarrivallocation.Text = dr.GetValue(4).ToString(); 
                    this.dateTimePicker1.Value = Convert.ToDateTime(dr.GetValue(5)); 
                    this.dateTimePicker2.Value = Convert.ToDateTime(dr.GetValue(6)); 
                    this.txtseat.Text = dr.GetValue(7).ToString(); 
                    this.comboBox1.Text = dr.GetValue(8).ToString(); 
                    this.txtTicketprice.Text = dr.GetValue(9).ToString(); 

                   
                    this.radioButton1.Checked = true;
                if (dr.GetValue(10).ToString() == "Paid")
                {
                    this.radioButton1.Checked = true;
                }
                else
                {
                    this.radioButton2.Checked = true;
                }



                txtsearch.Text = "";

                
               
            }
            else
            {
               
                MessageBox.Show("No records found for the given Ticket ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtsearch.Text = "";
            }

            
            con.Close();


        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
        }
    }
}


