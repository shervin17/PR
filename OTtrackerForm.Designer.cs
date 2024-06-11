namespace PayrollV3
{
    partial class OTtrackerForm
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
            this.username_field = new System.Windows.Forms.TextBox();
            this.password_field = new System.Windows.Forms.TextBox();
            this.time_label = new System.Windows.Forms.Label();
            this.Day_label = new System.Windows.Forms.Label();
            this.date_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(253, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "time in / time out";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // username_field
            // 
            this.username_field.Location = new System.Drawing.Point(211, 160);
            this.username_field.Name = "username_field";
            this.username_field.Size = new System.Drawing.Size(220, 22);
            this.username_field.TabIndex = 1;
            // 
            // password_field
            // 
            this.password_field.Location = new System.Drawing.Point(211, 204);
            this.password_field.Name = "password_field";
            this.password_field.Size = new System.Drawing.Size(220, 22);
            this.password_field.TabIndex = 2;
            // 
            // time_label
            // 
            this.time_label.AutoSize = true;
            this.time_label.BackColor = System.Drawing.Color.Transparent;
            this.time_label.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_label.ForeColor = System.Drawing.Color.MediumBlue;
            this.time_label.Location = new System.Drawing.Point(170, 34);
            this.time_label.Name = "time_label";
            this.time_label.Size = new System.Drawing.Size(327, 70);
            this.time_label.TabIndex = 3;
            this.time_label.Text = "08:00:00 ";
            // 
            // Day_label
            // 
            this.Day_label.AutoSize = true;
            this.Day_label.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Day_label.ForeColor = System.Drawing.Color.MediumBlue;
            this.Day_label.Location = new System.Drawing.Point(178, 123);
            this.Day_label.Name = "Day_label";
            this.Day_label.Size = new System.Drawing.Size(63, 20);
            this.Day_label.TabIndex = 4;
            this.Day_label.Text = "label2";
            // 
            // date_label
            // 
            this.date_label.AutoSize = true;
            this.date_label.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_label.ForeColor = System.Drawing.Color.MediumBlue;
            this.date_label.Location = new System.Drawing.Point(247, 125);
            this.date_label.Name = "date_label";
            this.date_label.Size = new System.Drawing.Size(56, 18);
            this.date_label.TabIndex = 5;
            this.date_label.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(121, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(121, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Password";
            // 
            // OTtrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(624, 322);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.date_label);
            this.Controls.Add(this.Day_label);
            this.Controls.Add(this.time_label);
            this.Controls.Add(this.password_field);
            this.Controls.Add(this.username_field);
            this.Controls.Add(this.button1);
            this.Name = "OTtrackerForm";
            this.Text = "OTtrackerForm";
            this.Load += new System.EventHandler(this.OTtrackerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox username_field;
        private System.Windows.Forms.TextBox password_field;
        private System.Windows.Forms.Label time_label;
        private System.Windows.Forms.Label Day_label;
        private System.Windows.Forms.Label date_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}