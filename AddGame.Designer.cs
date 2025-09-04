// AddGame.Designer.cs
namespace GameRentalSystem
{
    partial class AddGame
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labelVendorName = new System.Windows.Forms.Label();
            this.textBoxVendorName = new System.Windows.Forms.TextBox();
            this.labelVendorPhone = new System.Windows.Forms.Label();
            this.textBoxVendorPhone = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBox2.Location = new System.Drawing.Point(40, 55); // Adjusted Y
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(550, 30);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(36, 25); // Adjusted Y
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Enter Game Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(36, 105); // Adjusted Y
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Enter Game Date:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBox4.Location = new System.Drawing.Point(40, 215); // Adjusted Y
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(550, 30);
            this.textBox4.TabIndex = 5; // TabIndex 3
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(36, 185); // Adjusted Y
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Enter Game Stock:";
            // 
            // labelVendorName
            // 
            this.labelVendorName.AutoSize = true;
            this.labelVendorName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelVendorName.ForeColor = System.Drawing.Color.DimGray;
            this.labelVendorName.Location = new System.Drawing.Point(36, 265); // New Y
            this.labelVendorName.Name = "labelVendorName";
            this.labelVendorName.Size = new System.Drawing.Size(121, 23);
            this.labelVendorName.TabIndex = 6;
            this.labelVendorName.Text = "Vendor Name:";
            // 
            // textBoxVendorName
            // 
            this.textBoxVendorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVendorName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxVendorName.Location = new System.Drawing.Point(40, 295); // New Y
            this.textBoxVendorName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.textBoxVendorName.Name = "textBoxVendorName";
            this.textBoxVendorName.Size = new System.Drawing.Size(550, 30);
            this.textBoxVendorName.TabIndex = 7; // TabIndex 4
            // 
            // labelVendorPhone
            // 
            this.labelVendorPhone.AutoSize = true;
            this.labelVendorPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelVendorPhone.ForeColor = System.Drawing.Color.DimGray;
            this.labelVendorPhone.Location = new System.Drawing.Point(36, 345); // New Y
            this.labelVendorPhone.Name = "labelVendorPhone";
            this.labelVendorPhone.Size = new System.Drawing.Size(122, 23);
            this.labelVendorPhone.TabIndex = 8;
            this.labelVendorPhone.Text = "Vendor Phone:";
            // 
            // textBoxVendorPhone
            // 
            this.textBoxVendorPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVendorPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxVendorPhone.Location = new System.Drawing.Point(40, 375); // New Y
            this.textBoxVendorPhone.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.textBoxVendorPhone.Name = "textBoxVendorPhone";
            this.textBoxVendorPhone.Size = new System.Drawing.Size(550, 30);
            this.textBoxVendorPhone.TabIndex = 9; // TabIndex 5
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(255, 435); // Adjusted Y
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 40);
            this.button1.TabIndex = 10; // TabIndex 6
            this.button1.Text = "Add Game";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(40, 135); // Adjusted Y
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(550, 30);
            this.dateTimePicker1.TabIndex = 3; // TabIndex 2
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // AddGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(630, 510); // Increased height
            this.Controls.Add(this.textBoxVendorPhone);
            this.Controls.Add(this.labelVendorPhone);
            this.Controls.Add(this.textBoxVendorName);
            this.Controls.Add(this.labelVendorName);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddGame";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Game";
            this.Load += new System.EventHandler(this.AddGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox2; // Game Name
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox4; // Game Stock
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;   // Submit Button
        private System.Windows.Forms.DateTimePicker dateTimePicker1; // Game Date
        // New Controls for Vendor
        private System.Windows.Forms.Label labelVendorName;
        private System.Windows.Forms.TextBox textBoxVendorName;
        private System.Windows.Forms.Label labelVendorPhone;
        private System.Windows.Forms.TextBox textBoxVendorPhone;
    }
}