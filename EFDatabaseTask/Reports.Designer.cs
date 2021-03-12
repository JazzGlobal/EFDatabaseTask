
namespace EFDatabaseTask
{
    partial class Reports
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportTypeComboBox = new System.Windows.Forms.ComboBox();
            this.reportTypeLabel = new System.Windows.Forms.Label();
            this.generateReportButton = new System.Windows.Forms.Button();
            this.reportOutputRichEditTextbox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // reportTypeComboBox
            // 
            this.reportTypeComboBox.FormattingEnabled = true;
            this.reportTypeComboBox.Location = new System.Drawing.Point(12, 25);
            this.reportTypeComboBox.Name = "reportTypeComboBox";
            this.reportTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.reportTypeComboBox.TabIndex = 0;
            // 
            // reportTypeLabel
            // 
            this.reportTypeLabel.AutoSize = true;
            this.reportTypeLabel.Location = new System.Drawing.Point(12, 6);
            this.reportTypeLabel.Name = "reportTypeLabel";
            this.reportTypeLabel.Size = new System.Drawing.Size(66, 13);
            this.reportTypeLabel.TabIndex = 1;
            this.reportTypeLabel.Text = "Report Type";
            // 
            // generateReportButton
            // 
            this.generateReportButton.Location = new System.Drawing.Point(58, 52);
            this.generateReportButton.Name = "generateReportButton";
            this.generateReportButton.Size = new System.Drawing.Size(75, 23);
            this.generateReportButton.TabIndex = 2;
            this.generateReportButton.Text = "Generate";
            this.generateReportButton.UseVisualStyleBackColor = true;
            this.generateReportButton.Click += new System.EventHandler(this.generateReportButton_Click);
            // 
            // reportOutputRichEditTextbox
            // 
            this.reportOutputRichEditTextbox.Location = new System.Drawing.Point(139, 13);
            this.reportOutputRichEditTextbox.Name = "reportOutputRichEditTextbox";
            this.reportOutputRichEditTextbox.Size = new System.Drawing.Size(332, 179);
            this.reportOutputRichEditTextbox.TabIndex = 5;
            this.reportOutputRichEditTextbox.Text = "";
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 204);
            this.Controls.Add(this.reportOutputRichEditTextbox);
            this.Controls.Add(this.generateReportButton);
            this.Controls.Add(this.reportTypeLabel);
            this.Controls.Add(this.reportTypeComboBox);
            this.Name = "Reports";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.Reports_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox reportTypeComboBox;
        private System.Windows.Forms.Label reportTypeLabel;
        private System.Windows.Forms.Button generateReportButton;
        private System.Windows.Forms.RichTextBox reportOutputRichEditTextbox;
    }
}