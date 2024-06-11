namespace PayrollV3
{
    partial class OTRequestForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1_reasons = new System.Windows.Forms.ComboBox();
            this.Reason_Others_field = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reason";
            // 
            // comboBox1_reasons
            // 
            this.comboBox1_reasons.FormattingEnabled = true;
            this.comboBox1_reasons.Items.AddRange(new object[] {
            "EXPEDITE PROCESS / CLIENT REQUEST",
            "STAFF SHORTAGE",
            "UNEXPECTED WORKLOAD",
            "TRAININGS OR MEETINGS",
            "Deadline Pressure:",
            "Financial Incentives",
            "OTHER REASON"});
            this.comboBox1_reasons.Location = new System.Drawing.Point(99, 23);
            this.comboBox1_reasons.Name = "comboBox1_reasons";
            this.comboBox1_reasons.Size = new System.Drawing.Size(194, 24);
            this.comboBox1_reasons.TabIndex = 1;
            // 
            // Reason_Others_field
            // 
            this.Reason_Others_field.Location = new System.Drawing.Point(26, 104);
            this.Reason_Others_field.Multiline = true;
            this.Reason_Others_field.Name = "Reason_Others_field";
            this.Reason_Others_field.Size = new System.Drawing.Size(343, 139);
            this.Reason_Others_field.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "for other reason ,  Please specify";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OTRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(786, 398);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Reason_Others_field);
            this.Controls.Add(this.comboBox1_reasons);
            this.Controls.Add(this.label1);
            this.Name = "OTRequestForm";
            this.Text = "OTRequestForm";
            this.Load += new System.EventHandler(this.OTRequestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1_reasons;
        private System.Windows.Forms.TextBox Reason_Others_field;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}