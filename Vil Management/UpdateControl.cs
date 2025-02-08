using OfficeOpenXml;

namespace Vil_Management
{
    public partial class UpdateControl : UserControl
    {
        //string filePathDashboard = "Dashboard.csv";
        string filePathContest = "database\\Contest.csv";
        string filePath = "database\\Dashboard.xlsx";
        private String sale = "0";


        public UpdateControl()
        {
            InitializeComponent();
            LoadDashboardData();
            
        }

        private void btnUpdateSale_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
            string data = tbUpdateSale.Text;
            int todaySale = int.Parse(sale); // Cast Parent to ControlDasboard
            todaySale = int.Parse(data) + todaySale;
            dataGridView1.Rows[0].Cells[0].Value = todaySale;
            saveData(todaySale.ToString(), 1);
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
            saveDataNo(data, 6);
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
            //string filePath = "Dashboard.xlsx";
            FileInfo fileInfo = new FileInfo(filePath);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet workSheet;

                // Check if the file exists
                if (fileInfo.Exists)
                {
                    // Open the existing file
                    workSheet = package.Workbook.Worksheets[0] ?? package.Workbook.Worksheets.Add("Sheet1");
                    //Console.WriteLine("File opened successfully.");
                }
                else
                {
                    // Create a new worksheet
                    workSheet = package.Workbook.Worksheets.Add("Sheet1");
                    //Console.WriteLine("New file created.");
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

                //Console.WriteLine($"Excel file saved at: {filePath}");
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
                tbUpdateContest.Clear();
            }
            else
            {
                MessageBox.Show("Please enter valid data.");
            }


        }

        private void AddDataToCsv(string data, string contest)
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

                sw.WriteLine($"\"{data}\",\"{contest}\"");
            }

            MessageBox.Show("Data added successfully!");
        }

        private void BindDataToGridView(string[] columnData)
        {
            // Clear any previous data in the DataGridView
            dataGridView1.Rows.Clear();
            //dataGridView1.Columns.Clear();

            // Add a single column to DataGridView
            //dataGridView1.Columns.Add("ColumnA", "Column A");

            // Add rows based on the data in the specific column
            foreach (var value in columnData)
            {
                dataGridView1.Rows.Add(value);
            }
        }
        private string[] ReadExcelColumn(string filePath, int columnIndex)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];  // Assuming data is in the first worksheet

                int rowCount = worksheet.Dimension.Rows;
                var columnData = new string[rowCount];

                // Get the index of the column based on the letter (e.g., "A" -> 1, "B" -> 2, etc.)
                //int columnIndex = GetColumnIndex(columnLetter);

                // Read data from the specific column
                for (int row = 1; row <= rowCount; row++)  // EPPlus rows start at 1
                {
                    columnData[row - 1] = worksheet.Cells[row, columnIndex].Text;  // Get text value from the cell
                }

                return columnData;
            }
        }

        private void LoadDashboardData()
        {
            List<string> modifiedItems = new List<string>();
            // Ensure the file exists
            if (!File.Exists(filePath))
            {
                //MessageBox.Show("Excel file not found.");
                return;
            }

            FileInfo fileInfo = new FileInfo(filePath);


            try
            {
                using (var package = new ExcelPackage(fileInfo))
                {
                    // Ensure the workbook contains worksheets
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        MessageBox.Show("No worksheets found in the Excel file.");
                        return;
                    }

                    // Access the first worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    // Ensure the worksheet is not null
                    if (worksheet == null)
                    {
                        MessageBox.Show("The worksheet is null or invalid.");
                        return;
                    }

                    // Read the value from cell A1 (you can adjust this as needed)
                    sale = worksheet.Cells[2, 1].Text ?? "Default Value";   // Row 1, Column 1 (A1)
                    string target = worksheet.Cells[2, 2].Text ?? "Default Value";
                    string tnps = worksheet.Cells[2, 3].Text ?? "Default Value";
                    string vSearch = worksheet.Cells[2, 4].Text ?? "Default Value";
                    string retention = worksheet.Cells[2, 5].Text ?? "Default Value";
                    //string retention = worksheet.Cells[2, 6].Text ?? "Default Value";
                    string eq = worksheet.Cells[2, 7].Text ?? "Default Value";
                    string dq = worksheet.Cells[2, 8].Text ?? "Default Value";
                    //string sale = worksheet.Cells[1, 1].Text;

                    dataGridView1.Rows[0].Cells[0].Value = sale;
                    dataGridView1.Rows[0].Cells[1].Value = target;
                    dataGridView1.Rows[0].Cells[2].Value = tnps;
                    dataGridView1.Rows[0].Cells[3].Value = vSearch;
                    dataGridView1.Rows[0].Cells[4].Value = retention;
                    dataGridView1.Rows[0].Cells[6].Value = eq;
                    dataGridView1.Rows[0].Cells[7].Value = dq;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Excel file: " + ex.Message);
            }
        }

        
    }
}
