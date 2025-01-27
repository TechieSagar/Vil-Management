namespace Vil_Management
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelSide = new Panel();
            btnContactUs = new Button();
            btnTools = new Button();
            btnUpdate = new Button();
            btnNumberSystem = new Button();
            btnDashboard = new Button();
            panel1 = new Panel();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            labelTitle = new Label();
            panelNav = new Panel();
            controlDasboard1 = new ControlDasboard();
            bindingSource1 = new BindingSource(components);
            numberSystemControl11 = new NumberSystemControl1();
            updateControl1 = new UpdateControl();
            toolsControl1 = new ToolsControl();
            panelSide.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // panelSide
            // 
            panelSide.BackColor = Color.FromArgb(24, 30, 54);
            panelSide.Controls.Add(btnContactUs);
            panelSide.Controls.Add(btnTools);
            panelSide.Controls.Add(btnUpdate);
            panelSide.Controls.Add(btnNumberSystem);
            panelSide.Controls.Add(btnDashboard);
            panelSide.Controls.Add(panel1);
            panelSide.Dock = DockStyle.Left;
            panelSide.Location = new Point(0, 0);
            panelSide.Name = "panelSide";
            panelSide.Size = new Size(250, 875);
            panelSide.TabIndex = 0;
            // 
            // btnContactUs
            // 
            btnContactUs.FlatAppearance.BorderSize = 0;
            btnContactUs.FlatStyle = FlatStyle.Flat;
            btnContactUs.Font = new Font("Nirmala UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnContactUs.ForeColor = Color.FromArgb(0, 126, 249);
            btnContactUs.Image = (Image)resources.GetObject("btnContactUs.Image");
            btnContactUs.ImageAlign = ContentAlignment.MiddleLeft;
            btnContactUs.Location = new Point(32, 374);
            btnContactUs.Name = "btnContactUs";
            btnContactUs.Size = new Size(218, 42);
            btnContactUs.TabIndex = 2;
            btnContactUs.Text = "    Contact Us";
            btnContactUs.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnContactUs.UseVisualStyleBackColor = true;
            btnContactUs.Click += btnContactUs_Click;
            btnContactUs.Leave += btnContactUs_Leave;
            // 
            // btnTools
            // 
            btnTools.FlatAppearance.BorderSize = 0;
            btnTools.FlatStyle = FlatStyle.Flat;
            btnTools.Font = new Font("Nirmala UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTools.ForeColor = Color.FromArgb(0, 126, 249);
            btnTools.Image = (Image)resources.GetObject("btnTools.Image");
            btnTools.ImageAlign = ContentAlignment.MiddleLeft;
            btnTools.Location = new Point(32, 326);
            btnTools.Name = "btnTools";
            btnTools.Size = new Size(218, 42);
            btnTools.TabIndex = 2;
            btnTools.Text = "    Tools";
            btnTools.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTools.UseVisualStyleBackColor = true;
            btnTools.Click += btnTools_Click;
            btnTools.Leave += btnTools_Leave;
            // 
            // btnUpdate
            // 
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Nirmala UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdate.ForeColor = Color.FromArgb(0, 126, 249);
            btnUpdate.Image = (Image)resources.GetObject("btnUpdate.Image");
            btnUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpdate.Location = new Point(32, 278);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(218, 42);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "    Update";
            btnUpdate.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            btnUpdate.Leave += btnUpdate_Leave;
            // 
            // btnNumberSystem
            // 
            btnNumberSystem.FlatAppearance.BorderSize = 0;
            btnNumberSystem.FlatStyle = FlatStyle.Flat;
            btnNumberSystem.Font = new Font("Nirmala UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNumberSystem.ForeColor = Color.FromArgb(0, 126, 249);
            btnNumberSystem.Image = (Image)resources.GetObject("btnNumberSystem.Image");
            btnNumberSystem.ImageAlign = ContentAlignment.MiddleLeft;
            btnNumberSystem.Location = new Point(32, 230);
            btnNumberSystem.Name = "btnNumberSystem";
            btnNumberSystem.Size = new Size(218, 42);
            btnNumberSystem.TabIndex = 2;
            btnNumberSystem.Text = "    Number System";
            btnNumberSystem.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNumberSystem.UseVisualStyleBackColor = true;
            btnNumberSystem.Click += btnNumberSystem_Click;
            btnNumberSystem.Leave += btnNumberSystem_Leave;
            // 
            // btnDashboard
            // 
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Nirmala UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDashboard.ForeColor = Color.FromArgb(0, 126, 249);
            btnDashboard.Image = (Image)resources.GetObject("btnDashboard.Image");
            btnDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnDashboard.Location = new Point(32, 182);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(218, 42);
            btnDashboard.TabIndex = 2;
            btnDashboard.Text = "    Dashboard";
            btnDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            btnDashboard.Leave += btnDashboard_Leave;
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 176);
            panel1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 126, 249);
            label2.Location = new Point(81, 111);
            label2.Name = "label2";
            label2.Size = new Size(103, 20);
            label2.TabIndex = 3;
            label2.Text = "User Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(0, 126, 249);
            label1.Location = new Point(118, 91);
            label1.Name = "label1";
            label1.Size = new Size(28, 20);
            label1.TabIndex = 2;
            label1.Text = "Hi";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(101, 15);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(62, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Microsoft Sans Serif", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitle.Location = new Point(269, 12);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(211, 42);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Dashboard";
            // 
            // panelNav
            // 
            panelNav.BackColor = Color.FromArgb(0, 126, 249);
            panelNav.Location = new Point(0, 182);
            panelNav.Name = "panelNav";
            panelNav.Size = new Size(8, 42);
            panelNav.TabIndex = 1;
            // 
            // controlDasboard1
            // 
            controlDasboard1.BackColor = Color.FromArgb(46, 51, 73);
            controlDasboard1.Location = new Point(267, 56);
            controlDasboard1.Name = "controlDasboard1";
            controlDasboard1.Size = new Size(1157, 807);
            controlDasboard1.TabIndex = 2;
            // 
            // numberSystemControl11
            // 
            numberSystemControl11.BackColor = Color.FromArgb(46, 51, 73);
            numberSystemControl11.Location = new Point(263, 60);
            numberSystemControl11.Name = "numberSystemControl11";
            numberSystemControl11.Size = new Size(1481, 940);
            numberSystemControl11.TabIndex = 3;
            // 
            // updateControl1
            // 
            updateControl1.BackColor = Color.FromArgb(46, 51, 73);
            updateControl1.Location = new Point(269, 72);
            updateControl1.Name = "updateControl1";
            updateControl1.Size = new Size(1721, 949);
            updateControl1.TabIndex = 4;
            // 
            // toolsControl1
            // 
            toolsControl1.Location = new Point(247, 57);
            toolsControl1.Name = "toolsControl1";
            toolsControl1.Size = new Size(1419, 802);
            toolsControl1.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 73);
            ClientSize = new Size(1475, 875);
            Controls.Add(toolsControl1);
            Controls.Add(updateControl1);
            Controls.Add(panelNav);
            Controls.Add(labelTitle);
            Controls.Add(panelSide);
            Controls.Add(controlDasboard1);
            Controls.Add(numberSystemControl11);
            ForeColor = Color.FromArgb(158, 176, 161);
            Name = "Form1";
            Text = "Form1";
            panelSide.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelSide;
        private Panel panel1;
        private Label labelTitle;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label1;
        private Button btnDashboard;
        private Button btnContactUs;
        private Button btnTools;
        private Button btnUpdate;
        private Button btnNumberSystem;
        private Panel panelNav;
        private ControlDasboard controlDasboard1;
        private BindingSource bindingSource1;
        private NumberSystemControl1 numberSystemControl11;
        private UpdateControl updateControl1;
        private ToolsControl toolsControl1;
    }
}
