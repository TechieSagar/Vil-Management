using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Kafka;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;



namespace Vil_Management
{
    public partial class NumberSystemControl1 : UserControl
    {

        private string numberSeriesPath = "numbersDatabase\\numberSeries.csv";
        private string nSeriesPath = "numbersDatabase\\nSeries.csv";
        private string n2DigitPath = "numbersDatabase\\nMid2digit.csv";

        int[] numberSeriesSingle = { };
        //private TextBox inputDatxtInputData;
        private int digitCount = 0;
        //ErrorProvider errorProvider = null;
        private int RowLimit = 30000;
        public NumberSystemControl1()
        {
            InitializeComponent();
            LoadData(cbNmbrlgySrs, nSeriesPath);
            LoadData(cbSrsNmbrs, numberSeriesPath);
            LoadData(cbMid2Digit, n2DigitPath);
        }

        private void btnInsertSeries_Click(object sender, EventArgs e)
        {
            // Get the input data from TextBox
            string inputText = tbInserSrs.Text;

            // Validate if the input is a valid numeric value
            if (string.IsNullOrEmpty(inputText) || !double.TryParse(inputText, out double inputNumber))
            {
                MessageBox.Show("Please enter a valid numeric value.");
                return;
            }

            // save data
            SaveData(tbInserSrs, numberSeriesPath);

            // load data after save
            LoadData(cbSrsNmbrs, numberSeriesPath);
            if (cbSrsNmbrs.Items.Count > 0)
            {
                // Set the selected item to the last item
                cbSrsNmbrs.SelectedIndex = cbSrsNmbrs.Items.Count - 1;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(cbSrsNmbrs, numberSeriesPath);
        }

        // All Methods listed here

        private void SaveData(System.Windows.Forms.TextBox tb, string csv)
        {
            // Get the text from the TextBox
            string text = tb.Text.Trim();

            // Check if the TextBox is not empty
            if (!string.IsNullOrEmpty(text))
            {
                // Check if the CSV file exists and load the existing data to check for duplicates
                List<string> existingData = new List<string>();

                if (File.Exists(csv))
                {
                    // Read all lines from the CSV file
                    existingData = File.ReadAllLines(csv).ToList();
                }

                // Check if the text already exists in the CSV file (to avoid duplicates)
                if (existingData.Contains(text))
                {
                    MessageBox.Show(text + " already exists in the list.");
                    return;
                }

                // Append the new data to the CSV file
                using (StreamWriter writer = new StreamWriter(csv, append: true))
                {
                    // If the file doesn't exist, add a header (optional)
                    //if (!existingData.Any())
                    //{
                    //    writer.WriteLine("Data");  // Optional header
                    //}

                    // Append the new data (text from TextBox) to the CSV file
                    writer.WriteLine(text);
                }

                //MessageBox.Show("Data saved to CSV file.");
                tb.Clear();  // Clear the TextBox after saving

            }
            else
            {
                MessageBox.Show("Please enter some text to save.");
            }
        }

        private void LoadData(System.Windows.Forms.ComboBox cb, string csv)
        {
            // Check if the CSV file exists
            if (File.Exists(csv))
            {
                // Clear existing items in ComboBox before loading new data
                cb.Items.Clear();

                // Read all lines from the CSV file
                var lines = File.ReadAllLines(csv);

                // Start loading data from the second line (skip the header if present)
                for (int i = 0; i < lines.Length; i++)  // Skips header if there is one
                {
                    cb.Items.Add(lines[i]);
                }

                if (cb.Items.Count > 0)
                    cb.SelectedIndex = 0;
                //MessageBox.Show("Data loaded into ComboBox.");
            }
            else
            {
                MessageBox.Show("CSV file not found.");
            }
        }

        private void btnInsertNrlgy_Click(object sender, EventArgs e)
        {
            SaveData(tbNumerology, nSeriesPath);
            LoadData(cbNmbrlgySrs, nSeriesPath);

            if (cbNmbrlgySrs.Items.Count > 0)
            {
                // Set the selected item to the last item
                cbNmbrlgySrs.SelectedIndex = cbNmbrlgySrs.Items.Count - 1;
            }
        }

        private void tbInserSrs_TextChanged(object sender, EventArgs e)
        {
            // Check if the text length exceeds 4 digits
            if (tbInserSrs.Text.Length > 4)
            {
                // Show error message if the length exceeds 4 digits
                MessageBox.Show("You can only enter up to 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Truncate the text to 4 digits
                tbInserSrs.Text = tbInserSrs.Text.Substring(0, 4);
                tbInserSrs.SelectionStart = tbInserSrs.Text.Length; // Set cursor to the end of the text
            }

        }

        private void tbNumerology_TextChanged(object sender, EventArgs e)
        {
            if (tbNumerology.Text.Length > 4)
            {
                // Show error message if the length exceeds 4 digits
                MessageBox.Show("You can only enter up to 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Truncate the text to 4 digits
                tbNumerology.Text = tbNumerology.Text.Substring(0, 4);
                tbNumerology.SelectionStart = tbNumerology.Text.Length; // Set cursor to the end of the text
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearComboBox(nSeriesPath, cbNmbrlgySrs);
        }

        private void ClearComboBox(String csvPath, System.Windows.Forms.ComboBox cb)
        {
            // Get the entry to delete from the TextBox
            string entryToDelete = cb.Text.Trim();

            // Check if the entry to delete is not empty
            if (!string.IsNullOrEmpty(entryToDelete))
            {
                // Check if the CSV file exists
                if (File.Exists(csvPath))
                {
                    // Read all lines from the CSV file
                    List<string> lines = File.ReadAllLines(csvPath).ToList();

                    // Remove the entry from the list (skip the header)
                    bool entryFound = lines.Remove(entryToDelete);

                    // If the entry was found and removed, save the updated file
                    if (entryFound)
                    {
                        // Re-write the updated list back to the CSV file
                        using (StreamWriter writer = new StreamWriter(csvPath, false))
                        {
                            // Optionally, write the header again
                            //writer.WriteLine("Data");  // Write the header if needed

                            // Write the remaining lines back to the file
                            foreach (var line in lines)
                            {
                                writer.WriteLine(line);
                            }
                        }

                        //MessageBox.Show("Entry deleted successfully.");
                        cb.SelectedIndex = -1; // Clear the TextBox after deletion
                        LoadData(cb, csvPath);
                    }
                    else
                    {
                        MessageBox.Show("Entry not found in the file.");
                    }
                }
                else
                {
                    MessageBox.Show("Data file not found.");
                }
            }
            else
            {
                MessageBox.Show("Please enter the entry to delete.");
            }

        }

        private void btnRefrshNmrlgy_Click(object sender, EventArgs e)
        {
            LoadData(cbNmbrlgySrs, nSeriesPath);
        }

        private void tbInsertMid2digit_TextChanged(object sender, EventArgs e)
        {
            if (tbInsertMid2digit.Text.Length > 2)
            {
                // Show error message if the length exceeds 4 digits
                MessageBox.Show("You can only enter up to 2 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Truncate the text to 4 digits
                tbInsertMid2digit.Text = tbInsertMid2digit.Text.Substring(0, 2);
                tbInsertMid2digit.SelectionStart = tbInsertMid2digit.Text.Length; // Set cursor to the end of the text
            }
        }

        private void btnInsertMid2Digit_Click(object sender, EventArgs e)
        {

            SaveData(tbInsertMid2digit, n2DigitPath);
            LoadData(cbMid2Digit, n2DigitPath);

            if (cbMid2Digit.Items.Count > 0)
            {
                // Set the selected item to the last item
                cbMid2Digit.SelectedIndex = cbMid2Digit.Items.Count - 1;
            }
        }

        private void btnClrMid2Digit_Click(object sender, EventArgs e)
        {
            ClearComboBox(n2DigitPath, cbMid2Digit);
        }

        private void btnRfrshMid2Digit_Click(object sender, EventArgs e)
        {
            LoadData(cbMid2Digit, n2DigitPath);
        }

        private void tbUpto6Digit_TextChanged(object sender, EventArgs e)
        {
            if (radioButton6d.Checked)
            {
                digitCount = 6;
            }
            else if (radioButton5d.Checked)
            {
                digitCount = 5;
            }
            else if (radioButton4d.Checked)
            {
                digitCount = 4;
            }
            else if (radioButton3d.Checked)
            {
                digitCount = 3;
            }
            else if (radioButton2d.Checked)
            {
                digitCount = 2;
            }

            if (tbUpto6Digit.Text.Length > digitCount)
            {
                // Show error message if the length exceeds digits
                MessageBox.Show("You can only enter up to " + digitCount + "digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Truncate the text to 4 digits
                tbUpto6Digit.Text = tbUpto6Digit.Text.Substring(0, digitCount);
                tbUpto6Digit.SelectionStart = tbUpto6Digit.Text.Length; // Set cursor to the end of the text
            }

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<string> modifiedItems = new List<string>();

            if (tbUpto6Digit.Text.Length == 6)
            {
                foreach (var item in cbSrsNmbrs.Items)
                {
                    string modifiedItem = item.ToString() + tbUpto6Digit.Text;
                    modifiedItems.Add(modifiedItem);
                }
            }
            else if (tbUpto6Digit.Text.Length == 5)
            {
                foreach (var item in cbSrsNmbrs.Items)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        string modifiedItem = item.ToString() + i + tbUpto6Digit.Text;
                        modifiedItems.Add(modifiedItem);
                    }
                }
            }
            else if (tbUpto6Digit.Text.Length == 4)
            {
                foreach (var item in cbSrsNmbrs.Items)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string modifiedItem = item.ToString() + i.ToString("D2") + tbUpto6Digit.Text;
                        modifiedItems.Add(modifiedItem);
                    }
                }
            }
            else if (tbUpto6Digit.Text.Length == 3)
            {
                foreach (var item in cbSrsNmbrs.Items)
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        string modifiedItem = item.ToString() + i.ToString("D3") + tbUpto6Digit.Text;
                        modifiedItems.Add(modifiedItem);
                    }
                }
            }
            else if (tbUpto6Digit.Text.Length == 2)
            {
                foreach (string item in cbSrsNmbrs.Items)
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        string modifiedItem = item.ToString() + i.ToString("D4") + tbUpto6Digit.Text;
                        modifiedItems.Add(modifiedItem);
                    }
                }
            }
            else if (tbUpto6Digit.Text.Length == 0)
            {
                MessageBox.Show("Please enter the required digits");
            }


