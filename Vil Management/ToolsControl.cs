using System;
using System.Diagnostics;

namespace Vil_Management
{


    public partial class ToolsControl : UserControl
    {
        string appSumeruPath = "appsData\\sumeru.jar";
        string appSwiftPath = "appsData\\swift.jar";
        string appCposPath = "appsData\\cposOffice.jar";
        string appCposMasterPath = "appsData\\cposMaster.jar";
        string appVsearchPath = "appsData\\vSearchHit.jar";

        public ToolsControl()
        {
            InitializeComponent();
        }

        private void btnEdge_Click(object sender, EventArgs e)
        {

        }

        private void btnSumeru_Click(object sender, EventArgs e)
        {
            OpenApps(appSumeruPath);

        }

        private void OpenApps(string jarFilePath)
        {
            if (File.Exists(jarFilePath))
            {
                try
                {
                    // Start the process to run the .jar file using the "java -jar" command
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "java",  // "java" is the command to run Java applications
                        Arguments = $"-jar \"{jarFilePath}\"",  // Pass the .jar file as an argument
                        CreateNoWindow = true,  // Optionally hide the command window
                        UseShellExecute = false  // Don't use the shell to start the process
                    };

                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur when trying to start the .jar file
                    MessageBox.Show("Error launching the Java application: " + ex.Message);
                }
            }
            else
            {
                // If the .jar file doesn't exist, show an error message
                MessageBox.Show("Java file not found: " + jarFilePath);
            }
        }

        private void btnSwift_Click(object sender, EventArgs e)
        {
            OpenApps(appSwiftPath);
        }

        private void btnCpos_Click(object sender, EventArgs e)
        {
            OpenApps(appCposPath);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OpenApps(appCposMasterPath);
        }

        private void btnVsearch_Click(object sender, EventArgs e)
        {
            OpenApps(appVsearchPath);
        }

        private void btnCheckRecharge_Click(object sender, EventArgs e)
        {
            string date = dtpRecharge.Text;

            if (string.IsNullOrEmpty(date))
            {
                MessageBox.Show("Please enter a valid numeric value.");
                return;
            }

            int days = (int.Parse(tbRechargeDays.Text));
            dtpResult.Value = dtpRecharge.Value.AddDays(days-1);
        }
    }
}
