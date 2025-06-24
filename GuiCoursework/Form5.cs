
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GuiCoursework
{
    public partial class Form5 : Form
    {
        public Form5()
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

                
                string sql = "SELECT * FROM Passengers";
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

        private void btnadd_Click(object sender, EventArgs e)
        {
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            con.Open();

            

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(txtId.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtfirstname.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtLastName.Text.Trim()) ||
                Cmbnationality.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtNic.Text.Trim()) ||
                string.IsNullOrWhiteSpace(dateTimePicker1.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtContact.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtEmail.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtAddress.Text.Trim()))
            {
                MessageBox.Show("All fields must be filled in.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //id validation 
            if (!Regex.IsMatch(txtId.Text.Trim(), @"^P\d+$"))
            {
                MessageBox.Show("Passenger ID must begin with 'P' followed by numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate First Name and Last Name (only letters)
            if (!Regex.IsMatch(txtfirstname.Text.Trim(), @"^[A-Za-z]+$"))
            {
                MessageBox.Show("First Name must contain only letters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Regex.IsMatch(txtLastName.Text.Trim(), @"^[A-Za-z]+$"))
            {
                MessageBox.Show("Last Name must contain only letters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Contact Number (only numbers)
            if (!Regex.IsMatch(txtContact.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Contact Number must contain only numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Email format
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Gender selection
            string gender = "";
            if (rbMale.Checked)
            {
                gender = "M";
            }
            else if (rbFemale.Checked)
            {
                gender = "F";
            }
            else
            {
                MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          
            string sql = "INSERT INTO Passengers (PassengerID, FirstName, LastName, Nationality, NICPassportNo, DateOfBirth, Gender, ContactNo, Email, Address) " +
                         "VALUES (@PassengerID, @FirstName, @LastName, @Nationality, @NICPassportNo, @DateOfBirth, @Gender, @ContactNo, @Email, @Address)";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@PassengerID", txtId.Text.Trim());
            com.Parameters.AddWithValue("@FirstName", txtfirstname.Text.Trim());
            com.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
            com.Parameters.AddWithValue("@Nationality", Cmbnationality.SelectedItem.ToString().Trim());
            com.Parameters.AddWithValue("@NICPassportNo", txtNic.Text.Trim());
            com.Parameters.AddWithValue("@DateOfBirth", DateTime.Parse(dateTimePicker1.Text.Trim()));
            com.Parameters.AddWithValue("@Gender", gender);
            com.Parameters.AddWithValue("@ContactNo", txtContact.Text.Trim());
            com.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            com.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());

            int rowsInserted = com.ExecuteNonQuery();
            LoadData();

            if (rowsInserted > 0)
            {
                MessageBox.Show("Passenger details added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields after successful insertion
                txtId.Text = "";
                txtfirstname.Text = "";
                txtLastName.Text = "";
                Cmbnationality.SelectedIndex = -1;
                txtNic.Text = "";
                dateTimePicker1.Text = "";
                rbMale.Checked = false;
                rbFemale.Checked = false;
                txtContact.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";
            }
            else
            {
                MessageBox.Show("Failed to add passenger details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();


        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            con.Open();

     

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(txtId.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtfirstname.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtLastName.Text.Trim()) ||
                Cmbnationality.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtNic.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtContact.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtEmail.Text.Trim()) ||
                string.IsNullOrWhiteSpace(txtAddress.Text.Trim()))
            {
                MessageBox.Show("All fields must be filled in.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(txtId.Text.Trim(), @"^P\d+$"))
            {
                MessageBox.Show("Passenger ID must begin with 'P'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate First Name and Last Name (only letters)
            if (!Regex.IsMatch(txtfirstname.Text.Trim(), @"^[A-Za-z]+$"))
            {
                MessageBox.Show("First Name must contain only letters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Regex.IsMatch(txtLastName.Text.Trim(), @"^[A-Za-z]+$"))
            {
                MessageBox.Show("Last Name must contain only letters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Contact Number (only numbers)
            if (!Regex.IsMatch(txtContact.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Contact Number must contain only numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Email format
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate Gender selection
            string gender = "";
            if (rbMale.Checked)
            {
                gender = "M";
            }
            else if (rbFemale.Checked)
            {
                gender = "F";
            }
            else
            {
                MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            string sql = "UPDATE Passengers SET FirstName=@FirstName, LastName=@LastName, Nationality=@Nationality, NICPassportNo=@NICPassportNo, " +
                         "DateOfBirth=@DateOfBirth, Gender=@Gender, ContactNo=@ContactNo, Email=@Email, Address=@Address WHERE PassengerID=@PassengerID";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@PassengerID", txtId.Text.Trim());
            com.Parameters.AddWithValue("@FirstName", txtfirstname.Text.Trim());
            com.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
            com.Parameters.AddWithValue("@Nationality", Cmbnationality.SelectedItem?.ToString()?.Trim());
            com.Parameters.AddWithValue("@NICPassportNo", txtNic.Text.Trim());
            com.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value);
            com.Parameters.AddWithValue("@Gender", gender);
            com.Parameters.AddWithValue("@ContactNo", txtContact.Text.Trim());
            com.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            com.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());

            int rowsUpdated = com.ExecuteNonQuery();
            LoadData();

           
            if (rowsUpdated > 0)
            {
                MessageBox.Show($"Number of records updated: {rowsUpdated}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Text = "";
                txtfirstname.Text = "";
                txtLastName.Text = "";
                Cmbnationality.SelectedIndex = -1;
                txtNic.Text = "";
                dateTimePicker1.Text = "";
                rbMale.Checked = false;
                rbFemale.Checked = false;
                txtContact.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";

            }
            else
            {
                MessageBox.Show("No records were updated. Please check the Passenger ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
            con.Close();


        }

        private void btndlt_Click(object sender, EventArgs e)
        {
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);


            con.Open();


            string sql = "DELETE FROM Passengers WHERE PassengerID=@PassengerID";
            SqlCommand com = new SqlCommand(sql, con);


            com.Parameters.AddWithValue("@PassengerID", txtId.Text.Trim());


            DialogResult msgret = MessageBox.Show("Are you sure you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (msgret == DialogResult.Yes)
            {

                int ret = com.ExecuteNonQuery();
                LoadData();



                if (ret > 0)
                {
                    MessageBox.Show("Record deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtId.Text = "";
                    txtfirstname.Text = "";
                    txtLastName.Text = "";
                    Cmbnationality.SelectedIndex = -1;
                    txtNic.Text = "";
                    dateTimePicker1.Text = "";
                    rbMale.Checked = false;
                    rbFemale.Checked = false;
                    txtContact.Text = "";
                    txtEmail.Text = "";
                    txtAddress.Text = "";

                }
                else
                {
                    MessageBox.Show("No record found with the given Passenger ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



            con.Close();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            try
            {
               
                con.Open();

              
                string sql = "SELECT * FROM Passengers WHERE PassengerID=@PassengerID";
                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@PassengerID", this.txtsearch.Text.Trim()); 

                
                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read() == true)
                {
                    // the relevant fields getting data
                    this.txtId.Text = dr.GetValue(0).ToString(); 
                    this.txtfirstname.Text = dr.GetValue(1).ToString();  
                    this.txtLastName.Text = dr.GetValue(2).ToString();    
                    this.Cmbnationality.Text = dr.GetValue(3).ToString(); 
                    this.txtNic.Text = dr.GetValue(4).ToString(); 
                    this.dateTimePicker1.Value = Convert.ToDateTime(dr.GetValue(5)); 
                    if (dr.GetValue(6).ToString() == "M")
                    {
                        this.rbMale.Checked = true; 
                    }
                    else
                    {
                        this.rbFemale.Checked = true; 
                    }
                    this.txtContact.Text = dr.GetValue(7).ToString();   
                    this.txtEmail.Text = dr.GetValue(8).ToString();      
                    this.txtAddress.Text = dr.GetValue(9).ToString();
                    txtsearch.Text = "";

                }
               
                else
                {
                   
                    MessageBox.Show("No records found for the given Passenger ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtsearch.Text="";
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

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
