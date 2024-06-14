namespace PayrollV3
{
    partial class DailyTimeRecordApp
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
            this.components = new System.ComponentModel.Container();
            this.username_field = new System.Windows.Forms.TextBox();
            this.password_field = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label_time = new System.Windows.Forms.Label();
            this.day_label = new System.Windows.Forms.Label();
            this.date_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.shift_to = new PayrollV3.shift();
            this.shift_start = new PayrollV3.shift();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // username_field
            // 
            this.username_field.Location = new System.Drawing.Point(264, 263);
            this.username_field.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.username_field.Name = "username_field";
            this.username_field.Size = new System.Drawing.Size(263, 22);
            this.username_field.TabIndex = 1;
            // 
            // password_field
            // 
            this.password_field.Location = new System.Drawing.Point(264, 297);
            this.password_field.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.password_field.Name = "password_field";
            this.password_field.Size = new System.Drawing.Size(263, 22);
            this.password_field.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(321, 336);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "time in / time out";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.BackColor = System.Drawing.SystemColors.ControlText;
            this.label_time.Font = new System.Drawing.Font("Consolas", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_time.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label_time.Location = new System.Drawing.Point(187, 64);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(392, 94);
            this.label_time.TabIndex = 6;
            this.label_time.Text = "08:00:00";
            // 
            // day_label
            // 
            this.day_label.AutoSize = true;
            this.day_label.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.day_label.ForeColor = System.Drawing.SystemColors.Highlight;
            this.day_label.Location = new System.Drawing.Point(260, 182);
            this.day_label.Name = "day_label";
            this.day_label.Size = new System.Drawing.Size(43, 23);
            this.day_label.TabIndex = 7;
            this.day_label.Text = "Mon";
            // 
            // date_label
            // 
            this.date_label.AutoSize = true;
            this.date_label.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_label.ForeColor = System.Drawing.SystemColors.Highlight;
            this.date_label.Location = new System.Drawing.Point(317, 188);
            this.date_label.Name = "date_label";
            this.date_label.Size = new System.Drawing.Size(63, 15);
            this.date_label.TabIndex = 8;
            this.date_label.Text = "06-02-24";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(187, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(187, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(187, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(173, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Shift start";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(445, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "To:";
            // 
            // shift_to
            // 
            this.shift_to.BackColor = System.Drawing.Color.Transparent;
            this.shift_to.Location = new System.Drawing.Point(480, 226);
            this.shift_to.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.shift_to.Name = "shift_to";
            this.shift_to.Size = new System.Drawing.Size(176, 32);
            this.shift_to.TabIndex = 15;
            // 
            // shift_start
            // 
            this.shift_start.BackColor = System.Drawing.Color.Transparent;
            this.shift_start.Location = new System.Drawing.Point(264, 226);
            this.shift_start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.shift_start.Name = "shift_start";
            this.shift_start.Size = new System.Drawing.Size(176, 32);
            this.shift_start.TabIndex = 14;
            this.shift_start.Load += new System.EventHandler(this.shift_start_Load);
            // 
            // DailyTimeRecordApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(772, 414);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.shift_to);
            this.Controls.Add(this.shift_start);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.date_label);
            this.Controls.Add(this.day_label);
            this.Controls.Add(this.label_time);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.password_field);
            this.Controls.Add(this.username_field);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DailyTimeRecordApp";
            this.Text = "DailyTimeRecordApp";
            this.Load += new System.EventHandler(this.DailyTimeRecordApp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox username_field;
        private System.Windows.Forms.TextBox password_field;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label day_label;
        private System.Windows.Forms.Label date_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource bindingSource1;
        private shift shift_start;
        private shift shift_to;
        private System.Windows.Forms.Label label5;
    }
}