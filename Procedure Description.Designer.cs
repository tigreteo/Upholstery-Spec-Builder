namespace Upholstery_Builder
{
    partial class Procedure_Description
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.AddUpholProcedure = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(22, 25);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(233, 202);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // AddUpholProcedure
            // 
            this.AddUpholProcedure.Location = new System.Drawing.Point(117, 233);
            this.AddUpholProcedure.Name = "AddUpholProcedure";
            this.AddUpholProcedure.Size = new System.Drawing.Size(48, 21);
            this.AddUpholProcedure.TabIndex = 1;
            this.AddUpholProcedure.Text = "Okay";
            this.AddUpholProcedure.UseVisualStyleBackColor = true;
            this.AddUpholProcedure.Click += new System.EventHandler(this.AddUpholProcedure_Click);
            // 
            // Procedure_Description
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.AddUpholProcedure);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Procedure_Description";
            this.Text = "Procedure_Description";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button AddUpholProcedure;
    }
}