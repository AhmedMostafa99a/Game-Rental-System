namespace GameRentalSystem
{
    partial class Main
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
            this.Title = new System.Windows.Forms.Label();
            this.log_in = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.help = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Title.Font = new System.Drawing.Font("Rockwell", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(276, 20);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(477, 117);
            this.Title.TabIndex = 0;
            this.Title.Text = "Potato Game Rental System";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Title.Click += new System.EventHandler(this.label1_Click);
            // 
            // log_in
            // 
            this.log_in.BackColor = System.Drawing.Color.MediumTurquoise;
            this.log_in.Font = new System.Drawing.Font("Stencil", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_in.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.log_in.Location = new System.Drawing.Point(158, 348);
            this.log_in.Name = "log_in";
            this.log_in.Size = new System.Drawing.Size(275, 119);
            this.log_in.TabIndex = 2;
            this.log_in.Text = "Log In";
            this.log_in.UseVisualStyleBackColor = false;
            this.log_in.Click += new System.EventHandler(this.log_in_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.button2.Font = new System.Drawing.Font("Stencil", 16.2F);
            this.button2.Location = new System.Drawing.Point(582, 346);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(276, 123);
            this.button2.TabIndex = 2;
            this.button2.Text = "Sign Up";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Mongolian Baiti", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(158, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(700, 219);
            this.label1.TabIndex = 3;
            this.label1.Text = "Welcome to Potato – Your Ultimate Game Rental Hub!\r\nRent. Play. Repeat.\r\n Dive in" +
    "to your next adventure with just a click!\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // help
            // 
            this.help.Location = new System.Drawing.Point(948, 12);
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(75, 28);
            this.help.TabIndex = 4;
            this.help.Text = "Help";
            this.help.UseVisualStyleBackColor = true;
            this.help.Click += new System.EventHandler(this.help_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 517);
            this.Controls.Add(this.help);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.log_in);
            this.Controls.Add(this.Title);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button log_in;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button help;
    }
}

