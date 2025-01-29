using System;
using System.Windows.Forms;
using OfficeOpenXml;

namespace Vil_Management
{
    public partial class UpdateControl : UserControl
    {
        //string filePathDashboard = "Dashboard.csv";
        string filePathContest = "Contest.csv";
        DataGridView dataGrid;


        public UpdateControl()
        {
            InitializeComponent();
        }

        private void btnUpdateSale_Click(object sender, EventArgs e)
        {
            string data = tbUpdateSale.Text;
            dataGridView1.Rows[0].Cells[0].Value = data;

            saveData(data, 1);


        }

        private void btnUpdateTarget_Click(object sender, EventArgs e)
        {
            string data = tbUpdateTarget.Text;
            dataGridView1.Rows[0].Cells[1].Value = data;

            saveData(data, 2);
        }

        private void btnUpdateTnps_Click(object sender, EventArgs e)
        {
            string data = tbUpdateTnps.Text;
            dataGridView1.Rows[0].Cells[2].Value = data;
            saveData(data, 3);
        }

        private void btnUpdateVsearch_Click(object sender, EventArgs e)
        {
            string data = tbUpdateVsearch.Text;
            dataGridView1.Rows[0].Cells[3].Value = data;
            saveData(data, 4);
        }

        private void btnUpdateRetention_Click(object sender, EventArgs e)
        {
            string data = tbUpdateRetention.Text;
            dataGridView1.Rows[0].Cells[4].Value = data;
            saveData(data, 5);
        }
        private void btnUpdateRetainedNo_Click(object sender, EventArgs e)
        {
            string data = tbUpdateRetentionNo.Text;
            dataGridView1.Rows[0].Cells[5].Value = data;
            saveData(data, 6);
        }

        private void btnUpdateEq_Click(object sender, EventArgs e)
        {
            string data = tbUpdateEq.Text;
            dataGridView1.Rows[0].Cells[6].Value = data;
            saveData(data, 7);
        }


        private void btnUpdateDq_Click(object sender, EventArgs e)
        {
            string data = tbUpdateDq.Text;
            dataGridView1.Rows[0].Cells[7].Value = data;
            saveData(data, 8);
        }


        private void saveData(string testData, int columnNo)
        {
            string filePath = "Dashboard.xlsx";
            FileInfo fileInfo = new FileInfo(filePath);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet workSheet;

                // Check if the file exists
                if (fileInfo.Exists)
                {
                    // Open the existing file
                    workSheet = package.Workbook.Worksheets[0] ?? package.Workbook.Worksheets.Add("Sheet1");
                    Console.WriteLine("File opened successfully.");
                }
                else
                {
                    // Create a new worksheet
                    workSheet = package.Workbook.Worksheets.Add("Sheet1");
                    workSheet.Cells[1, 1].Value = "Sale";
                    workSheet.Cells[1, 2].Value = "Target";
                    workSheet.Cells[1, 3].Value = "TNPS";
                    workSheet.Cells[1, 4].Value = "V - Search";
                    workSheet.Cells[1, 5].Value = "Retention";
                    workSheet.Cells[1, 6].Value = "Retained Number";
                    workSheet.Cells[1, 7].Value = "EQ";
                    workSheet.Cells[1, 8].Value = "DQ";
                    Console.WriteLine("New file created.");
                }

                //workSheet.Cells[i + 1, columnNumber].Value = data[i];
                workSheet.Cells[2, columnNo].Value = testData;

                package.Save();

                Console.WriteLine($"Excel file saved at: {filePath}");
            }
        }

        private void saveDataNo(string testData, int columnNo)
        {
            string filePath = "Dashboard.xlsx";
            FileInfo fileInfo = new FileInfo(filePath);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet workSheet;

                // Check if the file exists
                if (fileInfo.Exists)
                {
                    // Open the existing file
                    workSheet = package.Workbook.Worksheets[0] ?? package.Workbook.Worksheets.Add("Sheet1");
                    Console.WriteLine("File opened successfully.");
                }
                else
                {
                    // Create a new worksheet
                    workSheet = package.Workbook.Worksheets.Add("Sheet1");
                    Console.WriteLine("New file created.");
                    MessageBox.Show("File is not available");
                }

                //workSheet.Cells[i + 1, columnNumber].Value = data[i];
                //workSheet.Cells[2, columnNo].Value = testData;
                // Find the next empty row in the specified column
                int row = workSheet.Dimension?.Rows + 1 ?? 1; // If the worksheet is empty, start from row 1

                // Loop through the data and append it to the specified column
                //foreach (var value in testData)
                //{
                //    // Append data to the next available row in the specified column
                //    workSheet.Cells[row++, columnNo].Value = value;
                //}

                workSheet.Cells[row++, columnNo].Value = testData;
                package.Save();

                Console.WriteLine($"Excel file saved at: {filePath}");
            }
        }

        private void btnUpdateConest_Click(object sender, EventArgs e)
        {
            // Get the input data from TextBox and DateTimePicker
            string contest = tbUpdateContest.Text;
            string date = dtpContest.Text;

            if (!string.IsNullOrEmpty(contest) && !string.IsNullOrEmpty(date))
            {
                AddDataToCsv(date, contest); // Add data to CSV
                //LoadCsvToGridview(); // Load data to DataGridView
            }
            else
            {
                MessageBox.Show("Please enter valid data.");
            }
        }

        private void AddDataToCsv(string date, string contest)
        {
            // Check if the file exists, if not, create it and add headers
            if (!File.Exists(filePathContest))
            {
                using (StreamWriter sw = new StreamWriter(filePathContest))
                {
                    sw.WriteLine("Date,Contest"); // Write headers if the file is new
                }
            }

            // Append new data to the CSV file
            using (StreamWriter sw = new StreamWriter(filePathContest, true))
            {

                sw.WriteLine($"{date},{contest}");
            }

            MessageBox.Show("Data added successfully!");
        }

       
    }
}
