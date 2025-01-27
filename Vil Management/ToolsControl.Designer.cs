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
            btnEdge = new Button();
            SuspendLayout();
            // 
            // btnEdge
            // 
            btnEdge.Location = new Point(573, 35);
            btnEdge.Name = "btnEdge";
            btnEdge.Size = new Size(94, 29);
            btnEdge.TabIndex = 0;
            btnEdge.Text = "button1";
            btnEdge.UseVisualStyleBackColor = true;
            btnEdge.Click += btnEdge_Click;
            // 
            // ToolsControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnEdge);
            Name = "ToolsControl";
            Size = new Size(1350, 745);
            ResumeLayout(false);
        }

        #endregion

        private Button btnEdge;
    }
}
