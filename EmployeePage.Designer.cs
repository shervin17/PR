namespace PayrollV3
{
    partial class EmployeePage
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewDTRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyForOTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewDTRToolStripMenuItem,
            this.applyForOTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1072, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewDTRToolStripMenuItem
            // 
            this.viewDTRToolStripMenuItem.Name = "viewDTRToolStripMenuItem";
            this.viewDTRToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
            this.viewDTRToolStripMenuItem.Text = "View DTR";
            // 
            // applyForOTToolStripMenuItem
            // 
            this.applyForOTToolStripMenuItem.Name = "applyForOTToolStripMenuItem";
            this.applyForOTToolStripMenuItem.Size = new System.Drawing.Size(109, 24);
            this.applyForOTToolStripMenuItem.Text = "Apply For OT";
            this.applyForOTToolStripMenuItem.Click += new System.EventHandler(this.applyForOTToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(23, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1021, 529);
            this.panel1.TabIndex = 1;
            // 
            // EmployeePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1072, 605);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EmployeePage";
            this.Text = "EmployeePage";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewDTRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyForOTToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}