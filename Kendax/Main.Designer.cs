namespace Kendax
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.StatusLbl = new System.Windows.Forms.Label();
            this.ATimer = new System.Windows.Forms.Timer(this.components);
            this.SessionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SessionMenuSplitter = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.kendaxTabControl1 = new Kendax.Components.KendaxTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SessionViewer = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SessionMenu.SuspendLayout();
            this.kendaxTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusLbl
            // 
            this.StatusLbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusLbl.Location = new System.Drawing.Point(0, 351);
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Size = new System.Drawing.Size(508, 24);
            this.StatusLbl.TabIndex = 1;
            this.StatusLbl.Text = "Standing By...";
            this.StatusLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ATimer
            // 
            this.ATimer.Interval = 200;
            this.ATimer.Tick += new System.EventHandler(this.ATimer_Tick);
            // 
            // SessionMenu
            // 
            this.SessionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SessionMenuSplitter,
            this.SelectAllBtn});
            this.SessionMenu.Name = "SessionMenu";
            this.SessionMenu.Size = new System.Drawing.Size(123, 32);
            // 
            // SessionMenuSplitter
            // 
            this.SessionMenuSplitter.Name = "SessionMenuSplitter";
            this.SessionMenuSplitter.Size = new System.Drawing.Size(119, 6);
            // 
            // SelectAllBtn
            // 
            this.SelectAllBtn.Name = "SelectAllBtn";
            this.SelectAllBtn.Size = new System.Drawing.Size(122, 22);
            this.SelectAllBtn.Text = "Select All";
            this.SelectAllBtn.Click += new System.EventHandler(this.SelectAllBtn_Click);
            // 
            // kendaxTabControl1
            // 
            this.kendaxTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.kendaxTabControl1.Controls.Add(this.tabPage1);
            this.kendaxTabControl1.Controls.Add(this.tabPage2);
            this.kendaxTabControl1.DisplayBoundary = true;
            this.kendaxTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kendaxTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.kendaxTabControl1.ItemSize = new System.Drawing.Size(65, 24);
            this.kendaxTabControl1.Location = new System.Drawing.Point(0, 0);
            this.kendaxTabControl1.Multiline = true;
            this.kendaxTabControl1.Name = "kendaxTabControl1";
            this.kendaxTabControl1.SelectedIndex = 0;
            this.kendaxTabControl1.Size = new System.Drawing.Size(508, 351);
            this.kendaxTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.kendaxTabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ConnectBtn);
            this.tabPage1.Controls.Add(this.LoginBtn);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.SessionViewer);
            this.tabPage1.Location = new System.Drawing.Point(69, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(435, 343);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sessions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Enabled = false;
            this.ConnectBtn.Location = new System.Drawing.Point(321, 93);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(108, 23);
            this.ConnectBtn.TabIndex = 5;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // LoginBtn
            // 
            this.LoginBtn.Enabled = false;
            this.LoginBtn.Location = new System.Drawing.Point(202, 93);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(113, 23);
            this.LoginBtn.TabIndex = 3;
            this.LoginBtn.Text = "Login/Authenticate";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Location = new System.Drawing.Point(76, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 110);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Kendax.Properties.Resources.Avatar;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // SessionViewer
            // 
            this.SessionViewer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.SessionViewer.ContextMenuStrip = this.SessionMenu;
            this.SessionViewer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SessionViewer.FullRowSelect = true;
            this.SessionViewer.GridLines = true;
            this.SessionViewer.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.SessionViewer.HideSelection = false;
            this.SessionViewer.Location = new System.Drawing.Point(3, 122);
            this.SessionViewer.Name = "SessionViewer";
            this.SessionViewer.ShowItemToolTips = true;
            this.SessionViewer.Size = new System.Drawing.Size(429, 218);
            this.SessionViewer.TabIndex = 0;
            this.SessionViewer.UseCompatibleStateImageBehavior = false;
            this.SessionViewer.View = System.Windows.Forms.View.Details;
            this.SessionViewer.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.SessionViewer_ColumnWidthChanging);
            this.SessionViewer.ItemActivate += new System.EventHandler(this.SessionViewer_ItemActivate);
            this.SessionViewer.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.SessionViewer_ItemSelectionChanged);
            this.SessionViewer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SessionViewer_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Player Name";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Player ID#";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Email";
            this.columnHeader3.Width = 163;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Status";
            this.columnHeader4.Width = 85;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(69, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(435, 343);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(508, 375);
            this.Controls.Add(this.kendaxTabControl1);
            this.Controls.Add(this.StatusLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kendax ~ Sessions Connected[0/0] | Selected: 0";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SessionViewer_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SessionViewer_DragEnter);
            this.SessionMenu.ResumeLayout(false);
            this.kendaxTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label StatusLbl;
        private Components.KendaxTabControl kendaxTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView SessionViewer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer ATimer;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.ContextMenuStrip SessionMenu;
        private System.Windows.Forms.ToolStripSeparator SessionMenuSplitter;
        private System.Windows.Forms.ToolStripMenuItem SelectAllBtn;
    }
}

