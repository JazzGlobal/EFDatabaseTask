
namespace EFDatabaseTask
{
    partial class DataEditHub
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
            this.editCustomersButton = new System.Windows.Forms.Button();
            this.editAddressesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // editCustomersButton
            // 
            this.editCustomersButton.Location = new System.Drawing.Point(13, 24);
            this.editCustomersButton.Name = "editCustomersButton";
            this.editCustomersButton.Size = new System.Drawing.Size(232, 23);
            this.editCustomersButton.TabIndex = 0;
            this.editCustomersButton.Text = "Edit Customers";
            this.editCustomersButton.UseVisualStyleBackColor = true;
            this.editCustomersButton.Click += new System.EventHandler(this.editCustomersButton_Click);
            // 
            // editAddressesButton
            // 
            this.editAddressesButton.Location = new System.Drawing.Point(12, 53);
            this.editAddressesButton.Name = "editAddressesButton";
            this.editAddressesButton.Size = new System.Drawing.Size(232, 23);
            this.editAddressesButton.TabIndex = 1;
            this.editAddressesButton.Text = "Edit Addresses";
            this.editAddressesButton.UseVisualStyleBackColor = true;
            this.editAddressesButton.Click += new System.EventHandler(this.editAddressesButton_Click);
            // 
            // DataEditHub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 149);
            this.Controls.Add(this.editAddressesButton);
            this.Controls.Add(this.editCustomersButton);
            this.Name = "DataEditHub";
            this.Text = "DataEditHub";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button editCustomersButton;
        private System.Windows.Forms.Button editAddressesButton;
    }
}