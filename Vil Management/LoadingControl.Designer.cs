namespace Vil_Management
{
    partial class LoadingControl
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
            labelLoading = new Label();
            SuspendLayout();
            // 
            // labelLoading
            // 
            labelLoading.AutoSize = true;
            labelLoading.Font = new Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelLoading.ForeColor = Color.FromArgb(0, 126, 249);
            labelLoading.Location = new Point(0, 0);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(414, 106);
            labelLoading.TabIndex = 0;
            labelLoading.Text = "Loading...";
            // 
            // LoadingControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelLoading);
            Name = "LoadingControl";
            Size = new Size(441, 143);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelLoading;
    }
}
