// Signup.Designer.cs
namespace GameRentalSystem
{
    partial class Signup
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
            this.UserName = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.sign_up = new System.Windows.Forms.Button();
            this.Address = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.TextBox();
            this.admin = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserName
            // 
            this.UserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.UserName.Location = new System.Drawing.Point(40, 65);
            this.UserName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 15);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(380, 30);
            this.UserName.TabIndex = 0;
            this.UserName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // password
            // 
            this.password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.password.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.password.Location = new System.Drawing.Point(40, 195);
            this.password.Margin = new System.Windows.Forms.Padding(4, 4, 4, 15);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(380, 30);
            this.password.TabIndex = 2; // Changed TabIndex
            this.password.UseSystemPasswordChar = true;
            this.password.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // sign_up
            // 
            this.sign_up.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sign_up.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.sign_up.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sign_up.FlatAppearance.BorderSize = 0;
            this.sign_up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sign_up.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.sign_up.ForeColor = System.Drawing.Color.White;
            this.sign_up.Location = new System.Drawing.Point(40, 360);
            this.sign_up.Margin = new System.Windows.Forms.Padding(4);
            this.sign_up.Name = "sign_up";
            this.sign_up.Size = new System.Drawing.Size(380, 40);
            this.sign_up.TabIndex = 5; // Changed TabIndex
            this.sign_up.Text = "Sign Up";
            this.sign_up.UseVisualStyleBackColor = false;
            this.sign_up.Click += new System.EventHandler(this.sign_up_Click);
            // 
            // Address
            // 
            this.Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Address.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Address.Location = new System.Drawing.Point(40, 260);
            this.Address.Margin = new System.Windows.Forms.Padding(4, 4, 4, 15);
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(380, 30);
            this.Address.TabIndex = 3; // Changed TabIndex
            this.Address.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // Email
            // 
            this.Email.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Email.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Email.Location = new System.Drawing.Point(40, 130);
            this.Email.Margin = new System.Windows.Forms.Padding(4, 4, 4, 15);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(380, 30);
            this.Email.TabIndex = 1; // Changed TabIndex
            // 
            // admin
            // 
            this.admin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.admin.AutoSize = true;
            this.admin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.admin.Location = new System.Drawing.Point(40, 315); // Adjusted position
            this.admin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 20);
            this.admin.Name = "admin";
            this.admin.Size = new System.Drawing.Size(236, 24);
            this.admin.TabIndex = 4; // Changed TabIndex
            this.admin.Text = "Create as Administrator Account";
            this.admin.UseVisualStyleBackColor = true;
            this.admin.CheckedChanged += new System.EventHandler(this.admin_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label1.Location = new System.Drawing.Point(36, 40); // Adjusted position
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "User Name:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label2.Location = new System.Drawing.Point(36, 105); // Adjusted position
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Email:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(36, 235); // Adjusted position
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label4.Location = new System.Drawing.Point(36, 170); // Adjusted position
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password:";
            // 
            // Signup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(460, 440); // Adjusted ClientSize
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.admin);
            this.Controls.Add(this.Address);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.sign_up);
            this.Controls.Add(this.password);
            this.Controls.Add(this.UserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Signup";
            this.Padding = new System.Windows.Forms.Padding(30);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Account"; // Changed Form Title
            this.Load += new System.EventHandler(this.Signup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Control names are kept the same as your original
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button sign_up;
        private System.Windows.Forms.TextBox Address;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.CheckBox admin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}