            foreach (var item in modifiedItems)
            {
                dataGridView1.Rows.Add(item);
            }

            if (dataGridView1 != null)
            {
                btnCopy.Visible = true;
            }
            else
            {
                btnCopy.Visible = false;
            }

            tbUpto6Digit.Clear();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyColumnData(dataGridView1, "Column1");
        }

        // Copy column data Funtion
        private void CopyColumnData(DataGridView gridView, string columnName)
        {
            // Specify the column name to copy (e.g., "Name" column)
            //string columnName = "Column1";  // You can change this to any column name you want to copy
            StringBuilder columnData = new StringBuilder();

            // Check if the column exists in the DataGridView
            if (gridView.Columns.Contains(columnName))
            {
                // Iterate through the rows and get data from the specified column
                foreach (DataGridViewRow row in gridView.Rows)
                {
                    // Ensure that the row is not a new empty row
                    if (!row.IsNewRow)
                    {
                        // Get the value from the specified column
                        var cellValue = row.Cells[columnName].Value;
                        if (cellValue != null)
                        {
                            // Append the value to the StringBuilder
                            columnData.AppendLine(cellValue.ToString());
                        }
                    }
                }

                // If we have collected data, copy it to the clipboard
                if (columnData.Length > 0)
                {
                    Clipboard.SetText(columnData.ToString());
                    MessageBox.Show("Column data copied to clipboard!");
                }
                else
                {
                    MessageBox.Show("No data to copy.");
                }
            }
            else
            {
                MessageBox.Show($"Column '{columnName}' does not exist.");
            }
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUpto6Digit.Text))
            {
                RowLimit = 30000;
            }
            else
            {
                RowLimit = int.Parse(tbUpto6Digit.Text);
            }

            // Start from the first column (index 0)
            int currentColumn = 0;

            // Loop through the rows to ensure data does not exceed RowLimit per column
            while (currentColumn < dataGridView1.Columns.Count)
            {
                // Iterate through each row in the DataGridView
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!dataGridView1.Rows[i].IsNewRow)
                    {
                        // If row index exceeds the RowLimit, move data to the next column
                        if (i >= RowLimit)
                        {
                            // Move data to the next column (currentColumn + 1)
                            if (currentColumn + 1 < dataGridView1.Columns.Count)
                            {
                                // Move the data from the current column to the next column
                                dataGridView1.Rows[i - RowLimit].Cells[currentColumn + 1].Value = dataGridView1.Rows[i].Cells[currentColumn].Value;
                                // Clear the current column after moving data
                                dataGridView1.Rows[i].Cells[currentColumn].Value = null;
                            }
                        }
                    }
                }

                // After moving data, update the threshold for the next set of rows
                if (currentColumn + 1 < dataGridView1.Columns.Count)
                {
                    // Move to the next column
                    currentColumn++;
                }
                else
                {
                    // If no more columns are available, show a message and break out of the loop
                    MessageBox.Show("No more columns available to move data.");
                    break;
                }
            }

            // Show a message once the data has been processed
            MessageBox.Show("Data has been successfully distributed across columns.");
        }

        private async void btnGnrtNmrlgy_Click(object sender, EventArgs e)
        {
            btnGnrtNmrlgy.Enabled = false;

            // Pre-allocate capacity for generating items
            List<string> modifiedItems = new List<string>(1000000); // Pre-allocate large capacity

            // Start the number generation process in a background thread without waiting (immediate start)
            Task task = Task.Run(() =>
            {
                // Use Parallel.For to parallelize the work across multiple CPU cores
                Parallel.ForEach(cbNmbrlgySrs.Items.Cast<object>(), item =>
                {
                    foreach (var item2 in cbMid2Digit.Items)
                    {
                        for (int i = 0; i < 10000; i++)
                        {
                            string modifiedItem = item.ToString() + item2 + i.ToString("D4");

                            // Avoid lock and add to list directly (fast)
                            modifiedItems.Add(modifiedItem);
                        }
                    }
                });

                // After generating the items, update the DataGridView in bulk
                dataGridView1.Invoke((MethodInvoker)delegate
                {
                    dataGridView1.Rows.Clear();  // Clear old rows
                });

                // Add all items in a single bulk operation (faster than adding one by one)
                dataGridView2.Invoke((MethodInvoker)delegate
                {
                    foreach (var item in modifiedItems)
                    {
                        dataGridView2.Rows.Add(item);
                    }
                });

                // Enable the button and show the completion message (in the UI thread)
                btnGnrtNmrlgy.Invoke((MethodInvoker)delegate
                {
                    btnGnrtNmrlgy.Enabled = true;
                    MessageBox.Show("Generated");
                });
            });

            // This button becomes visible without waiting for task completion
            btnCopy1.Visible = true;
            dataGridView2.Visible = true;
            btnClrList2.Visible = true;
        }

        private void btnFilterDigit_Click(object sender, EventArgs e)
        {
            //foreach (string item in columnDataArray)
            //{
            //    // Add a new row with one cell (one column per array element)
            //    dataGridView2.Rows.Add(item);
            //}
            CopyDataToDataGridView(dataGridView2, dataGridView3);


            // Get the selected digits based on the checkboxes
            string selectedDigits = GetSelectedDigits();

            // Loop through the DataGridView rows in reverse order to avoid index shifting when deleting rows
            for (int i = dataGridView3.Rows.Count - 1; i >= 0; i--)
            {
                var row = dataGridView3.Rows[i];

                // Check if the row has a valid cell in the first column (column index 0)
                if (row.Cells[0].Value != null)
                {
                    string rowData = row.Cells[0].Value.ToString();

                    // Check if row contains any unwanted digits
                    if (ContainsUnwantedDigits(rowData, selectedDigits))
                    {
                        // Remove the row if it contains any unwanted digits
                        dataGridView3.Rows.RemoveAt(i);
                    }
                }
                else
                {
                    // Optional: Handle cases where the cell is null (empty or no data)
                    // If you want to remove rows with no data, you can use this:
                    // dataGridView1.Rows.RemoveAt(i);
                }
            }

            if (dataGridView3.Visible == false)
            {
                dataGridView3.Visible = true;
                btnCopy2.Visible = true;
                btnClrList3.Visible = true;
            }


            MessageBox.Show("Required digits are filtered.");
        }

        // Get the selected digits as a string
        private string GetSelectedDigits()
        {
            string selectedDigits = string.Empty;

            // Check which checkboxes are checked and add the corresponding digits to the selectedDigits string
            if (checkBox0.Checked) selectedDigits += "0";
            if (checkBox1.Checked) selectedDigits += "1";
            if (checkBox2.Checked) selectedDigits += "2";
            if (checkBox3.Checked) selectedDigits += "3";
            if (checkBox4.Checked) selectedDigits += "4";
            if (checkBox5.Checked) selectedDigits += "5";
            if (checkBox6.Checked) selectedDigits += "6";
            if (checkBox7.Checked) selectedDigits += "7";
            if (checkBox8.Checked) selectedDigits += "8";
            if (checkBox9.Checked) selectedDigits += "9";

            return selectedDigits;
        }

        // Check if the row data contains any unwanted digits
        private bool ContainsUnwantedDigits(string rowData, string selectedDigits)
        {
            // Loop through each character in the row data and check if it is not in the selected digits
            foreach (char c in rowData)
            {
                if (!selectedDigits.Contains(c.ToString())) // If the digit is not selected, return true
                {
                    return true; // The row contains unwanted digit, so return true
                }
            }

            // If all digits are in the selected digits, return false
            return false;
        }

        private void btnFilterSum_Click(object sender, EventArgs e)
        {
            CopyDataToDataGridView(dataGridView3, dataGridView4);

            List<int> requiredSums = new List<int>();  // List to store selected sums

            // Add the corresponding sum values to the list based on checked checkboxes
            if (checkBox10.Checked) requiredSums.Add(0);
            if (checkBox11.Checked) requiredSums.Add(1);
            if (checkBox11.Checked) requiredSums.Add(10);
            if (checkBox12.Checked) requiredSums.Add(2);
            if (checkBox13.Checked) requiredSums.Add(3);
            if (checkBox14.Checked) requiredSums.Add(4);
            if (checkBox15.Checked) requiredSums.Add(5);
            if (checkBox16.Checked) requiredSums.Add(6);
            if (checkBox17.Checked) requiredSums.Add(7);
            if (checkBox18.Checked) requiredSums.Add(8);
            if (checkBox19.Checked) requiredSums.Add(9);

            // Loop through all the rows in DataGridView in reverse order to avoid index shifting when removing rows
            for (int i = dataGridView4.Rows.Count - 1; i >= 0; i--)
            {
                var row = dataGridView4.Rows[i];

                if (row.Cells[0].Value != null)
                {
                    string rowData = row.Cells[0].Value.ToString();
   
                    int sum = CalculateDigitSum(rowData);

                    // Check if the sum is a two-digit number (between 10 and 99)
                    if (sum >=10 && sum <= 99)
                    {
                        // Recalculate sum of digits if the sum is a two-digit number
                        sum = CalculateDigitSum(sum.ToString());
                    }

                    // If the sum is not in the list of required sums, remove the row
                    if (!requiredSums.Contains(sum))
                    {
                        dataGridView4.Rows.RemoveAt(i);
                    }
                }
            }

            if (dataGridView4.Visible == false)
            {
                dataGridView4.Visible = true;
                btnCopy3.Visible = true;
                btnClrList4.Visible = true;
            }

            MessageBox.Show("Filtered by sum.");
        }

        // Helper method to calculate the sum of digits of a number (string version)
        private int CalculateDigitSum(string value)
        {
            return value.Where(c => Char.IsDigit(c)) // Filter only digits
                        .Select(c => int.Parse(c.ToString())) // Convert characters to integers
                        .Sum(); // Sum the digits
        }

        // Alternatively, helper method to calculate the sum of digits of an integer
        private int CalculateDigitSum(int value)
        {
            return value.ToString().Where(c => Char.IsDigit(c)) // Filter only digits
                                    .Select(c => int.Parse(c.ToString())) // Convert characters to integers
                                    .Sum(); // Sum the digits
        }

        // Get the selected digits as a string
        private int GetRequiredSum()
        {
            int requiredSum = 0;

            // Check which checkboxes are checked and add the corresponding digits to the selectedDigits string
            if (checkBox10.Checked) requiredSum = 0;
            if (checkBox11.Checked) requiredSum = 1;
            if (checkBox12.Checked) requiredSum = 2;
            if (checkBox13.Checked) requiredSum = 3;
            if (checkBox14.Checked) requiredSum = 4;
            if (checkBox15.Checked) requiredSum = 5;
            if (checkBox16.Checked) requiredSum = 6;
            if (checkBox17.Checked) requiredSum = 7;
            if (checkBox18.Checked) requiredSum = 8;
            if (checkBox19.Checked) requiredSum = 9;

            return requiredSum;
        }

        private void btnFltrRptvDgts_Click(object sender, EventArgs e)
        {
            CopyDataToDataGridView(dataGridView4, dataGridView5);

            // Loop through all the rows in DataGridView in reverse order
            for (int i = dataGridView5.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dataGridView5.Rows[i];

                // Ensure the row has data in the first column (column 0)
                if (row.Cells[0].Value != null)
                {
                    string rowData = row.Cells[0].Value.ToString();

                    // Check if the row contains any repetitive digits based on the selected checkboxes
                    if (ContainsRepetitiveDigits(rowData))
                    {
                        // Remove the row if it contains repetitive digits that are not required
                        dataGridView5.Rows.RemoveAt(i);
                    }
                }
            }

            if (dataGridView5.Visible == false)
            {
                dataGridView5.Visible = true;
                btnCopy4.Visible = true;
                btnClrList5.Visible = true;
            }


            MessageBox.Show("Repetitive Filter applied.");

        }

        // Helper method to check if the string contains repetitive digits that are not selected
        private bool ContainsRepetitiveDigits(string rowData)
        {
            // Get the list of required digits based on selected checkboxes
            var selectedDigits = GetSelectedDigitsNew();

            // Group digits in rowData and check if any digit is repeated
            var groupedDigits = rowData.GroupBy(c => c).Where(g => g.Count() > 1); // Group same digits

            foreach (var group in groupedDigits)
            {
                int digit = int.Parse(group.Key.ToString());

                // If the repeated digit is not selected, return true (indicating that the row should be removed)
                if (!selectedDigits.Contains(digit))
                {
                    return true;
                }
            }

            // If no unwanted repeated digits are found, return false
            return false;
        }

        // Helper method to get the selected digits from checkboxes
        private HashSet<int> GetSelectedDigitsNew()
        {
            HashSet<int> selectedDigits = new HashSet<int>();

            // Check each checkbox to see if it is checked
            if (checkBox20.Checked) selectedDigits.Add(0);
            if (checkBox21.Checked) selectedDigits.Add(1);
            if (checkBox22.Checked) selectedDigits.Add(2);
            if (checkBox23.Checked) selectedDigits.Add(3);
            if (checkBox24.Checked) selectedDigits.Add(4);
            if (checkBox25.Checked) selectedDigits.Add(5);
            if (checkBox26.Checked) selectedDigits.Add(6);
            if (checkBox27.Checked) selectedDigits.Add(7);
            if (checkBox28.Checked) selectedDigits.Add(8);
            if (checkBox29.Checked) selectedDigits.Add(9);


            return selectedDigits;
        }

        private void CopyDataToDataGridView(DataGridView source, DataGridView destination)
        {
            // Clear destination DataGridView first
            destination.Rows.Clear();

            // Create a list to hold rows that will be added to the destination
            var rows = new List<DataGridViewRow>();

            // Run the parallel task for copying rows from source to rows list
            Parallel.ForEach(source.Rows.Cast<DataGridViewRow>(), (sourceRow) =>
            {
                if (!sourceRow.IsNewRow) // Skip the new row (if any)
                {
                    // Create a new row for the destination DataGridView
                    var destinationRow = (DataGridViewRow)sourceRow.Clone();

                    // Copy each cell value from the source row to the destination row
                    for (int i = 0; i < sourceRow.Cells.Count; i++)
                    {
                        destinationRow.Cells[i].Value = sourceRow.Cells[i].Value;
                    }

                    //destinationRow.Cells[0].Value = sourceRow.Cells[0].Value;

                    // Add the new row to the list (we add rows later on the UI thread)
                    rows.Add(destinationRow);
                }
            });

            // After parallel task is done, add rows to destination DataGridView on the UI thread
            this.Invoke((MethodInvoker)(() =>
            {
                destination.Rows.Clear(); // Clear the destination DataGridView before adding new rows
                destination.Rows.AddRange(rows.ToArray());
            }));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string inputText = tbPair.Text;

            // Validate if the input is a valid numeric value
            if (string.IsNullOrEmpty(inputText) || !double.TryParse(inputText, out double inputNumber))
            {
                MessageBox.Show("Please enter a valid numeric value.");
                return;
            }

            bool itemExists = false;

            foreach (var existingItem in cbPair.Items)
            {
                if (existingItem.ToString() == inputText)
                {
                    itemExists = true;
                    break; // Stop the loop once the item is found
                }
            }

            if (!itemExists)
            {
                cbPair.Items.Add(inputText);
                if (cbPair.Items.Count > 0)
                {
                    // Set the selected item to the last item
                    cbPair.SelectedIndex = cbPair.Items.Count - 1;
                }
            }
            else
            {
                MessageBox.Show("Item already exists in the ComboBox.");
            }

        }

        private void btnFilterPair_Click(object sender, EventArgs e)
        {
            CopyDataToDataGridView(dataGridView5, dataGridView6);

            // Loop through each row in the DataGridView in reverse order (to avoid skipping rows when deleting)
            for (int i = dataGridView6.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dataGridView6.Rows[i];

                // Check if the row is not a new row
                if (!row.IsNewRow)
                {
                    // Loop through each cell in the row
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        foreach (string item in cbPair.Items)
                        {
                            // Check if the cell's value matches the search value
                            if (cell.Value != null && cell.Value.ToString().Contains(item))
                            {
                                // Remove the row if the value is found
                                dataGridView6.Rows.RemoveAt(i);
                                break; // Stop checking other cells in this row after deletion
                            }
                        }
                    }
                }

                if (dataGridView6.Visible == false)
                {
                    dataGridView6.Visible = true;
                    btnCopy5.Visible = true;
                    btnClrList6.Visible = true;
                }
            }
        }

        private void btnCopy1_Click(object sender, EventArgs e)
        {
            CopyColumnData(dataGridView2, "Column15");
        }

        private void btnCopy2_Click(object sender, EventArgs e)
        {
            CopyColumnData(dataGridView3, "Column16");
        }

        private void btnCopy3_Click(object sender, EventArgs e)
        {
            CopyColumnData(dataGridView4, "Column17");
        }

        private void btnCopy4_Click(object sender, EventArgs e)
        {
            CopyColumnData(dataGridView5, "Column18");
        }

        private void btnCopy5_Click(object sender, EventArgs e)
        {
            CopyColumnData(dataGridView6, "Column19");
        }

        private void btnClrPairCb_Click(object sender, EventArgs e)
        {
            cbPair.Items.Clear();
        }

        private void btnCheckSum_Click(object sender, EventArgs e)
        {
            string inputNumber = tbCheckSum.Text;
            // Validate if the input is a valid 10-digit number
            if (inputNumber.Length != 10 || !long.TryParse(inputNumber, out _))
            {
                MessageBox.Show("Please enter a valid 10-digit number.");
                return;
            }

            int sum = 0;
            foreach (char digit in inputNumber)
            {
                sum += int.Parse(digit.ToString());
            }

            tbTotalSum2digit.Text = sum.ToString();
            tbFinalSum.Text = sum.ToString();

            // If the sum is a two-digit number, calculate the sum of its digits again
            if (sum >= 10 && sum <= 99)
            {
                sum = SumOfDigits(sum);
                tbFinalSum.Text = sum.ToString();
            }

            if (sum >= 10 && sum <= 99)
            {
                sum = SumOfDigits(sum);
                tbFinalSum.Text = sum.ToString();
            }

        }

        // Helper function to sum the digits of a number
        private int SumOfDigits(int number)
        {
            int sum = 0;
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum;
        }

        private void btnSingleGenerate_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<string> numbersList = new List<string>();

            if (cbSrsNmbrs.SelectedItem != null)
            {
                string selectedValue = (string)cbSrsNmbrs.SelectedItem;

                if (tbUpto6Digit.Text.Length == 5)
                {
                    for (int i = 0; i < 10; ++i)
                    {
                        string updatedItem = cbSrsNmbrs.SelectedItem.ToString() + i.ToString() + tbUpto6Digit.Text;
                        numbersList.Add(updatedItem);
                    }
                }

                else if (tbUpto6Digit.Text.Length == 4)
                {
                    for (int i = 0; i < 100; ++i)
                    {
                        string updatedItem = cbSrsNmbrs.SelectedItem.ToString() + i.ToString("D2") + tbUpto6Digit.Text;
                        numbersList.Add(updatedItem);
                    }
                }

                else if (tbUpto6Digit.Text.Length == 3)
                {
                    for (int i = 0; i < 1000; ++i)
                    {
                        string updatedItem = cbSrsNmbrs.SelectedItem.ToString() + i.ToString("D3") + tbUpto6Digit.Text;
                        numbersList.Add(updatedItem);
                    }
                }

                else if (tbUpto6Digit.Text.Length == 2)
                {
                    for (int i = 0; i < 10000; ++i)
                    {
                        string updatedItem = cbSrsNmbrs.SelectedItem.ToString() + i.ToString("D4") + tbUpto6Digit.Text;
                        numbersList.Add(updatedItem);
                    }
                }

            }

            foreach (var item in numbersList)
            {
                dataGridView1.Rows.Add(item);
            }

        }


        private void ClearDataGridRows(DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you really want to clear ??", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Data has been cleared.");
                    dataGridView.Rows.Clear();
                }
                else if (result == DialogResult.No)
                {
                    // User clicked No

                }

            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            //CopyDataToDataGridView(dataGridView1, dataGridView2);
            dataGridView2.SuspendLayout();
            dataGridView2.Rows.Clear();
            var rowsCount = dataGridView1.Rows.Count;
            Parallel.For(0, rowsCount, i =>
            {
                if (!dataGridView1.Rows[i].IsNewRow)
                {
                    var cellValue = dataGridView1.Rows[i].Cells[0].Value ?? DBNull.Value;

                    dataGridView2.Invoke(new Action(() =>
                    {
                        dataGridView2.Rows.Add(cellValue);
                    }));
                }
            });
            dataGridView2.ResumeLayout();

            btnCopy1.Visible = true;
            dataGridView2.Visible = true;
            btnClrList2.Visible = true;

        }

        private void tbCheckSum_TextChanged(object sender, EventArgs e)
        {
            if (tbCheckSum.Text.Length > 10)
            {
                // Show error message if the length exceeds 4 digits
                MessageBox.Show("You can only enter up to 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Truncate the text to 4 digits
                tbCheckSum.Text = tbCheckSum.Text.Substring(0, 10);
                tbCheckSum.SelectionStart = tbCheckSum.Text.Length; // Set cursor to the end of the text
            }
        }

        private void btnClrList2_Click(object sender, EventArgs e)
        {
            ClearDataGridRows(dataGridView2);
        }

        private void btnClrList3_Click(object sender, EventArgs e)
        {
            ClearDataGridRows(dataGridView3);
        }

        private void btnClrList4_Click(object sender, EventArgs e)
        {
            ClearDataGridRows(dataGridView4);
        }

        private void btnClrList5_Click(object sender, EventArgs e)
        {
            ClearDataGridRows(dataGridView5);
        }

        private void btnClrList6_Click(object sender, EventArgs e)
        {
            ClearDataGridRows(dataGridView6);
        }

        private void btnDoubleZero_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<string> modifiedItems = new List<string>();

            foreach (var item in cbSrsNmbrs.Items)
            {
                for (int i = 0; i < 100; i++)
                {
                    string number = item.ToString() + "00" + i.ToString("D2") + "00";
                    modifiedItems.Add(number);
                }

            }

            foreach (var item in modifiedItems)
            {
                dataGridView1.Rows.Add(item);
            }

        }

        private void btnGenerateByMid_Click(object sender, EventArgs e)
        {
            String digits = tbUpto6Digit.Text;

            //dataGridView1.Rows.Clear();
            List<string> modifiedItems = new List<string>();

            if (tbUpto6Digit.Text.Length == 5)
            {
                foreach (var item in cbSrsNmbrs.Items)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        string modifiedItem = item.ToString() + digits + i.ToString();
                        modifiedItems.Add(modifiedItem);
                    }
                }
            }
            else if (tbUpto6Digit.Text.Length == 4)
            {
                foreach (var item in cbSrsNmbrs.Items)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string modifiedItem = item.ToString() + digits + i.ToString("D2");
                        modifiedItems.Add(modifiedItem);
                    }
                }
            }
            else if (tbUpto6Digit.Text.Length == 3)
            {
                foreach (var item in cbSrsNmbrs.Items)
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        string modifiedItem = item.ToString() + digits + i.ToString("D3");
                        modifiedItems.Add(modifiedItem);
                    }
                }
            }
            else if (tbUpto6Digit.Text.Length == 0)
            {
                MessageBox.Show("Please enter the required digits");
            }

            dataGridView1.Rows.Clear();
            foreach (var item in modifiedItems)
            {
                dataGridView1.Rows.Add(item);
            }

            MessageBox.Show("Numbers generated successfully.");

            tbUpto6Digit.Clear();
        }

        private void btnClrDatGrid1_Click(object sender, EventArgs e)
        {
            ClearDataGridRows(dataGridView1);
        }
    }
}
