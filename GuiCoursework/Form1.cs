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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void registerpgbtn_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {

           
                // Validation
                if (string.IsNullOrEmpty(txtusername.Text) || string.IsNullOrEmpty(txtpassword.Text))
                {
                    MessageBox.Show("Please enter both Username and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);

               
                con.Open();

               
                string sql = "SELECT * FROM Users WHERE Username = @username AND Passwords = @password";
                SqlCommand com = new SqlCommand(sql, con);
                com.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                com.Parameters.AddWithValue("@password", txtpassword.Text.Trim());

                
                SqlDataReader dr = com.ExecuteReader();

                if (dr.HasRows)
            { 
                  

                    
                    Form3 form3 = new Form3();
                    form3.Show();
                    this.Hide();
                }
                else
                {
                   
                    MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

               
                dr.Close();
                con.Close();
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
