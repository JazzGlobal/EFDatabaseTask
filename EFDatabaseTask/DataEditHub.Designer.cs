﻿
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
            this.editAppointmentsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // editCustomersButton
            // 
            this.editCustomersButton.Location = new System.Drawing.Point(10, 115);
            this.editCustomersButton.Name = "editCustomersButton";
            this.editCustomersButton.Size = new System.Drawing.Size(232, 23);
            this.editCustomersButton.TabIndex = 0;
            this.editCustomersButton.Text = "Edit Customers";
            this.editCustomersButton.UseVisualStyleBackColor = true;
            this.editCustomersButton.Click += new System.EventHandler(this.editCustomersButton_Click);
            // 
            // editAddressesButton
            // 
            this.editAddressesButton.Location = new System.Drawing.Point(9, 144);
            this.editAddressesButton.Name = "editAddressesButton";
            this.editAddressesButton.Size = new System.Drawing.Size(232, 23);
            this.editAddressesButton.TabIndex = 1;
            this.editAddressesButton.Text = "Edit Addresses";
            this.editAddressesButton.UseVisualStyleBackColor = true;
            this.editAddressesButton.Click += new System.EventHandler(this.editAddressesButton_Click);
            // 
            // editAppointmentsButton
            // 
            this.editAppointmentsButton.Location = new System.Drawing.Point(9, 173);
            this.editAppointmentsButton.Name = "editAppointmentsButton";
            this.editAppointmentsButton.Size = new System.Drawing.Size(232, 23);
            this.editAppointmentsButton.TabIndex = 2;
            this.editAppointmentsButton.Text = "Edit Appointments";
            this.editAppointmentsButton.UseVisualStyleBackColor = true;
            this.editAppointmentsButton.Click += new System.EventHandler(this.editAppointmentsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Edit Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "View";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(13, 29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(232, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "View Calendar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // DataEditHub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 212);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editAppointmentsButton);
            this.Controls.Add(this.editAddressesButton);
            this.Controls.Add(this.editCustomersButton);
            this.Name = "DataEditHub";
            this.Text = "DataEditHub";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button editCustomersButton;
        private System.Windows.Forms.Button editAddressesButton;
        private System.Windows.Forms.Button editAppointmentsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}