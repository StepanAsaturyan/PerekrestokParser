namespace PerekrestokParser.Forms
{
    partial class LinkRequestPage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textAbove = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Submit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textAbove);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.Submit);
            this.groupBox1.Location = new System.Drawing.Point(103, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 211);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // textAbove
            // 
            this.textAbove.BackColor = System.Drawing.SystemColors.Control;
            this.textAbove.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textAbove.Location = new System.Drawing.Point(50, 15);
            this.textAbove.Name = "textAbove";
            this.textAbove.Size = new System.Drawing.Size(381, 28);
            this.textAbove.TabIndex = 2;
            this.textAbove.Text = "Вставьте ссылку на страницу каталога";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(34, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(397, 35);
            this.textBox1.TabIndex = 1;
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(79, 143);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(287, 40);
            this.Submit.TabIndex = 0;
            this.Submit.Text = "Подтвердить";
            this.Submit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += SubmitButton_Click;
            // 
            // LinkRequestPage
            // 
            this.AcceptButton = this.Submit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 394);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "LinkRequestPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вставьте ссылку";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            _link = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

        private GroupBox groupBox1;
        private Button Submit;
        private TextBox textBox1;
        private string _link;
        public TextBox textAbove;

        public string Link
        {
            get => _link;
        }
    }
}