namespace PayrollV3
{
    partial class shift
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hours_option = new System.Windows.Forms.ComboBox();
            this.mins_options = new System.Windows.Forms.ComboBox();
            this.AMorPM = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // hours_option
            // 
            this.hours_option.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hours_option.FormattingEnabled = true;
            this.hours_option.Location = new System.Drawing.Point(3, 3);
            this.hours_option.Margin = new System.Windows.Forms.Padding(1);
            this.hours_option.Name = "hours_option";
            this.hours_option.Size = new System.Drawing.Size(58, 24);
            this.hours_option.TabIndex = 0;
            // 
            // mins_options
            // 
            this.mins_options.FormattingEnabled = true;
            this.mins_options.Location = new System.Drawing.Point(67, 3);
            this.mins_options.Name = "mins_options";
            this.mins_options.Size = new System.Drawing.Size(46, 24);
            this.mins_options.TabIndex = 1;
            // 
            // AMorPM
            // 
            this.AMorPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AMorPM.FormattingEnabled = true;
            this.AMorPM.Location = new System.Drawing.Point(119, 3);
            this.AMorPM.Margin = new System.Windows.Forms.Padding(1);
            this.AMorPM.Name = "AMorPM";
            this.AMorPM.Size = new System.Drawing.Size(52, 24);
            this.AMorPM.TabIndex = 2;
            // 
            // shift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.AMorPM);
            this.Controls.Add(this.mins_options);
            this.Controls.Add(this.hours_option);
            this.Name = "shift";
            this.Size = new System.Drawing.Size(176, 32);
            this.Load += new System.EventHandler(this.shift_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox hours_option;
        private System.Windows.Forms.ComboBox mins_options;
        private System.Windows.Forms.ComboBox AMorPM;
    }
}
