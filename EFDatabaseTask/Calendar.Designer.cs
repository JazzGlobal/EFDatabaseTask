
namespace EFDatabaseTask
{
    partial class Calendar
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
            this.eventResultBox = new System.Windows.Forms.RichTextBox();
            this.viewButton = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.timePeriodSelection = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // eventResultBox
            // 
            this.eventResultBox.Location = new System.Drawing.Point(258, 18);
            this.eventResultBox.Name = "eventResultBox";
            this.eventResultBox.ReadOnly = true;
            this.eventResultBox.Size = new System.Drawing.Size(232, 162);
            this.eventResultBox.TabIndex = 1;
            this.eventResultBox.Text = "";
            // 
            // viewButton
            // 
            this.viewButton.Location = new System.Drawing.Point(138, 71);
            this.viewButton.Name = "viewButton";
            this.viewButton.Size = new System.Drawing.Size(75, 23);
            this.viewButton.TabIndex = 2;
            this.viewButton.Text = "View";
            this.viewButton.UseVisualStyleBackColor = true;
            this.viewButton.Click += new System.EventHandler(this.viewButton_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(13, 18);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker.TabIndex = 3;
            // 
            // timePeriodSelection
            // 
            this.timePeriodSelection.FormattingEnabled = true;
            this.timePeriodSelection.Location = new System.Drawing.Point(13, 44);
            this.timePeriodSelection.Name = "timePeriodSelection";
            this.timePeriodSelection.Size = new System.Drawing.Size(200, 21);
            this.timePeriodSelection.TabIndex = 4;
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 224);
            this.Controls.Add(this.timePeriodSelection);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.viewButton);
            this.Controls.Add(this.eventResultBox);
            this.Name = "Calendar";
            this.Text = "Calendar";
            this.Load += new System.EventHandler(this.Calendar_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox eventResultBox;
        private System.Windows.Forms.Button viewButton;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.ComboBox timePeriodSelection;
    }
}