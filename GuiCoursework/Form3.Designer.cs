
namespace GuiCoursework
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Flightbtn = new System.Windows.Forms.Button();
            this.Passengerbtn = new System.Windows.Forms.Button();
            this.bookticketbtn = new System.Windows.Forms.Button();
            this.TicketGeneratebtn = new System.Windows.Forms.Button();
            this.Logoutbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Flightbtn
            // 
            this.Flightbtn.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Flightbtn.Location = new System.Drawing.Point(168, 85);
            this.Flightbtn.Margin = new System.Windows.Forms.Padding(5);
            this.Flightbtn.Name = "Flightbtn";
            this.Flightbtn.Size = new System.Drawing.Size(279, 70);
            this.Flightbtn.TabIndex = 0;
            this.Flightbtn.Text = "Flight Management";
            this.Flightbtn.UseVisualStyleBackColor = true;
            this.Flightbtn.Click += new System.EventHandler(this.Flightbtn_Click);
            // 
            // Passengerbtn
            // 
            this.Passengerbtn.Location = new System.Drawing.Point(168, 194);
            this.Passengerbtn.Margin = new System.Windows.Forms.Padding(5);
            this.Passengerbtn.Name = "Passengerbtn";
            this.Passengerbtn.Size = new System.Drawing.Size(279, 69);
            this.Passengerbtn.TabIndex = 1;
            this.Passengerbtn.Text = "Passenger Management";
            this.Passengerbtn.UseVisualStyleBackColor = true;
            this.Passengerbtn.Click += new System.EventHandler(this.Passengerbtn_Click);
            // 
            // bookticketbtn
            // 
            this.bookticketbtn.Location = new System.Drawing.Point(168, 311);
            this.bookticketbtn.Name = "bookticketbtn";
            this.bookticketbtn.Size = new System.Drawing.Size(279, 70);
            this.bookticketbtn.TabIndex = 2;
            this.bookticketbtn.Text = "Ticket Booking";
            this.bookticketbtn.UseVisualStyleBackColor = true;
            this.bookticketbtn.Click += new System.EventHandler(this.bookticketbtn_Click);
            // 
            // TicketGeneratebtn
            // 
            this.TicketGeneratebtn.Location = new System.Drawing.Point(168, 430);
            this.TicketGeneratebtn.Name = "TicketGeneratebtn";
            this.TicketGeneratebtn.Size = new System.Drawing.Size(279, 70);
            this.TicketGeneratebtn.TabIndex = 3;
            this.TicketGeneratebtn.Text = "Ticket Generation";
            this.TicketGeneratebtn.UseVisualStyleBackColor = true;
            this.TicketGeneratebtn.Click += new System.EventHandler(this.TicketGeneratebtn_Click);
            // 
            // Logoutbtn
            // 
            this.Logoutbtn.Location = new System.Drawing.Point(902, 23);
            this.Logoutbtn.Name = "Logoutbtn";
            this.Logoutbtn.Size = new System.Drawing.Size(138, 40);
            this.Logoutbtn.TabIndex = 4;
            this.Logoutbtn.Text = "Logout";
            this.Logoutbtn.UseVisualStyleBackColor = true;
            this.Logoutbtn.Click += new System.EventHandler(this.Logoutbtn_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GuiCoursework.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1052, 602);
            this.Controls.Add(this.Logoutbtn);
            this.Controls.Add(this.TicketGeneratebtn);
            this.Controls.Add(this.bookticketbtn);
            this.Controls.Add(this.Passengerbtn);
            this.Controls.Add(this.Flightbtn);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashbaord";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Flightbtn;
        private System.Windows.Forms.Button Passengerbtn;
        private System.Windows.Forms.Button bookticketbtn;
        private System.Windows.Forms.Button TicketGeneratebtn;
        private System.Windows.Forms.Button Logoutbtn;
    }
}