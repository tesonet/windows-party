namespace ServerListerApp
{
    partial class MainForm
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
            this.mainPC = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.mainPC)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPC
            // 
            this.mainPC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPC.Location = new System.Drawing.Point(0, 0);
            this.mainPC.Name = "mainPC";
            this.mainPC.Size = new System.Drawing.Size(544, 355);
            this.mainPC.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 355);
            this.Controls.Add(this.mainPC);
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Testio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl mainPC;
    }
}

