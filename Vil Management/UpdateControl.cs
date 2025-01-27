using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Vil_Management
{
    public partial class UpdateControl : UserControl
    {
        string filePathDashboard = "Dashboard.csv";


        public UpdateControl()
        {
            InitializeComponent();
        }

        private void btnUpdateSale_Click(object sender, EventArgs e)
        {
            string data = tbUpdateSale.Text;
            bool fileExists = File.Exists(filePathDashboard);
            // Open the CSV file for appending
            using (StreamWriter writer = new StreamWriter(filePathDashboard, append: true))
            {
                // If the file doesn't exist, write the header row (skip if header already exists)
                if (!fileExists)
                {
                    // Write headers to the file
                    writer.WriteLine();  // Update with your columns
                }

                // Write the data from the TextBox to the CSV
                // Assuming that the data is comma-separated
                writer.WriteLine(data[0]);
                tbUpdateTarget.Text = data;
            }

            // Optionally, you can give feedback to the user that data was saved
            MessageBox.Show("Data saved to CSV!");
        }

        public void AppendDataToExcel(string filePath)
        {
            // Sample data to append (replace this with your actual data)
            List<string[]> newData = new List<string[]>()
                {
                    new string[] { "4", "David", "40" }
                };

            // Ensure EPPlus is used with license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.NonCommercial

            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists)
            {
                // Open the existing Excel file
                using (var package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Access the first worksheet

                    // Find the last used row in the worksheet
                    int lastRow = worksheet.Dimension?.End.Row ?? 0;

                    // Append new data starting from the next row
                    for (int i = 0; i < newData.Count; i++)
                    {
                        for (int j = 0; j < newData[i].Length; j++)
                        {
                            worksheet.Cells[lastRow + i + 1, j + 1].Value = newData[i][j];
                        }
                    }

                    // Save the updated Excel file
                    package.Save();
                }
            }
            else
            {
                // If the file doesn't exist, create a new one and add data
                CreateNewExcelFileWithSpecificColumns(filePath, newData);
            }
        }

        public void AppendDataToSpecificColumns(string filePath)
        {
            // Sample data to append (replace this with your actual data)
            List<string[]> newData = new List<string[]>()
    {
        new string[] { "4", "David", "40" },
        new string[] { "5", "Eve", "28" }
    };

            // Specify the columns where data will be appended (e.g., Column 1 for ID, Column 2 for Name, Column 3 for Age)
            int columnId = 1;   // Column A
            int columnName = 2; // Column B
            int columnAge = 3;  // Column C

            // Ensure EPPlus is used with license context
            ExcelPackage.LicenseContext = LicenseContext.Commercial; // or LicenseContext.NonCommercial

            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists)
            {
                // Open the existing Excel file
                using (var package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Access the first worksheet

                    // Find the last used row in the worksheet
                    int lastRow = worksheet.Dimension?.End.Row ?? 0;

                    // Append new data to specified columns
                    for (int i = 0; i < newData.Count; i++)
                    {
                        // ID in column 1 (A), Name in column 2 (B), Age in column 3 (C)
                        worksheet.Cells[lastRow + i + 1, columnId].Value = newData[i][0]; // ID
                        worksheet.Cells[lastRow + i + 1, columnName].Value = newData[i][1]; // Name
                        worksheet.Cells[lastRow + i + 1, columnAge].Value = newData[i][2]; // Age
                    }

                    // Save the updated Excel file
                    package.Save();
                }
            }
            else
            {
                // If the file doesn't exist, create a new one and add data
                CreateNewExcelFileWithSpecificColumns(filePath, newData);
            }
        }

        public void CreateNewExcelFileWithSpecificColumns(string filePath, List<string[]> data)
        {
            // Create a new Excel file
            ExcelPackage.LicenseContext = LicenseContext.Commercial; // or LicenseContext.NonCommercial

            using (var package = new ExcelPackage())
            {
                // Add a worksheet to the package
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Specify the columns for headers (e.g., Column A for ID, Column B for Name, Column C for Age)
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Age";

                // Loop through the data and write it to the worksheet
                for (int row = 0; row < data.Count; row++)
                {
                    worksheet.Cells[row + 2, 1].Value = data[row][0]; // ID in Column A
                    worksheet.Cells[row + 2, 2].Value = data[row][1]; // Name in Column B
                    worksheet.Cells[row + 2, 3].Value = data[row][2]; // Age in Column C
                }

                // Save the new Excel file
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
