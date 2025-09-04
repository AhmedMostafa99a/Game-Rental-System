// AdminWelcome.Designer.cs
namespace GameRentalSystem
{
    partial class AdminWelcome
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
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button(); // This is "Delete Games" in your original
            this.button2 = new System.Windows.Forms.Button(); // This is "Update Game Details"
            this.label1 = new System.Windows.Forms.Label();
            this.report = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(50, 150);
            this.button1.Margin = new System.Windows.Forms.Padding(10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(280, 70);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Games";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69))))); // Red for delete
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(50, 330); // Positioned below update
            this.button3.Margin = new System.Windows.Forms.Padding(10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(280, 70);
            this.button3.TabIndex = 2; // Adjusted TabIndex
            this.button3.Text = "Delete Games";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button2_Click); // Original event handler name
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7))))); // Amber for update
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.Black; // Dark text on light button
            this.button2.Location = new System.Drawing.Point(50, 240); // Positioned below add
            this.button2.Margin = new System.Windows.Forms.Padding(10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(280, 70);
            this.button2.TabIndex = 1; // Adjusted TabIndex
            this.button2.Text = "Update Game Details";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button3_Click); // Original event handler name
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 70);
            this.label1.TabIndex = 3;
            this.label1.Text = "Welcome, Admin!"; // Personalized
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // report
            // 
            this.report.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184))))); // Teal for report
            this.report.Cursor = System.Windows.Forms.Cursors.Hand;
            this.report.FlatAppearance.BorderSize = 0;
            this.report.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.report.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.report.ForeColor = System.Drawing.Color.White;
            this.report.Location = new System.Drawing.Point(50, 420); // Positioned below delete
            this.report.Margin = new System.Windows.Forms.Padding(10);
            this.report.Name = "report";
            this.report.Size = new System.Drawing.Size(280, 70);
            this.report.TabIndex = 3; // Adjusted TabIndex
            this.report.Text = "Generate Sales Report";
            this.report.UseVisualStyleBackColor = false;
            this.report.Click += new System.EventHandler(this.button4_Click);
            // 
            // AdminWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250))))); // Light gray background
            this.ClientSize = new System.Drawing.Size(380, 540); // Adjusted for a vertical layout
            this.Controls.Add(this.report);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AdminWelcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard"; // Changed Form Title
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        // Control names are kept the same as your original
        private System.Windows.Forms.Button button1; // Add Games
        private System.Windows.Forms.Button button3; // Delete Games (Note: its Click event is button2_Click in your original)
        private System.Windows.Forms.Button button2; // Update Game Details (Note: its Click event is button3_Click in your original)
        private System.Windows.Forms.Label label1;   // Welcome Label
        private System.Windows.Forms.Button report;  // Generate Sales Report
    }
}