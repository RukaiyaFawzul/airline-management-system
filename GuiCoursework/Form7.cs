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

namespace GuiCoursework
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
           
               
            

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
             string selectedTicketID = comboBox2.SelectedItem.ToString();

                string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    string sql = "SELECT * FROM Tickets WHERE TicketID = @TicketID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@TicketID", selectedTicketID);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        // Check if the ticket is paid
                        string paymentStatus = dr["PaymentStatus"].ToString();
                        if (paymentStatus == "Paid")
                        {
                            // Populate the relevant fields
                            this.txtTicketId.Text = dr["TicketID"].ToString(); 
                            this.txtpassengerid.Text = dr["PassengerID"].ToString();
                            this.txtflightid.Text = dr["FlightID"].ToString(); 
                            this.txtdeplocation.Text = dr["DepartureLocation"].ToString(); 
                            this.txtarrivallocation.Text = dr["ArrivalDestination"].ToString(); 
                            this.dateTimePicker1.Value = Convert.ToDateTime(dr["DepartureDate"]);
                            this.dateTimePicker2.Value = Convert.ToDateTime(dr["ArrivalDate"]);
                            this.txtSeat.Text = dr["SeatNumber"].ToString(); 
                            this.comboBox1.Text = dr["SeatClass"].ToString();
                            this.txtTicketprice.Text = dr["TicketPrice"].ToString(); 

                            
                        }
                        else
                        {
                            
                            MessageBox.Show("This ticket has not been paid for. Data cannot be displayed.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtTicketId.Text = "";
                        this.txtpassengerid.Text = "";
                        this.txtflightid.Text = "";
                        this.txtdeplocation.Text = "";
                        this.txtarrivallocation.Text = "";
                        this.dateTimePicker1.Value = DateTime.Now;
                        this.dateTimePicker2.Value = DateTime.Now;
                        this.txtSeat.Text = "";
                        this.comboBox1.SelectedIndex = -1;
                        this.txtTicketprice.Text = "";
                    }
                    }
                }
            }


        private void PopulateComboBox()
        {
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string sql = "SELECT TicketID FROM Tickets";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();

                // Clear existing items and populate ComboBox
                comboBox2.Items.Clear();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["TicketID"].ToString());
                }
            }
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTicketId.Text.Trim()))
            {
                MessageBox.Show("Please enter or select a Ticket ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the TicketID from the text field
            string ticketID = txtTicketId.Text.Trim();

            // Open the Report Viewer Form and pass the TicketID
            Form11 reportViewer = new Form11(ticketID); 
            reportViewer.Show();
        }
    }

    

}
