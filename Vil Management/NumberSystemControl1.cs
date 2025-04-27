using System;
using System.Buffers;
using System.Collections;
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
        private int RowLimit = 29999;

        public List<string> modifiedItemsNew = new List<string>(10000000);
        public List<string> filteredData = new List<string>(10000000);
        public List<string> modifiedItems = new List<string>(10000000);
        public List<string> filteredRows = new List<string>(10000000);
        public List<string> FilterRepetitve = new List<string>(10000000);
        public List<string> FilterPair = new List<string>(10000000);

        public NumberSystemControl1()
        {
            InitializeComponent();
            LoadData(cbNmbrlgySrs, nSeriesPath);
            LoadData(cbSrsNmbrs, numberSeriesPath);
            //LoadData(cbMid2Digit, n2DigitPath);
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
            //if (tbInsertMid2digit.Text.Length > 2)
            //{
            //    // Show error message if the length exceeds 4 digits
            //    MessageBox.Show("You can only enter up to 2 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    // Truncate the text to 4 digits
            //    tbInsertMid2digit.Text = tbInsertMid2digit.Text.Substring(0, 2);
            //    tbInsertMid2digit.SelectionStart = tbInsertMid2digit.Text.Length; // Set cursor to the end of the text
            //}
        }

        private void btnInsertMid2Digit_Click(object sender, EventArgs e)
        {

            //SaveData(tbInsertMid2digit, n2DigitPath);
            //LoadData(cbMid2Digit, n2DigitPath);

            //if (cbMid2Digit.Items.Count > 0)
            //{
            //    // Set the selected item to the last item
            //    cbMid2Digit.SelectedIndex = cbMid2Digit.Items.Count - 1;
            //}
        }

        private void btnClrMid2Digit_Click(object sender, EventArgs e)
        {
            //ClearComboBox(n2DigitPath, cbMid2Digit);
        }

        private void btnRfrshMid2Digit_Click(object sender, EventArgs e)
        {
            //LoadData(cbMid2Digit, n2DigitPath);
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
            modifiedItems.Clear();
            //List<string> modifiedItems = new List<string>();

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
                                //dataGridView1.Rows.RemoveAt(i);
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
                    //MessageBox.Show("No more columns available to move data.");
                    break;
                }
            }

            // Show a message once the data has been processed
            MessageBox.Show("Data has been successfully distributed.");
        }

        private async void btnGnrtNmrlgy_Click(object sender, EventArgs e)
        {
            //btnGnrtNmrlgy.Enabled = false;

            // Pre-allocate capacity for generating items
            //List<string> modifiedItems = new List<string>(1000000); // Pre-allocate large capacity

            // Start the number generation process in a background thread without waiting (immediate start)
            Task task = Task.Run(() =>
            {
                // Use Parallel.For to parallelize the work across multiple CPU cores
                Parallel.ForEach(cbNmbrlgySrs.Items.Cast<object>(), item =>
                {
                    //foreach (var item2 in cbMid2Digit.Items)
                    for (int j = 0; j <= 99; ++j)
                    {
                        for (int i = 0; i < 10000; i++)
                        {
                            string modifiedItem = item.ToString() + j.ToString("D2") + i.ToString("D4");

                            // Avoid lock and add to list directly (fast)
                            modifiedItemsNew.Add(modifiedItem);

                        }
                    }
                });

                //MessageBox.Show("Generated");

                //DataTable dataTable = new DataTable();
                //dataTable.Columns.Add("Items", typeof(string));  // Define a single column for the strings

                //// After generating the items, update the DataGridView in bulk
                //dataGridView1.Invoke((MethodInvoker)delegate
                //{
                //    dataGridView1.Rows.Clear();  // Clear old rows
                //});

                // Add all items in a single bulk operation (faster than adding one by one)
                //dataGridView2.Invoke((MethodInvoker)delegate
                //{
                //    foreach (var item in modifiedItems)
                //    {
                //        dataGridView2.Rows.Add(item);
                //    }
                //});

                // Enable the button and show the completion message (in the UI thread)
                //btnGnrtNmrlgy.Invoke((MethodInvoker)delegate
                //{
                //    btnGnrtNmrlgy.Enabled = true;
                //    MessageBox.Show("Generated");
                //});

                //MessageBox.Show("Generated");
                MessageBox.Show("✅ Numbers Generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnGnrtNmrlgy.Enabled = false;
            });

            // This button becomes visible without waiting for task completion
            //btnCopy1.Visible = true;
            //dataGridView2.Visible = true;
            //btnClrList2.Visible = true;
        }


        private void btnFilterDigit_Click(object sender, EventArgs e)
        {
            List<string> rowDataList = new List<string>();

            // Collecting data from DataGridView (only the first column)
            //foreach (DataGridViewRow row in dataGridView2.Rows)
            //{
            //    if (row.Cells[0].Value != null)
            //    {
            //        rowDataList.Add(row.Cells[0].Value.ToString());
            //    }
            //}
            rowDataList.AddRange(modifiedItemsNew);

            string selectedDigits = GetSelectedDigits();
            HashSet<char> selectedDigitSet = new HashSet<char>(selectedDigits); // Set for fast lookup

            // Filter rows based on selected digits
            filteredData = rowDataList
                .Where(data => !ContainsUnwantedDigits(data, selectedDigitSet)) // Faster check with HashSet
                .ToList();

            // Clear DataGridView and add all filtered data at once
            //dataGridView3.Rows.Clear();
            //foreach (var data in filteredData)
            //{
            //    dataGridView3.Rows.Add(data); // Add rows to DataGridView
            //}

            //var rows = filteredData.Select(item => new DataGridViewRow
            //{
            //    Cells = { new DataGridViewTextBoxCell { Value = item } }
            //}).ToArray();

            //// Add all rows at once using AddRange
            //dataGridView3.Rows.AddRange(rows);

            //if (dataGridView3.Visible == false)
            //{
            //    dataGridView3.Visible = true;
            //    btnCopy2.Visible = true;
            //    btnClrList3.Visible = true;
            //}

            Clipboard.Clear();
            // Join the list into a single string (each item on a new line)
            string clipboardText = string.Join(Environment.NewLine, filteredData);

            // Copy to clipboard
            Clipboard.SetText(clipboardText);


            //MessageBox.Show("Required digits are filtered.");
            MessageBox.Show("✅ Required digits are filtered! and Copied", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //btnFilterDigit.Enabled = false;
        }

        // Method to get selected digits from checkboxes
        private string GetSelectedDigits()
        {
            StringBuilder selectedDigits = new StringBuilder();

            if (checkBox0.Checked) selectedDigits.Append("0");
            if (checkBox1.Checked) selectedDigits.Append("1");
            if (checkBox2.Checked) selectedDigits.Append("2");
            if (checkBox3.Checked) selectedDigits.Append("3");
            if (checkBox4.Checked) selectedDigits.Append("4");
            if (checkBox5.Checked) selectedDigits.Append("5");
            if (checkBox6.Checked) selectedDigits.Append("6");
            if (checkBox7.Checked) selectedDigits.Append("7");
            if (checkBox8.Checked) selectedDigits.Append("8");
            if (checkBox9.Checked) selectedDigits.Append("9");

            return selectedDigits.ToString();
        }

        // Optimized method to check unwanted digits
        private bool ContainsUnwantedDigits(string rowData, HashSet<char> selectedDigitSet)
        {
            foreach (char c in rowData)
            {
                if (!selectedDigitSet.Contains(c)) // Check if the digit is not in the selected set
                {
                    return true; // Unwanted digit found
                }
            }

            return false; // All digits are in the selected set
        }


        private void btnFilterSum_Click(object sender, EventArgs e)
        {
            //CopyDataToDataGridView(dataGridView3, dataGridView4);

            List<string> rowDataList = new List<string>();

            rowDataList.AddRange(filteredData);

            List<int> requiredSums = new List<int>();  // List to store selected sums

            // Add the corresponding sum values to the list based on checked checkboxes
            if (checkBox10.Checked) requiredSums.Add(0);
            //if (checkBox11.Checked) requiredSums.Add(1);
            if (checkBox11.Checked) requiredSums.Add(10);
            if (checkBox12.Checked) requiredSums.Add(2);
            if (checkBox13.Checked) requiredSums.Add(3);
            if (checkBox14.Checked) requiredSums.Add(4);
            if (checkBox15.Checked) requiredSums.Add(5);
            if (checkBox16.Checked) requiredSums.Add(6);
            if (checkBox17.Checked) requiredSums.Add(7);
            if (checkBox18.Checked) requiredSums.Add(8);
            if (checkBox19.Checked) requiredSums.Add(9);

            //List<string> filteredRows = new List<string>();

            foreach (string rowData in rowDataList)
            {
                int sum = CalculateDigitSum(rowData);

                // Check if the sum is a two-digit number (between 10 and 99)
                if (sum >= 10 && sum <= 99)
                {
                    // Recalculate sum of digits if the sum is a two-digit number
                    sum = CalculateDigitSum(sum.ToString());
                }

                // If the sum is in the list of required sums, add it to the filtered list
                if (requiredSums.Contains(sum))
                {
                    filteredRows.Add(rowData);
                }
            }


            // Clear DataGridView and add all filtered data at once
            //dataGridView4.Rows.Clear();

            

            if (radioButtonShow.Checked)
            {
                dataGridView4.Rows.Clear();

                foreach (var data in filteredRows)
                {
                    dataGridView4.Rows.Add(data); // Add rows to DataGridView
                }
            }


            if (dataGridView4.Visible == false)
            {
                dataGridView4.Visible = true;
                btnCopy3.Visible = true;
                btnClrList4.Visible = true;
            }

            Clipboard.Clear();

            // Join the list into a single string (each item on a new line)
            string clipboardText = string.Join(Environment.NewLine, filteredRows);

            // Copy to clipboard
            Clipboard.SetText(clipboardText);

            //MessageBox.Show("Filtered by sum.");
            MessageBox.Show("✅ Filtered by sum & Copied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnFltrRptvDgts_Click(object sender, EventArgs e)
        {
            FilterRepetitve.Clear();
            List<string> rowDataList = new List<string>();

            rowDataList.AddRange(filteredRows);

            if (rowDataList.Count == 0)
            {
                MessageBox.Show("No data to filter.");

            }
            else
            {
                //foreach (string data in rowDataList)
                //{
                //    // Check if the row contains any repetitive digits based on the selected checkboxes
                //    //if (ContainsRepetitiveDigits(data))
                //    //{
                //    //    // Remove the row if it contains repetitive digits that are not required
                //    //    //rowDataList.Remove(data);
                //    //    rowDataList.RemoveAt(rowDataList.IndexOf(data));
                //    //}

                //    rowDataList.RemoveAll(data => ContainsRepetitiveDigits(data));
                //}
                for (int i = rowDataList.Count - 1; i >= 0; i--)
                {
                    if (ContainsRepetitiveDigits(rowDataList[i]))
                    {
                        rowDataList.RemoveAt(i);
                    }
                }

            }

            FilterRepetitve.AddRange(rowDataList);

            if (radioButtonShow.Checked)
            {
                dataGridView5.Rows.Clear();
                foreach (var data in FilterRepetitve)
                {
                    dataGridView5.Rows.Add(data); // Add rows to DataGridView
                }
            }

            //dataGridView5.Rows.Clear();
            //CopyDataToDataGridView(dataGridView4, dataGridView5);

            //// Loop through all the rows in DataGridView in reverse order
            //for (int i = dataGridView5.Rows.Count - 1; i >= 0; i--)
            //{
            //    DataGridViewRow row = dataGridView5.Rows[i];

            //    // Ensure the row has data in the first column (column 0)
            //    if (row.Cells[0].Value != null)
            //    {
            //        string rowData = row.Cells[0].Value.ToString();

            //        // Check if the row contains any repetitive digits based on the selected checkboxes
            //        if (ContainsRepetitiveDigits(rowData))
            //        {
            //            // Remove the row if it contains repetitive digits that are not required
            //            dataGridView5.Rows.RemoveAt(i);
            //        }
            //    }
            //}

            if (dataGridView5.Visible == false)
            {
                dataGridView5.Visible = true;
                btnCopy4.Visible = true;
                btnClrList5.Visible = true;
            }

            Clipboard.Clear();

            // Join the list into a single string (each item on a new line)
            string clipboardText = string.Join(Environment.NewLine, FilterRepetitve);

            // Copy to clipboard
            Clipboard.SetText(clipboardText);

            //MessageBox.Show("Repetitive Filter applied.");
            MessageBox.Show("✅ Repetitive Filter applied and Copied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


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
            FilterPair.Clear();
            List<string> rowDataList = new List<string>();
            rowDataList.Clear();
            rowDataList.AddRange(FilterRepetitve);

            if (rowDataList.Count == 0)
            {
                MessageBox.Show("No data to filter.");

            }
            else
            {

                for (int i = rowDataList.Count - 1; i >= 0; i--)
                {
                    foreach (string item in cbPair.Items)
                    {
                        if (rowDataList[i].Contains(item.ToString()))
                        {
                            rowDataList.RemoveAt(i);
                            break;
                        }
                    }

                }



            }

            FilterPair.Clear();
            FilterPair.AddRange(rowDataList);
            FilterPair = rowDataList.Distinct().ToList();

            if (radioButtonShow.Checked)
            {
                dataGridView6.Rows.Clear();

                foreach (var data in FilterPair)
                {
                    dataGridView6.Rows.Add(data); // Add rows to DataGridView
                }
            }

            

            Clipboard.Clear();
            // Join the list into a single string (each item on a new line)
            string clipboardText = string.Join(Environment.NewLine, FilterPair);

            // Copy to clipboard
            Clipboard.SetText(clipboardText);

            //MessageBox.Show("Filtered by pair.");
            MessageBox.Show("✅ Filtered by pair & Data copied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //CopyDataToDataGridView(dataGridView5, dataGridView6);

            //// Loop through each row in the DataGridView in reverse order (to avoid skipping rows when deleting)
            //for (int i = dataGridView6.Rows.Count - 1; i >= 0; i--)
            //{
            //    DataGridViewRow row = dataGridView6.Rows[i];

            //    // Check if the row is not a new row
            //    if (!row.IsNewRow)
            //    {
            //        // Loop through each cell in the row
            //        foreach (DataGridViewCell cell in row.Cells)
            //        {
            //            foreach (string item in cbPair.Items)
            //            {
            //                // Check if the cell's value matches the search value
            //                if (cell.Value != null && cell.Value.ToString().Contains(item))
            //                {
            //                    // Remove the row if the value is found
            //                    dataGridView6.Rows.RemoveAt(i);
            //                    break; // Stop checking other cells in this row after deletion
            //                }
            //            }
            //        }
            //    }
            //}

            if (dataGridView6.Visible == false)
            {
                dataGridView6.Visible = true;
                btnCopy5.Visible = true;
                btnClrList6.Visible = true;
            }
            //}
        }

        private void btnCopy1_Click(object sender, EventArgs e)
        {
            //CopyColumnData(dataGridView2, "Column15");
        }

        private void btnCopy2_Click(object sender, EventArgs e)
        {
            //CopyColumnData(dataGridView3, "Column16");
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
            cbPair.Text = string.Empty;
            cbPair.SelectedItem = null;
            tbPair.Clear();
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
            modifiedItems.Clear();
            //List<string> numbersList = new List<string>();

            if (cbSrsNmbrs.SelectedItem != null)
            {
                string selectedValue = (string)cbSrsNmbrs.SelectedItem;

                if (tbUpto6Digit.Text.Length == 5)
                {
                    for (int i = 0; i < 10; ++i)
                    {
                        string updatedItem = cbSrsNmbrs.SelectedItem.ToString() + i.ToString() + tbUpto6Digit.Text;
                        modifiedItems.Add(updatedItem);
                    }
                }

                else if (tbUpto6Digit.Text.Length == 4)
                {
                    for (int i = 0; i < 100; ++i)
                    {
                        string updatedItem = cbSrsNmbrs.SelectedItem.ToString() + i.ToString("D2") + tbUpto6Digit.Text;
                        modifiedItems.Add(updatedItem);
                    }
                }

                else if (tbUpto6Digit.Text.Length == 3)
                {
                    for (int i = 0; i < 1000; ++i)
                    {
                        string updatedItem = cbSrsNmbrs.SelectedItem.ToString() + i.ToString("D3") + tbUpto6Digit.Text;
                        modifiedItems.Add(updatedItem);
                    }
                }

                else if (tbUpto6Digit.Text.Length == 2)
                {
                    for (int i = 0; i < 10000; ++i)
                    {
                        string updatedItem = cbSrsNmbrs.SelectedItem.ToString() + i.ToString("D4") + tbUpto6Digit.Text;
                        modifiedItems.Add(updatedItem);
                    }
                }

            }

            foreach (var item in modifiedItems)
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
            //List<string> rowDataList = new List<string>();
            btnGnrtNmrlgy.Enabled = false;

            modifiedItemsNew.AddRange(modifiedItems);

            ////CopyDataToDataGridView(dataGridView1, dataGridView2);
            //dataGridView2.SuspendLayout();
            //dataGridView2.Rows.Clear();
            //var rowsCount = dataGridView1.Rows.Count;
            //Parallel.For(0, rowsCount, i =>
            //{
            //    if (!dataGridView1.Rows[i].IsNewRow)
            //    {
            //        var cellValue = dataGridView1.Rows[i].Cells[0].Value ?? DBNull.Value;

            //        dataGridView2.Invoke(new Action(() =>
            //        {
            //            dataGridView2.Rows.Add(cellValue);
            //        }));
            //    }
            //});
            //dataGridView2.ResumeLayout();

            //btnCopy1.Visible = true;
            //dataGridView2.Visible = true;
            //btnClrList2.Visible = true;
            MessageBox.Show("Filter applied successfully.");
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
            //ClearDataGridRows(dataGridView2);
        }

        private void btnClrList3_Click(object sender, EventArgs e)
        {
            //ClearDataGridRows(dataGridView3);
        }

        private void btnClrList4_Click(object sender, EventArgs e)
        {
            ClearDataGridRows(dataGridView4);
        }

        private void btnClrList5_Click(object sender, EventArgs e)
        {
            ClearDataGridRows(dataGridView5);
            FilterRepetitve.Clear();
        }

        private void btnClrList6_Click(object sender, EventArgs e)
        {
            ClearDataGridRows(dataGridView6);
            FilterPair.Clear();
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

            modifiedItems.Clear();
            //dataGridView1.Rows.Clear();
            //List<string> modifiedItems = new List<string>();

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
            //ClearDataGridRows(dataGridView1);
            if (Clipboard.ContainsText())
            {

                string clipboardText = Clipboard.GetText();

                // Split the clipboard text by new line character
                string[] rows = clipboardText.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                // Loop through each row
                foreach (string row in rows)
                {
                    //if (!string.IsNullOrEmpty(row))
                    //{
                    //    // Split columns by tab or other delimiter
                    //    string[] cells = row.Split('\t');

                    //    DataGridViewRow newRow =new DataGridViewRow();

                    //    foreach (string cell in cells)
                    //    {
                    //        newRow.Cells.Add(new DataGridViewTextBoxCell { Value = cell });
                    //    }

                    //    modifiedItems.Add(newRow);
                    //    dataGridView1.Rows.Add(newRow);
                    //}
                    string trimmedRow = row.Trim();
                    if (!string.IsNullOrEmpty(trimmedRow))
                    {
                        modifiedItems.Add(trimmedRow);
                        dataGridView1.Rows.Add(trimmedRow);
                    }
                }

            }
            else
            {
                MessageBox.Show("No data to paste.");
            }


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            modifiedItems.Clear();
            modifiedItemsNew.Clear();
            filteredData.Clear();
            FilterRepetitve.Clear();
            FilterPair.Clear();
            Clipboard.Clear();
            btnFilterDigit.Enabled = true;
            btnGnrtNmrlgy.Enabled = true;
            dataGridView1.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            dataGridView6.Rows.Clear();
        }

        private void btnDoublePair_Click(object sender, EventArgs e)
        {
            List<string> numbers = new List<string>();

            foreach (var item in cbSrsNmbrs.Items)
            {
                for (int i = 0; i < 100; i++)
                {
                    string number = item.ToString() + item.ToString() + i.ToString("D2");
                    numbers.Add(number);
                }

                for (int i = 0; i < 100; i++)
                {
                    string number = item.ToString() + i.ToString("D2") + item.ToString();
                    numbers.Add(number);
                }

                for (int i = 0; i < 100; i++)
                {
                    string twoDigit = item.ToString();
                    string number = item.ToString() + twoDigit.Substring(twoDigit.Length - 2) + i.ToString("D2") + i.ToString("D2");
                    numbers.Add(number);
                }

                for (int i = 0; i < 100; i++)
                {
                    string number = item.ToString() + "00" + i.ToString("D2") + "00";
                    numbers.Add(number);
                }

            }

            foreach (var i in numbers)
            {
                dataGridView1.Rows.Add(i);
            }

        }

        
    }
}
