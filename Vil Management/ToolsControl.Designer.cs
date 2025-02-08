namespace Vil_Management
{
    partial class ToolsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSumeru = new Button();
            btnSwift = new Button();
            btnCpos = new Button();
            btnSearch = new Button();
            btnVsearch = new Button();
            btnCheckRecharge = new Button();
            dtpRecharge = new DateTimePicker();
            dtpResult = new DateTimePicker();
            label1 = new Label();
            tbRechargeDays = new TextBox();
            SuspendLayout();
            // 
            // btnSumeru
            // 
            btnSumeru.Enabled = false;
            btnSumeru.ForeColor = Color.Black;
            btnSumeru.Location = new Point(1181, 29);
            btnSumeru.Name = "btnSumeru";
            btnSumeru.Size = new Size(94, 29);
            btnSumeru.TabIndex = 1;
            btnSumeru.Text = "Sumeru";
            btnSumeru.UseVisualStyleBackColor = true;
            btnSumeru.Visible = false;
            btnSumeru.Click += btnSumeru_Click;
            // 
            // btnSwift
            // 
            btnSwift.Enabled = false;
            btnSwift.ForeColor = Color.Black;
            btnSwift.Location = new Point(1181, 59);
            btnSwift.Name = "btnSwift";
            btnSwift.Size = new Size(94, 29);
            btnSwift.TabIndex = 2;
            btnSwift.Text = "Swift";
            btnSwift.UseVisualStyleBackColor = true;
            btnSwift.Visible = false;
            btnSwift.Click += btnSwift_Click;
            // 
            // btnCpos
            // 
            btnCpos.Enabled = false;
            btnCpos.ForeColor = Color.Black;
            btnCpos.Location = new Point(1181, 94);
            btnCpos.Name = "btnCpos";
            btnCpos.Size = new Size(94, 29);
            btnCpos.TabIndex = 2;
            btnCpos.Text = "Cpos";
            btnCpos.UseVisualStyleBackColor = true;
            btnCpos.Visible = false;
            btnCpos.Click += btnCpos_Click;
            // 
            // btnSearch
            // 
            btnSearch.Enabled = false;
            btnSearch.ForeColor = Color.Black;
            btnSearch.Location = new Point(1181, 129);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(133, 29);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Cpos Master";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Visible = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnVsearch
            // 
            btnVsearch.Enabled = false;
            btnVsearch.ForeColor = Color.Black;
            btnVsearch.Location = new Point(1181, 164);
            btnVsearch.Name = "btnVsearch";
            btnVsearch.Size = new Size(133, 29);
            btnVsearch.TabIndex = 3;
            btnVsearch.Text = "V Search";
            btnVsearch.UseVisualStyleBackColor = true;
            btnVsearch.Visible = false;
            btnVsearch.Click += btnVsearch_Click;
            // 
            // btnCheckRecharge
            // 
            btnCheckRecharge.Location = new Point(60, 131);
            btnCheckRecharge.Name = "btnCheckRecharge";
            btnCheckRecharge.Size = new Size(175, 27);
            btnCheckRecharge.TabIndex = 4;
            btnCheckRecharge.Text = "Check";
            btnCheckRecharge.UseVisualStyleBackColor = true;
            btnCheckRecharge.Click += btnCheckRecharge_Click;
            // 
            // dtpRecharge
            // 
            dtpRecharge.Location = new Point(60, 98);
            dtpRecharge.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpRecharge.Name = "dtpRecharge";
            dtpRecharge.Size = new Size(244, 27);
            dtpRecharge.TabIndex = 6;
            // 
            // dtpResult
            // 
            dtpResult.Location = new Point(241, 131);
            dtpResult.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpResult.Name = "dtpResult";
            dtpResult.Size = new Size(177, 27);
            dtpResult.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(60, 57);
            label1.Name = "label1";
            label1.Size = new Size(239, 31);
            label1.TabIndex = 8;
            label1.Text = "Check Recharge Days";
            // 
            // tbRechargeDays
            // 
            tbRechargeDays.Location = new Point(310, 98);
            tbRechargeDays.Name = "tbRechargeDays";
            tbRechargeDays.PlaceholderText = "Days";
            tbRechargeDays.Size = new Size(108, 27);
            tbRechargeDays.TabIndex = 9;
            tbRechargeDays.TextAlign = HorizontalAlignment.Center;
            // 
            // ToolsControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 73);
            Controls.Add(tbRechargeDays);
            Controls.Add(label1);
            Controls.Add(dtpResult);
            Controls.Add(dtpRecharge);
            Controls.Add(btnCheckRecharge);
            Controls.Add(btnVsearch);
            Controls.Add(btnSearch);
            Controls.Add(btnCpos);
            Controls.Add(btnSwift);
            Controls.Add(btnSumeru);
            ForeColor = Color.Black;
            Name = "ToolsControl";
            Size = new Size(1350, 745);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSumeru;
        private Button btnSwift;
        private Button btnCpos;
        private Button btnSearch;
        private Button btnVsearch;
        private Button btnCheckRecharge;
        private DateTimePicker dtpRecharge;
        private DateTimePicker dtpResult;
        private Label label1;
        private TextBox tbRechargeDays;
    }
}
