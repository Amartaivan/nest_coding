/*
 * WinChecker 0.3.spata-alpha
 * Authors: ZAccess, adm1nn
 * (c) 2016
 */
using System;
using System.Windows.Forms;

namespace WinChecker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ToolTip wButton_Exit_Tip = new ToolTip();
            wButton_Exit_Tip.SetToolTip(Exit, "Выход");
            ToolTip wButton_Scan_Tip = new ToolTip();
            wButton_Exit_Tip.SetToolTip(Scan, "Сканировать");
            ToolTip wButton_Settings_Tip = new ToolTip();
            wButton_Exit_Tip.SetToolTip(Settings, "В разработке...");
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Scan_Click(object sender, EventArgs e)
        {
            Scan.Enabled = false;
            Exit.Enabled = false;
            Settings.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            API.Log.CreateLog();

            Application.Exit();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("На данный момент данная функция находится в разработке...",
                "В разработке...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region MovingHeader
        bool wIsMoving = false;
        System.Drawing.Point wMouseOffset = new System.Drawing.Point(0, 0);

        private void Header_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                wIsMoving = false;
            }
        }
        private void Header_MouseMove(object sender, MouseEventArgs e)
        {
            if (wIsMoving)
            {
                System.Drawing.Point mousePos = MousePosition;
                mousePos.Offset(wMouseOffset.X, wMouseOffset.Y);
                Location = mousePos;
            }
        }
        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight -
                    SystemInformation.FrameBorderSize.Height;
                wMouseOffset = new System.Drawing.Point(xOffset, yOffset);
                wIsMoving = true;
            }
        }
        #endregion MovingHeader
    }
}
