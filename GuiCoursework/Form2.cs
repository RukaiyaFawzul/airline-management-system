using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GuiCoursework
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Registerbtn_Click(object sender, EventArgs e)
        {

               
                if (string.IsNullOrWhiteSpace(txtname.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtusername.Text) ||
                    string.IsNullOrWhiteSpace(txtpassword.Text))
                {
                    MessageBox.Show("Please fill all 4 fields!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                string password = txtpassword.Text.Trim();
                if (password.Length <= 8)
                {
                    MessageBox.Show("Password must be more than 8 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);

              
                con.Open();

               
                string sql = "INSERT INTO Users (Name, Email, Username, Passwords) VALUES (@name, @Email, @Username, @Password)";
                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@name", txtname.Text.Trim());
                com.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                com.Parameters.AddWithValue("@Username", txtusername.Text.Trim());
                com.Parameters.AddWithValue("@Password", password);

                
                int rowsAffected = com.ExecuteNonQuery();

              
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Signup Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                   
                    txtname.Text = "";
                    txtEmail.Text = "";
                    txtusername.Text = "";
                    txtpassword.Text = "";
                }
                else
                {
                   
                    MessageBox.Show("Signup failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

              
                con.Close();
            }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }



    
}
