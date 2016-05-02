namespace Huffman
{
    partial class FHuffman
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
            this.CodeBtn = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.RichTextBox();
            this.DecodeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CodeBtn
            // 
            this.CodeBtn.Location = new System.Drawing.Point(573, 12);
            this.CodeBtn.Name = "CodeBtn";
            this.CodeBtn.Size = new System.Drawing.Size(75, 23);
            this.CodeBtn.TabIndex = 0;
            this.CodeBtn.Text = "Code";
            this.CodeBtn.UseVisualStyleBackColor = true;
            this.CodeBtn.Click += new System.EventHandler(this.CodeBtn_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Location = new System.Drawing.Point(12, 12);
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.Size = new System.Drawing.Size(555, 225);
            this.OutputTextBox.TabIndex = 1;
            this.OutputTextBox.Text = "";
            // 
            // DecodeBtn
            // 
            this.DecodeBtn.Location = new System.Drawing.Point(573, 41);
            this.DecodeBtn.Name = "DecodeBtn";
            this.DecodeBtn.Size = new System.Drawing.Size(75, 23);
            this.DecodeBtn.TabIndex = 2;
            this.DecodeBtn.Text = "Decode";
            this.DecodeBtn.UseVisualStyleBackColor = true;
            this.DecodeBtn.Click += new System.EventHandler(this.DecodeBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 247);
            this.Controls.Add(this.DecodeBtn);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.CodeBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CodeBtn;
        private System.Windows.Forms.RichTextBox OutputTextBox;
        private System.Windows.Forms.Button DecodeBtn;
    }
}

