namespace ElGamalCryptosystem
{
    partial class Main
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
            this.plainTextBox = new System.Windows.Forms.TextBox();
            this.cipherTextBox = new System.Windows.Forms.TextBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.pTextBox = new System.Windows.Forms.TextBox();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DecryptRadioButton = new System.Windows.Forms.RadioButton();
            this.EncryptRadioButton = new System.Windows.Forms.RadioButton();
            this.GComboBox = new System.Windows.Forms.ComboBox();
            this.buttonBreak = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plainTextBox
            // 
            this.plainTextBox.Location = new System.Drawing.Point(20, 123);
            this.plainTextBox.Multiline = true;
            this.plainTextBox.Name = "plainTextBox";
            this.plainTextBox.Size = new System.Drawing.Size(209, 200);
            this.plainTextBox.TabIndex = 0;
            this.plainTextBox.TextChanged += new System.EventHandler(this.GComboBox_SelectedIndexChanged);
            // 
            // cipherTextBox
            // 
            this.cipherTextBox.Location = new System.Drawing.Point(283, 123);
            this.cipherTextBox.Multiline = true;
            this.cipherTextBox.Name = "cipherTextBox";
            this.cipherTextBox.Size = new System.Drawing.Size(209, 200);
            this.cipherTextBox.TabIndex = 1;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(20, 344);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(82, 26);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.Location = new System.Drawing.Point(283, 344);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(82, 26);
            this.buttonSaveAs.TabIndex = 3;
            this.buttonSaveAs.Text = "Save As";
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // pTextBox
            // 
            this.pTextBox.Location = new System.Drawing.Point(36, 12);
            this.pTextBox.Name = "pTextBox";
            this.pTextBox.Size = new System.Drawing.Size(100, 20);
            this.pTextBox.TabIndex = 4;
            this.pTextBox.TextChanged += new System.EventHandler(this.plainTextBox_TextChanged);
            this.pTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pTextBox_KeyPress);
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(185, 12);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(100, 20);
            this.xTextBox.TabIndex = 6;
            this.xTextBox.TextChanged += new System.EventHandler(this.plainTextBox_TextChanged);
            this.xTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "P";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "G";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Y";
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(185, 52);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(100, 20);
            this.yTextBox.TabIndex = 11;
            // 
            // buttonRun
            // 
            this.buttonRun.Enabled = false;
            this.buttonRun.Location = new System.Drawing.Point(435, 29);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 12;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonEncrypt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DecryptRadioButton);
            this.groupBox1.Controls.Add(this.EncryptRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(312, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(105, 74);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // DecryptRadioButton
            // 
            this.DecryptRadioButton.AutoSize = true;
            this.DecryptRadioButton.Location = new System.Drawing.Point(6, 51);
            this.DecryptRadioButton.Name = "DecryptRadioButton";
            this.DecryptRadioButton.Size = new System.Drawing.Size(62, 17);
            this.DecryptRadioButton.TabIndex = 1;
            this.DecryptRadioButton.Text = "Decrypt";
            this.DecryptRadioButton.UseVisualStyleBackColor = true;
            // 
            // EncryptRadioButton
            // 
            this.EncryptRadioButton.AutoSize = true;
            this.EncryptRadioButton.Checked = true;
            this.EncryptRadioButton.Location = new System.Drawing.Point(7, 20);
            this.EncryptRadioButton.Name = "EncryptRadioButton";
            this.EncryptRadioButton.Size = new System.Drawing.Size(61, 17);
            this.EncryptRadioButton.TabIndex = 0;
            this.EncryptRadioButton.TabStop = true;
            this.EncryptRadioButton.Text = "Encrypt";
            this.EncryptRadioButton.UseVisualStyleBackColor = true;
            // 
            // GComboBox
            // 
            this.GComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GComboBox.FormattingEnabled = true;
            this.GComboBox.Location = new System.Drawing.Point(36, 51);
            this.GComboBox.Name = "GComboBox";
            this.GComboBox.Size = new System.Drawing.Size(66, 21);
            this.GComboBox.TabIndex = 19;
            this.GComboBox.SelectedIndexChanged += new System.EventHandler(this.GComboBox_SelectedIndexChanged);
            // 
            // buttonBreak
            // 
            this.buttonBreak.Enabled = false;
            this.buttonBreak.Location = new System.Drawing.Point(435, 63);
            this.buttonBreak.Name = "buttonBreak";
            this.buttonBreak.Size = new System.Drawing.Size(75, 23);
            this.buttonBreak.TabIndex = 21;
            this.buttonBreak.Text = "Break";
            this.buttonBreak.UseVisualStyleBackColor = true;
            this.buttonBreak.Click += new System.EventHandler(this.button_Click);
            this.buttonBreak.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pTextBox_KeyPress);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 381);
            this.Controls.Add(this.buttonBreak);
            this.Controls.Add(this.GComboBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.yTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.xTextBox);
            this.Controls.Add(this.pTextBox);
            this.Controls.Add(this.buttonSaveAs);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.cipherTextBox);
            this.Controls.Add(this.plainTextBox);
            this.Name = "Main";
            this.Text = "Main";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox plainTextBox;
        private System.Windows.Forms.TextBox cipherTextBox;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.TextBox pTextBox;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton DecryptRadioButton;
        private System.Windows.Forms.RadioButton EncryptRadioButton;
        private System.Windows.Forms.ComboBox GComboBox;
        private System.Windows.Forms.Button buttonBreak;
    }
}

