namespace LocationManagementSystem
{
    partial class SearchForm
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
            this.btnSearch.Click -= new System.EventHandler(this.btnSearch_Click);

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.rbtCardNumber = new System.Windows.Forms.RadioButton();
            this.rbtCnicNumber = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblLocation = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maskedTextBox1);
            this.groupBox1.Controls.Add(this.rbtCardNumber);
            this.groupBox1.Controls.Add(this.rbtCnicNumber);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(36, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Card Holder";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(6, 30);
            this.maskedTextBox1.Mask = "00000-0000000-0";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(225, 20);
            this.maskedTextBox1.TabIndex = 3;
            // 
            // rbtCardNumber
            // 
            this.rbtCardNumber.AutoSize = true;
            this.rbtCardNumber.Location = new System.Drawing.Point(102, 56);
            this.rbtCardNumber.Name = "rbtCardNumber";
            this.rbtCardNumber.Size = new System.Drawing.Size(87, 17);
            this.rbtCardNumber.TabIndex = 5;
            this.rbtCardNumber.TabStop = true;
            this.rbtCardNumber.Text = "Card Number";
            this.rbtCardNumber.UseVisualStyleBackColor = true;
            this.rbtCardNumber.CheckedChanged += new System.EventHandler(this.rbtCardNumber_CheckedChanged);
            // 
            // rbtCnicNumber
            // 
            this.rbtCnicNumber.AutoSize = true;
            this.rbtCnicNumber.Checked = true;
            this.rbtCnicNumber.Location = new System.Drawing.Point(6, 56);
            this.rbtCnicNumber.Name = "rbtCnicNumber";
            this.rbtCnicNumber.Size = new System.Drawing.Size(90, 17);
            this.rbtCnicNumber.TabIndex = 4;
            this.rbtCnicNumber.TabStop = true;
            this.rbtCnicNumber.Text = "CNIC Number";
            this.rbtCnicNumber.UseVisualStyleBackColor = true;
            this.rbtCnicNumber.CheckedChanged += new System.EventHandler(this.rbtCnicNumber_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(156, 86);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(39, 9);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(72, 27);
            this.lblLocation.TabIndex = 2;
            this.lblLocation.Text = "label1";
            // 
            // SearchForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 216);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.RadioButton rbtCardNumber;
        private System.Windows.Forms.RadioButton rbtCnicNumber;
    }
}