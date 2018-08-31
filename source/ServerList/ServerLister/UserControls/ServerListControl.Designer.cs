namespace ServerListerApp.UserControls
{
    partial class ServerListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerListControl));
            this.mainPC = new DevExpress.XtraEditors.PanelControl();
            this.mainLTP = new System.Windows.Forms.TableLayoutPanel();
            this.serverListGridControl = new DevExpress.XtraGrid.GridControl();
            this.serverListGrid = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ServerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Distance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.logoutButton = new DevExpress.XtraEditors.SimpleButton();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.sideBackground = new System.Windows.Forms.Panel();
            this.rightUpLC = new DevExpress.XtraLayout.LayoutControl();
            this.upRightLCG = new DevExpress.XtraLayout.LayoutControlGroup();
            this.logoutBtnLCI = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainPC)).BeginInit();
            this.mainPC.SuspendLayout();
            this.mainLTP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverListGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverListGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightUpLC)).BeginInit();
            this.rightUpLC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upRightLCG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtnLCI)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPC
            // 
            this.mainPC.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mainPC.Controls.Add(this.mainLTP);
            this.mainPC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPC.Location = new System.Drawing.Point(0, 0);
            this.mainPC.Name = "mainPC";
            this.mainPC.Size = new System.Drawing.Size(597, 401);
            this.mainPC.TabIndex = 0;
            // 
            // mainLTP
            // 
            this.mainLTP.BackColor = System.Drawing.Color.White;
            this.mainLTP.ColumnCount = 2;
            this.mainLTP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLTP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLTP.Controls.Add(this.rightUpLC, 1, 0);
            this.mainLTP.Controls.Add(this.serverListGridControl, 0, 1);
            this.mainLTP.Controls.Add(this.logoPanel, 0, 0);
            this.mainLTP.Controls.Add(this.sideBackground, 1, 1);
            this.mainLTP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLTP.Location = new System.Drawing.Point(2, 2);
            this.mainLTP.Name = "mainLTP";
            this.mainLTP.RowCount = 2;
            this.mainLTP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.mainLTP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLTP.Size = new System.Drawing.Size(593, 397);
            this.mainLTP.TabIndex = 2;
            // 
            // serverListGridControl
            // 
            this.serverListGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverListGridControl.Location = new System.Drawing.Point(3, 54);
            this.serverListGridControl.MainView = this.serverListGrid;
            this.serverListGridControl.Name = "serverListGridControl";
            this.serverListGridControl.Size = new System.Drawing.Size(290, 340);
            this.serverListGridControl.TabIndex = 1;
            this.serverListGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.serverListGrid});
            // 
            // serverListGrid
            // 
            this.serverListGrid.Appearance.GroupPanel.Image = ((System.Drawing.Image)(resources.GetObject("gridView1.Appearance.GroupPanel.Image")));
            this.serverListGrid.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ServerName,
            this.Distance});
            this.serverListGrid.GridControl = this.serverListGridControl;
            this.serverListGrid.Name = "serverListGrid";
            this.serverListGrid.OptionsBehavior.ReadOnly = true;
            this.serverListGrid.OptionsView.ShowGroupPanel = false;
            // 
            // ServerName
            // 
            this.ServerName.Caption = "SERVER";
            this.ServerName.FieldName = "ServerName";
            this.ServerName.Name = "ServerName";
            this.ServerName.OptionsColumn.AllowEdit = false;
            this.ServerName.Visible = true;
            this.ServerName.VisibleIndex = 0;
            this.ServerName.Width = 205;
            // 
            // Distance
            // 
            this.Distance.Caption = "DISTANCE";
            this.Distance.FieldName = "Distance";
            this.Distance.Name = "Distance";
            this.Distance.OptionsColumn.AllowEdit = false;
            this.Distance.Visible = true;
            this.Distance.VisibleIndex = 1;
            this.Distance.Width = 67;
            // 
            // logoutButton
            // 
            this.logoutButton.AllowFocus = false;
            this.logoutButton.Appearance.BackColor = System.Drawing.Color.White;
            this.logoutButton.Appearance.BackColor2 = System.Drawing.Color.White;
            this.logoutButton.Appearance.BorderColor = System.Drawing.Color.White;
            this.logoutButton.Appearance.Options.UseBackColor = true;
            this.logoutButton.Appearance.Options.UseBorderColor = true;
            this.logoutButton.AppearanceDisabled.ForeColor = System.Drawing.Color.White;
            this.logoutButton.AppearanceDisabled.Options.UseForeColor = true;
            this.logoutButton.Location = new System.Drawing.Point(0, 0);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(100, 22);
            this.logoutButton.StyleController = this.rightUpLC;
            this.logoutButton.TabIndex = 2;
            this.logoutButton.Text = "Logout";
            this.logoutButton.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // logoPanel
            // 
            this.logoPanel.BackgroundImage = global::ServerListerApp.Properties.Resources.logoBlue;
            this.logoPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoPanel.Location = new System.Drawing.Point(3, 3);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Size = new System.Drawing.Size(134, 43);
            this.logoPanel.TabIndex = 5;
            // 
            // sideBackground
            // 
            this.sideBackground.BackgroundImage = global::ServerListerApp.Properties.Resources.serverListBackground;
            this.sideBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sideBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideBackground.Location = new System.Drawing.Point(299, 54);
            this.sideBackground.Name = "sideBackground";
            this.sideBackground.Size = new System.Drawing.Size(291, 340);
            this.sideBackground.TabIndex = 6;
            // 
            // rightUpLC
            // 
            this.rightUpLC.Controls.Add(this.logoutButton);
            this.rightUpLC.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightUpLC.Location = new System.Drawing.Point(493, 0);
            this.rightUpLC.Margin = new System.Windows.Forms.Padding(0);
            this.rightUpLC.Name = "rightUpLC";
            this.rightUpLC.Root = this.upRightLCG;
            this.rightUpLC.Size = new System.Drawing.Size(100, 51);
            this.rightUpLC.TabIndex = 1;
            this.rightUpLC.Text = "layoutControl1";
            // 
            // upRightLCG
            // 
            this.upRightLCG.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.upRightLCG.GroupBordersVisible = false;
            this.upRightLCG.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.logoutBtnLCI});
            this.upRightLCG.Name = "upRightLCG";
            this.upRightLCG.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.upRightLCG.Size = new System.Drawing.Size(100, 51);
            this.upRightLCG.TextVisible = false;
            // 
            // logoutBtnLCI
            // 
            this.logoutBtnLCI.Control = this.logoutButton;
            this.logoutBtnLCI.Location = new System.Drawing.Point(0, 0);
            this.logoutBtnLCI.Name = "logoutBtnLCI";
            this.logoutBtnLCI.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.logoutBtnLCI.Size = new System.Drawing.Size(100, 51);
            this.logoutBtnLCI.TextSize = new System.Drawing.Size(0, 0);
            this.logoutBtnLCI.TextVisible = false;
            // 
            // ServerListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPC);
            this.Name = "ServerListControl";
            this.Size = new System.Drawing.Size(597, 401);
            this.Load += new System.EventHandler(this.ServerListControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPC)).EndInit();
            this.mainPC.ResumeLayout(false);
            this.mainLTP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serverListGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverListGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightUpLC)).EndInit();
            this.rightUpLC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upRightLCG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtnLCI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl mainPC;
        private DevExpress.XtraGrid.GridControl serverListGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView serverListGrid;
        private System.Windows.Forms.TableLayoutPanel mainLTP;
        private DevExpress.XtraEditors.SimpleButton logoutButton;
        private DevExpress.XtraGrid.Columns.GridColumn ServerName;
        private DevExpress.XtraGrid.Columns.GridColumn Distance;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Panel sideBackground;
        private DevExpress.XtraLayout.LayoutControl rightUpLC;
        private DevExpress.XtraLayout.LayoutControlGroup upRightLCG;
        private DevExpress.XtraLayout.LayoutControlItem logoutBtnLCI;
    }
}
