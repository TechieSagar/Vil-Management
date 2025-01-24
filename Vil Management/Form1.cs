using System.Runtime.InteropServices;

namespace Vil_Management
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreatRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
          (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
          );

        public Form1()
        {
            InitializeComponent();

            panelNav.Height = btnDashboard.Height;
            panelNav.Top = btnDashboard.Top;
            panelNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            panelNav.Height = btnDashboard.Height;
            panelNav.Top = btnDashboard.Top;
            panelNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);
            controlDasboard1.BringToFront();
            labelTitle.Text = "Dashboard";
        }

        private void btnNumberSystem_Click(object sender, EventArgs e)
        {
            panelNav.Height = btnNumberSystem.Height;
            panelNav.Top = btnNumberSystem.Top;
            panelNav.Left = btnNumberSystem.Left;
            btnNumberSystem.BackColor = Color.FromArgb(46, 51, 73);
            numberSystemControl11.BringToFront();
            labelTitle.Text = "Number System";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            panelNav.Height = btnUpdate.Height;
            panelNav.Top = btnUpdate.Top;
            panelNav.Left = btnUpdate.Left;
            btnUpdate.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            panelNav.Height = btnTools.Height;
            panelNav.Top = btnTools.Top;
            panelNav.Left = btnTools.Left;
            btnTools.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnContactUs_Click(object sender, EventArgs e)
        {
            panelNav.Height = btnContactUs.Height;
            panelNav.Top = btnContactUs.Top;
            panelNav.Left = btnContactUs.Left;
            btnContactUs.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnDashboard_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnNumberSystem_Leave(object sender, EventArgs e)
        {
            btnNumberSystem.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnUpdate_Leave(object sender, EventArgs e)
        {
            btnUpdate.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnTools_Leave(object sender, EventArgs e)
        {
            btnTools.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnContactUs.BackColor = Color.FromArgb(24, 30, 54);
        }

        
    }
}
