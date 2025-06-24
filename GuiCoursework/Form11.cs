using CrystalDecisions.CrystalReports.Engine;
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

namespace GuiCoursework
{
    public partial class Form11 : Form
    {
        private string _ticketID;

        public Form11(string ticketID)
        {
            InitializeComponent();
            _ticketID = ticketID;
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            string cs = @"Data Source=ASUS-NEW\SQLEXPRESS02;Initial Catalog=AirlineManagementSystem;Integrated Security=True";

            // Query to fetch data for the selected TicketID
            string sql = "SELECT * FROM Tickets WHERE TicketID = @TicketID";

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@TicketID", _ticketID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // Load the Crystal Report
                    ReportDocument rpt = new ReportDocument();
                    rpt.Load(@"C:\Users\Rukaiya Fawzul\source\repos\GuiCoursework\GuiCoursework\CrystalReport4.rpt"); 

                    // Set the data source for the report
                    rpt.SetDataSource(dt);

                    // Show the report in the Crystal Report Viewer
                    crystalReportViewer1.ReportSource = rpt;
                }
                else
                {
                    MessageBox.Show("No data found for the selected Ticket ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }


            }
        }
    }
}

