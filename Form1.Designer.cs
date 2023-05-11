namespace Upholstery_Builder
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.styleIDBox = new System.Windows.Forms.TextBox();
            this.lookUpButton = new System.Windows.Forms.Button();
            this.topViewBox = new System.Windows.Forms.CheckBox();
            this.frontViewBox = new System.Windows.Forms.CheckBox();
            this.sideViewBox = new System.Windows.Forms.CheckBox();
            this.otherViewBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.initialsBox = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose a style ID";
            // 
            // styleIDBox
            // 
            this.styleIDBox.Location = new System.Drawing.Point(25, 54);
            this.styleIDBox.Name = "styleIDBox";
            this.styleIDBox.ReadOnly = true;
            this.styleIDBox.Size = new System.Drawing.Size(154, 20);
            this.styleIDBox.TabIndex = 1;
            // 
            // lookUpButton
            // 
            this.lookUpButton.Location = new System.Drawing.Point(185, 49);
            this.lookUpButton.Name = "lookUpButton";
            this.lookUpButton.Size = new System.Drawing.Size(69, 25);
            this.lookUpButton.TabIndex = 2;
            this.lookUpButton.Text = "Look Up";
            this.lookUpButton.UseVisualStyleBackColor = true;
            this.lookUpButton.Click += new System.EventHandler(this.lookUpButton_Click);
            // 
            // topViewBox
            // 
            this.topViewBox.AutoSize = true;
            this.topViewBox.Location = new System.Drawing.Point(31, 123);
            this.topViewBox.Name = "topViewBox";
            this.topViewBox.Size = new System.Drawing.Size(45, 17);
            this.topViewBox.TabIndex = 3;
            this.topViewBox.Text = "Top";
            this.topViewBox.UseVisualStyleBackColor = true;
            // 
            // frontViewBox
            // 
            this.frontViewBox.AutoSize = true;
            this.frontViewBox.Location = new System.Drawing.Point(31, 146);
            this.frontViewBox.Name = "frontViewBox";
            this.frontViewBox.Size = new System.Drawing.Size(50, 17);
            this.frontViewBox.TabIndex = 4;
            this.frontViewBox.Text = "Front";
            this.frontViewBox.UseVisualStyleBackColor = true;
            // 
            // sideViewBox
            // 
            this.sideViewBox.AutoSize = true;
            this.sideViewBox.Location = new System.Drawing.Point(31, 169);
            this.sideViewBox.Name = "sideViewBox";
            this.sideViewBox.Size = new System.Drawing.Size(47, 17);
            this.sideViewBox.TabIndex = 5;
            this.sideViewBox.Text = "Side";
            this.sideViewBox.UseVisualStyleBackColor = true;
            // 
            // otherViewBox
            // 
            this.otherViewBox.AutoSize = true;
            this.otherViewBox.Location = new System.Drawing.Point(31, 192);
            this.otherViewBox.Name = "otherViewBox";
            this.otherViewBox.Size = new System.Drawing.Size(52, 17);
            this.otherViewBox.TabIndex = 6;
            this.otherViewBox.Text = "Other";
            this.otherViewBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Pictures to add";
            // 
            // createButton
            // 
            this.createButton.Enabled = false;
            this.createButton.Location = new System.Drawing.Point(114, 244);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(119, 25);
            this.createButton.TabIndex = 8;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Visible = false;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "Y:\\Product Development\\Style Specifications";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Created By:";
            // 
            // initialsBox
            // 
            this.initialsBox.Location = new System.Drawing.Point(165, 123);
            this.initialsBox.Name = "initialsBox";
            this.initialsBox.Size = new System.Drawing.Size(69, 20);
            this.initialsBox.TabIndex = 10;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoCheck = false;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(25, 80);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Spec already exists";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // updateButton
            // 
            this.updateButton.Enabled = false;
            this.updateButton.Location = new System.Drawing.Point(114, 244);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(119, 25);
            this.updateButton.TabIndex = 12;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Visible = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 301);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.initialsBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.otherViewBox);
            this.Controls.Add(this.sideViewBox);
            this.Controls.Add(this.frontViewBox);
            this.Controls.Add(this.topViewBox);
            this.Controls.Add(this.lookUpButton);
            this.Controls.Add(this.styleIDBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Upholstery Builder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox styleIDBox;
        private System.Windows.Forms.Button lookUpButton;
        private System.Windows.Forms.CheckBox topViewBox;
        private System.Windows.Forms.CheckBox frontViewBox;
        private System.Windows.Forms.CheckBox sideViewBox;
        private System.Windows.Forms.CheckBox otherViewBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox initialsBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button updateButton;
    }
}

