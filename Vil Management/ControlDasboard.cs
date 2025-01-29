﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Build.Utilities;
using OfficeOpenXml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Vil_Management
{
    public partial class ControlDasboard : UserControl
    {
        private string filePathWalkin = "WalkinData.csv"; // Path to store Walkin data
        private string taskFilePath = "Tasks.csv"; // Path to store tasks in CSV file
        private string pendingCsvFile = "pending_tasks.csv"; // Path to store pending tasks
        private string completedCsvFile = "completed_tasks.csv"; // Path to store completed tasks
        private string filePathDashboard = "Dashboard.xlsx"; // Path to store Dashboard Data
        private string filePathContest = "Contest.csv"; // Path to store Contest Data
        

        private List<TaskItem> pendingTasks = new List<TaskItem>();
        private List<TaskItem> completedTasks = new List<TaskItem>();

        public ControlDasboard()
        {
            InitializeComponent();
            LoadCsvToGridview(); // Load data from 
            LoadTasks();
            DisplayTasks();
            LoadDashboardData();
            LoadContestData();

        }



        private void btnAddTask_Click(object sender, EventArgs e)
        {
            string title = tbAddTask.Text.Trim();
            var taskDescription = tbAddTask.Text.Trim();
            if (!string.IsNullOrEmpty(taskDescription))
            {
                // Create a new task and add it to pending tasks
                var task = new TaskItem(taskDescription, false);
                pendingTasks.Add(task);
                SaveTasks();  // Save to CSV
                DisplayTasks();  // Update the ListBoxes
                tbAddTask.Clear();
            }

        }

        private void btnAddWalkin_Click(object sender, EventArgs e)
        {
            // Get the input data from TextBox and DateTimePicker
            string number = tbNumberWalkin.Text;
            string query = tbDescWalkin.Text;

            if (!string.IsNullOrEmpty(number) && !string.IsNullOrEmpty(query))
            {
                AddDataToCsv(number, query); // Add data to CSV
                LoadCsvToGridview(); // Load data to DataGridView
            }
            else
            {
                MessageBox.Show("Please enter valid data.");
            }

        }

        private void AddDataToCsv(string number, string query)
        {
            // Check if the file exists, if not, create it and add headers
            if (!File.Exists(filePathWalkin))
            {
                using (StreamWriter sw = new StreamWriter(filePathWalkin))
                {
                    sw.WriteLine("Date,Number,Query"); // Write headers if the file is new
                }
            }

            // Append new data to the CSV file
            using (StreamWriter sw = new StreamWriter(filePathWalkin, true))
            {
                string currentDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                sw.WriteLine($"{currentDate},{number},{query}");
            }

            MessageBox.Show("Data added successfully!");
        }

        // Load data from CSV to DataGridView starting from the last entry
        private void LoadCsvToGridview()
        {
            // Create a list to store rows
            var rows = new System.Collections.Generic.List<string[]>();

            // Read the CSV file
            if (File.Exists(filePathWalkin))
            {
                using (StreamReader sr = new StreamReader(filePathWalkin))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var values = line.Split(',');
                        rows.Add(values);
                    }
                }

                // Remove the header row (first row in reversed list)
                if (rows.Count > 0)
                {
                    rows.RemoveAt(0);  // Skip the header row
                }

                // Reverse the list (to display from the last entry to the top)
                rows.Reverse();

                // Bind the reversed data (without the header) to the DataGridView
                dataGridViewWalkin.Rows.Clear(); // Clear existing rows

                // Bind the reversed data (without the header) to the DataGridView
                foreach (var row in rows)
                {
                    dataGridViewWalkin.Rows.Add(row[0], row[1], row[2]);
                }
                //alkin.DataSource = rows.Select(r => new { Date = r[0], Number = r[1], Query = r[2] }).ToList();
            }
            else
            {
                //MessageBox.Show("Walkin file not found.");
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (listBoxPending.SelectedItem != null)
            {
                var selectedTask = (TaskItem)listBoxPending.SelectedItem;
                // Remove the selected task from pending tasks
                pendingTasks.Remove(selectedTask);

                // Add the task to completed tasks
                completedTasks.Add(selectedTask);
                SaveTasks();  // Save to CSV
                DisplayTasks();  // Update the ListBoxes
            }
        }

        public class TaskItem
        {
            public string Description { get; set; }
            public bool Completed { get; set; }

            public TaskItem(string description, bool completed)
            {
                Description = description;
                Completed = completed;
            }

            public override string ToString()
            {
                return Description;
            }
        }

        // Load tasks from the CSV files (pending and completed)
        private void LoadTasks()
        {
            // Load pending tasks from the pending CSV
            if (File.Exists(pendingCsvFile))
            {
                var lines = File.ReadAllLines(pendingCsvFile);
                pendingTasks.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 1)  // Only description needed for pending tasks
                    {
                        var description = parts[0];
                        pendingTasks.Add(new TaskItem(description, false));
                    }
                }
            }

            // Load completed tasks from the completed CSV
            if (File.Exists(completedCsvFile))
            {
                var lines = File.ReadAllLines(completedCsvFile);
                completedTasks.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 1)  // Only description needed for completed tasks
                    {
                        var description = parts[0];
                        completedTasks.Add(new TaskItem(description, true));
                    }
                }
            }
        }

        // Save pending and completed tasks to the respective CSV files
        private void SaveTasks()
        {
            // Save pending tasks to CSV
            var pendingLines = pendingTasks.Select(t => t.Description);
            File.WriteAllLines(pendingCsvFile, pendingLines);

            // Save completed tasks to CSV
            var completedLines = completedTasks.Select(t => t.Description);
            File.WriteAllLines(completedCsvFile, completedLines);
        }

        // Display tasks in the ListBoxes
        private void DisplayTasks()
        {
           
            // Display pending tasks in the pending ListBox
            listBoxPending.Items.Clear();
            foreach (var task in pendingTasks)
            {
                listBoxPending.Items.Add(task);
            }

            // Display completed tasks in the completed ListBox
            listBoxCompleted.Items.Clear();
            foreach (var task in completedTasks)
            {
                listBoxCompleted.Items.Add(task);
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            if (listBoxCompleted.SelectedItem != null)
            {
                //var selectedTask = (TaskItem)listBoxCompleted.Items[0];
                foreach(var items in completedTasks)
                {
                    pendingTasks.Add((TaskItem)items);
                }

                listBoxPending.Items.Clear();
                foreach (var task in pendingTasks)
                {
                    listBoxPending.Items.Add(task);
                }

                listBoxCompleted.Items.Clear();
                completedTasks.Clear();
                // Remove the selected task from pending tasks
                //pendingTasks.Remove(selectedTask);

                SaveTasks();  // Save to CSV
                DisplayTasks();  // Update the ListBoxes
            }
        }

        private void LoadDashboardData()
        {
            // Open the Excel file using EPPlus
            string file = "Dashboard.xlsx";

            // Ensure the file exists
            if (!File.Exists(file))
            {
                //MessageBox.Show("Excel file not found.");
                return;
            }

            FileInfo fileInfo = new FileInfo(file);
             

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
                    string sale = worksheet.Cells[2, 1].Text ?? "Default Value";   // Row 1, Column 1 (A1)
                    string target = worksheet.Cells[2, 2].Text ?? "Default Value";
                    string tnps = worksheet.Cells[2, 3].Text ?? "Default Value";
                    string vSearch = worksheet.Cells[2, 4].Text ?? "Default Value";
                    string retention = worksheet.Cells[2, 5].Text ?? "Default Value";
                    //string retention = worksheet.Cells[2, 6].Text ?? "Default Value";
                    string eq = worksheet.Cells[2, 7].Text ?? "Default Value";
                    string dq = worksheet.Cells[2, 8].Text ?? "Default Value";
                    //string sale = worksheet.Cells[1, 1].Text;

                    //foreach(int i in worksheet.Rows)
                    //{

                    //}

                    // Set the value to the TextBox
                    labelSale.Text = sale + " Vs " + target;
                    labelTnps.Text = tnps;
                    labelVsearch.Text = vSearch + "%";
                    labelRetention.Text = retention + "%";
                    labelEq.Text = eq + "%";
                    labelDq.Text = dq + "%";

                    // set the progressbar value
                    pbSale.Value = (int.Parse(sale) * 100) / int.Parse(target);
                    pbTnps.Value = (int.Parse(tnps) * 100) / 10;
                    pbVsearch.Value = (int.Parse(vSearch));
                    pbRetention.Value = (int.Parse(retention));
                    pbEq.Value = (int.Parse(eq));
                    pbDq.Value = (int.Parse(dq));
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Excel file: " + ex.Message);
            }
        }

        private void LoadContestData()
        {
            // Create a list to store rows
            var rows = new System.Collections.Generic.List<string[]>();

            // Read the CSV file
            if (File.Exists(filePathContest))
            {
                using (StreamReader sr = new StreamReader(filePathContest))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var values = line.Split(',');
                        rows.Add(values);
                    }
                }

                // Remove the header row (first row in reversed list)
                if (rows.Count > 0)
                {
                    rows.RemoveAt(0);  // Skip the header row
                }

                // Reverse the list (to display from the last entry to the top)
                rows.Reverse();

                // Bind the reversed data (without the header) to the DataGridView
                dataGridViewContest.Rows.Clear(); // Clear existing rows

                // Bind the reversed data (without the header) to the DataGridView
                foreach (var row in rows)
                {
                    dataGridViewContest.Rows.Add(row[0], row[1]);
                }
                //alkin.DataSource = rows.Select(r => new { Date = r[0], Number = r[1], Query = r[2] }).ToList();
            }
            else
            {
                //MessageBox.Show("Walkin file not found.");
            }
        }

    }
}
