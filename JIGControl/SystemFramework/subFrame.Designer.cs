namespace QCInventoryF2
{
    partial class subFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(subFrame));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.guna2VSeparator1 = new Guna.UI2.WinForms.Guna2VSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFOrm = new Guna.UI2.WinForms.Guna2Button();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUser = new Guna.UI2.WinForms.Guna2ImageButton();
            this.userMenu = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.accountSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.lblTittle = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.userMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.guna2VSeparator1);
            this.panelMenu.Controls.Add(this.label1);
            this.panelMenu.Controls.Add(this.btnFOrm);
            this.panelMenu.Controls.Add(this.btnDashboard);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(240, 614);
            this.panelMenu.TabIndex = 0;
            // 
            // guna2VSeparator1
            // 
            this.guna2VSeparator1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2VSeparator1.Location = new System.Drawing.Point(230, 0);
            this.guna2VSeparator1.Name = "guna2VSeparator1";
            this.guna2VSeparator1.Size = new System.Drawing.Size(10, 614);
            this.guna2VSeparator1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "JIG Control";
            // 
            // btnFOrm
            // 
            this.btnFOrm.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFOrm.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFOrm.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFOrm.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFOrm.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFOrm.ForeColor = System.Drawing.Color.White;
            this.btnFOrm.Location = new System.Drawing.Point(16, 142);
            this.btnFOrm.Name = "btnFOrm";
            this.btnFOrm.Size = new System.Drawing.Size(209, 38);
            this.btnFOrm.TabIndex = 1;
            this.btnFOrm.Text = "Manage JIG";
            this.btnFOrm.Click += new System.EventHandler(this.btnFOrm_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDashboard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(16, 98);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(209, 38);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Home";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUser);
            this.panel2.Controls.Add(this.guna2Separator1);
            this.panel2.Controls.Add(this.lblTittle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(240, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(896, 78);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnUser
            // 
            this.btnUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUser.BackColor = System.Drawing.Color.Transparent;
            this.btnUser.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnUser.ContextMenuStrip = this.userMenu;
            this.btnUser.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btnUser.HoverState.ImageSize = new System.Drawing.Size(33, 33);
            this.btnUser.Image = ((System.Drawing.Image)(resources.GetObject("btnUser.Image")));
            this.btnUser.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnUser.ImageRotate = 0F;
            this.btnUser.ImageSize = new System.Drawing.Size(32, 32);
            this.btnUser.Location = new System.Drawing.Point(854, 3);
            this.btnUser.Name = "btnUser";
            this.btnUser.PressedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.btnUser.PressedState.ImageSize = new System.Drawing.Size(31, 31);
            this.btnUser.Size = new System.Drawing.Size(39, 37);
            this.btnUser.TabIndex = 2;
            this.btnUser.UseTransparentBackground = true;
            this.btnUser.Click += new System.EventHandler(this.guna2ImageButton1_Click);
            // 
            // userMenu
            // 
            this.userMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.userMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountSettingsToolStripMenuItem});
            this.userMenu.Name = "userMenu";
            this.userMenu.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.userMenu.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.userMenu.RenderStyle.ColorTable = null;
            this.userMenu.RenderStyle.RoundedEdges = true;
            this.userMenu.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.userMenu.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.userMenu.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.userMenu.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.userMenu.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.userMenu.Size = new System.Drawing.Size(194, 30);
            // 
            // accountSettingsToolStripMenuItem
            // 
            this.accountSettingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountSettingsToolStripMenuItem.Name = "accountSettingsToolStripMenuItem";
            this.accountSettingsToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.accountSettingsToolStripMenuItem.Text = "Account Settings";
            this.accountSettingsToolStripMenuItem.Click += new System.EventHandler(this.accountSettingsToolStripMenuItem_Click);
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Separator1.Location = new System.Drawing.Point(0, 68);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(896, 10);
            this.guna2Separator1.TabIndex = 1;
            // 
            // lblTittle
            // 
            this.lblTittle.AutoSize = true;
            this.lblTittle.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTittle.Location = new System.Drawing.Point(6, 28);
            this.lblTittle.Name = "lblTittle";
            this.lblTittle.Size = new System.Drawing.Size(80, 37);
            this.lblTittle.TabIndex = 0;
            this.lblTittle.Text = "Tittle";
            // 
            // panelForm
            // 
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(240, 78);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(896, 536);
            this.panelForm.TabIndex = 2;
            // 
            // subFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 614);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelMenu);
            this.Name = "subFrame";
            this.Text = "subFrame";
            this.Load += new System.EventHandler(this.subFrame_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.userMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelForm;
        private Guna.UI2.WinForms.Guna2Button btnFOrm;
        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private System.Windows.Forms.Label lblTittle;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ImageButton btnUser;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip userMenu;
        private System.Windows.Forms.ToolStripMenuItem accountSettingsToolStripMenuItem;
        private Guna.UI2.WinForms.Guna2VSeparator guna2VSeparator1;
    }
}