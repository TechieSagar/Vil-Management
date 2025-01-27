namespace Vil_Management
{
    partial class UpdateControl
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
            panel1 = new Panel();
            btnUpdateVsearch = new Button();
            btnUpdateRetainedNo = new Button();
            btnUpdateRetention = new Button();
            btnUpdateTnps = new Button();
            btnUpdateSale = new Button();
            tbUpdateRetentionNo = new TextBox();
            tbUpdateRetention = new TextBox();
            tbUpdateVsearch = new TextBox();
            tbUpdateTnps = new TextBox();
            tbUpdateTarget = new TextBox();
            tbUpdateSale = new TextBox();
            label1 = new Label();
            panel2 = new Panel();
            btnUpdateConest = new Button();
            tbUpdateContest = new TextBox();
            dtpContest = new DateTimePicker();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnUpdateVsearch);
            panel1.Controls.Add(btnUpdateRetainedNo);
            panel1.Controls.Add(btnUpdateRetention);
            panel1.Controls.Add(btnUpdateTnps);
            panel1.Controls.Add(btnUpdateSale);
            panel1.Controls.Add(tbUpdateRetentionNo);
            panel1.Controls.Add(tbUpdateRetention);
            panel1.Controls.Add(tbUpdateVsearch);
            panel1.Controls.Add(tbUpdateTnps);
            panel1.Controls.Add(tbUpdateTarget);
            panel1.Controls.Add(tbUpdateSale);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(25, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(520, 333);
            panel1.TabIndex = 0;
            // 
            // btnUpdateVsearch
            // 
            btnUpdateVsearch.Location = new Point(255, 113);
            btnUpdateVsearch.Name = "btnUpdateVsearch";
            btnUpdateVsearch.Size = new Size(94, 29);
            btnUpdateVsearch.TabIndex = 2;
            btnUpdateVsearch.Text = "Update";
            btnUpdateVsearch.UseVisualStyleBackColor = true;
            // 
            // btnUpdateRetainedNo
            // 
            btnUpdateRetainedNo.Location = new Point(255, 176);
            btnUpdateRetainedNo.Name = "btnUpdateRetainedNo";
            btnUpdateRetainedNo.Size = new Size(94, 29);
            btnUpdateRetainedNo.TabIndex = 2;
            btnUpdateRetainedNo.Text = "Update";
            btnUpdateRetainedNo.UseVisualStyleBackColor = true;
            // 
            // btnUpdateRetention
            // 
            btnUpdateRetention.Location = new Point(255, 143);
            btnUpdateRetention.Name = "btnUpdateRetention";
            btnUpdateRetention.Size = new Size(94, 29);
            btnUpdateRetention.TabIndex = 2;
            btnUpdateRetention.Text = "Update";
            btnUpdateRetention.UseVisualStyleBackColor = true;
            // 
            // btnUpdateTnps
            // 
            btnUpdateTnps.Location = new Point(255, 78);
            btnUpdateTnps.Name = "btnUpdateTnps";
            btnUpdateTnps.Size = new Size(94, 29);
            btnUpdateTnps.TabIndex = 2;
            btnUpdateTnps.Text = "Update";
            btnUpdateTnps.UseVisualStyleBackColor = true;
            // 
            // btnUpdateSale
            // 
            btnUpdateSale.Location = new Point(255, 46);
            btnUpdateSale.Name = "btnUpdateSale";
            btnUpdateSale.Size = new Size(94, 29);
            btnUpdateSale.TabIndex = 2;
            btnUpdateSale.Text = "Update";
            btnUpdateSale.UseVisualStyleBackColor = true;
            btnUpdateSale.Click += btnUpdateSale_Click;
            // 
            // tbUpdateRetentionNo
            // 
            tbUpdateRetentionNo.Location = new Point(28, 178);
            tbUpdateRetentionNo.Name = "tbUpdateRetentionNo";
            tbUpdateRetentionNo.PlaceholderText = "Enter Retained Number";
            tbUpdateRetentionNo.Size = new Size(221, 27);
            tbUpdateRetentionNo.TabIndex = 1;
            // 
            // tbUpdateRetention
            // 
            tbUpdateRetention.Location = new Point(28, 145);
            tbUpdateRetention.Name = "tbUpdateRetention";
            tbUpdateRetention.PlaceholderText = "Retention 0-100";
            tbUpdateRetention.Size = new Size(221, 27);
            tbUpdateRetention.TabIndex = 1;
            // 
            // tbUpdateVsearch
            // 
            tbUpdateVsearch.Location = new Point(28, 112);
            tbUpdateVsearch.Name = "tbUpdateVsearch";
            tbUpdateVsearch.PlaceholderText = "V - Search from 0-100";
            tbUpdateVsearch.Size = new Size(221, 27);
            tbUpdateVsearch.TabIndex = 1;
            // 
            // tbUpdateTnps
            // 
            tbUpdateTnps.Location = new Point(28, 79);
            tbUpdateTnps.Name = "tbUpdateTnps";
            tbUpdateTnps.PlaceholderText = "Enter TNPS if Done any today";
            tbUpdateTnps.Size = new Size(221, 27);
            tbUpdateTnps.TabIndex = 1;
            // 
            // tbUpdateTarget
            // 
            tbUpdateTarget.Location = new Point(168, 47);
            tbUpdateTarget.Name = "tbUpdateTarget";
            tbUpdateTarget.PlaceholderText = "Target";
            tbUpdateTarget.Size = new Size(81, 27);
            tbUpdateTarget.TabIndex = 1;
            // 
            // tbUpdateSale
            // 
            tbUpdateSale.Location = new Point(28, 46);
            tbUpdateSale.Name = "tbUpdateSale";
            tbUpdateSale.PlaceholderText = "Today's Activation";
            tbUpdateSale.Size = new Size(134, 27);
            tbUpdateSale.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(175, 0);
            label1.Name = "label1";
            label1.Size = new Size(151, 31);
            label1.TabIndex = 0;
            label1.Text = "Performance";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnUpdateConest);
            panel2.Controls.Add(tbUpdateContest);
            panel2.Controls.Add(dtpContest);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(578, 23);
            panel2.Name = "panel2";
            panel2.Size = new Size(553, 331);
            panel2.TabIndex = 1;
            // 
            // btnUpdateConest
            // 
            btnUpdateConest.Location = new Point(286, 45);
            btnUpdateConest.Name = "btnUpdateConest";
            btnUpdateConest.Size = new Size(117, 65);
            btnUpdateConest.TabIndex = 3;
            btnUpdateConest.Text = "Save";
            btnUpdateConest.UseVisualStyleBackColor = true;
            // 
            // tbUpdateContest
            // 
            tbUpdateContest.Location = new Point(31, 83);
            tbUpdateContest.Name = "tbUpdateContest";
            tbUpdateContest.PlaceholderText = "Contest Description";
            tbUpdateContest.Size = new Size(248, 27);
            tbUpdateContest.TabIndex = 2;
            // 
            // dtpContest
            // 
            dtpContest.Location = new Point(29, 46);
            dtpContest.Name = "dtpContest";
            dtpContest.Size = new Size(250, 27);
            dtpContest.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(217, 0);
            label2.Name = "label2";
            label2.Size = new Size(96, 31);
            label2.TabIndex = 0;
            label2.Text = "Contest";
            // 
            // UpdateControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 73);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "UpdateControl";
            Size = new Size(1377, 759);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Button btnUpdateVsearch;
        private Button btnUpdateRetainedNo;
        private Button btnUpdateRetention;
        private Button btnUpdateTnps;
        private Button btnUpdateSale;
        private TextBox tbUpdateRetentionNo;
        private TextBox tbUpdateRetention;
        private TextBox tbUpdateVsearch;
        private TextBox tbUpdateTnps;
        private TextBox tbUpdateTarget;
        private TextBox tbUpdateSale;
        private Panel panel2;
        private Label label2;
        private Button btnUpdateConest;
        private TextBox tbUpdateContest;
        private DateTimePicker dtpContest;
    }
}
