/*
 * WinChecker 0.1.iron-alpha
 * Authors: ZAccess, adm1nn
 * (c) 2016
 */
namespace WinChecker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Header = new System.Windows.Forms.PictureBox();
            this.Scan = new System.Windows.Forms.PictureBox();
            this.Settings = new System.Windows.Forms.PictureBox();
            this.Exit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Header)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Settings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exit)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Header.BackgroundImage")));
            this.Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Header.Image = ((System.Drawing.Image)(resources.GetObject("Header.Image")));
            this.Header.Location = new System.Drawing.Point(0, -2);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(596, 60);
            this.Header.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Header.TabIndex = 0;
            this.Header.TabStop = false;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Header_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Header_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Header_MouseUp);
            // 
            // Scan
            // 
            this.Scan.Image = ((System.Drawing.Image)(resources.GetObject("Scan.Image")));
            this.Scan.Location = new System.Drawing.Point(0, 58);
            this.Scan.Name = "Scan";
            this.Scan.Size = new System.Drawing.Size(200, 200);
            this.Scan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Scan.TabIndex = 1;
            this.Scan.TabStop = false;
            this.Scan.Click += new System.EventHandler(this.Scan_Click);
            // 
            // Settings
            // 
            this.Settings.Image = ((System.Drawing.Image)(resources.GetObject("Settings.Image")));
            this.Settings.Location = new System.Drawing.Point(197, 58);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(200, 200);
            this.Settings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Settings.TabIndex = 2;
            this.Settings.TabStop = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // Exit
            // 
            this.Exit.Image = ((System.Drawing.Image)(resources.GetObject("Exit.Image")));
            this.Exit.Location = new System.Drawing.Point(396, 58);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(200, 200);
            this.Exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Exit.TabIndex = 3;
            this.Exit.TabStop = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 258);
            this.ControlBox = false;
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.Scan);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinChecker";
            ((System.ComponentModel.ISupportInitialize)(this.Header)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Settings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Header;
        private System.Windows.Forms.PictureBox Scan;
        private System.Windows.Forms.PictureBox Settings;
        private System.Windows.Forms.PictureBox Exit;
    }
}

