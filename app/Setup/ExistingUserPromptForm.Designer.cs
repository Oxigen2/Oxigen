namespace Setup
{
    partial class ExistingUserPromptForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExistingUserPromptForm));
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.btnNext = new System.Windows.Forms.Button();
      this.btnBack = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.rbNo = new System.Windows.Forms.RadioButton();
      this.rbYes = new System.Windows.Forms.RadioButton();
      this.label3 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 118);
      this.label1.MaximumSize = new System.Drawing.Size(500, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(490, 39);
      this.label1.TabIndex = 5;
      this.label1.Text = resources.GetString("label1.Text");
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(13, 82);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(84, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "User Account";
      // 
      // btnNext
      // 
      this.btnNext.Enabled = false;
      this.btnNext.Location = new System.Drawing.Point(422, 315);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 2;
      this.btnNext.Text = "Next >";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnBack
      // 
      this.btnBack.Location = new System.Drawing.Point(341, 315);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 3;
      this.btnBack.Text = "< Back";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(260, 315);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // rbNo
      // 
      this.rbNo.AutoSize = true;
      this.rbNo.Location = new System.Drawing.Point(240, 207);
      this.rbNo.Name = "rbNo";
      this.rbNo.Size = new System.Drawing.Size(39, 17);
      this.rbNo.TabIndex = 1;
      this.rbNo.TabStop = true;
      this.rbNo.Text = "No";
      this.rbNo.UseVisualStyleBackColor = true;
      this.rbNo.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
      // 
      // rbYes
      // 
      this.rbYes.AutoSize = true;
      this.rbYes.Location = new System.Drawing.Point(240, 183);
      this.rbYes.Name = "rbYes";
      this.rbYes.Size = new System.Drawing.Size(43, 17);
      this.rbYes.TabIndex = 0;
      this.rbYes.TabStop = true;
      this.rbYes.Text = "Yes";
      this.rbYes.UseVisualStyleBackColor = true;
      this.rbYes.CheckedChanged += new System.EventHandler(this.rbYes_CheckedChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(94, 187);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(125, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Are you an existing user?";
      // 
      // ExistingUserPromptForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(524, 350);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.rbYes);
      this.Controls.Add(this.rbNo);
      this.Name = "ExistingUserPromptForm";
      this.Load += new System.EventHandler(this.ExistingUserPromptForm_Load);
      this.Shown += new System.EventHandler(this.Form_Shown);
      this.Controls.SetChildIndex(this.rbNo, 0);
      this.Controls.SetChildIndex(this.rbYes, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnBack, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbNo;
        private System.Windows.Forms.RadioButton rbYes;
        private System.Windows.Forms.Label label3;
    }
}