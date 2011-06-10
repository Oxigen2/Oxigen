namespace Setup
{
  partial class TermsAndConditionsForm
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
      this.btnNext = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnBack = new System.Windows.Forms.Button();
      this.rbNotAgree = new System.Windows.Forms.RadioButton();
      this.rbAgree = new System.Windows.Forms.RadioButton();
      this.label1 = new System.Windows.Forms.Label();
      this.laBox = new System.Windows.Forms.RichTextBox();
      this.label2 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // btnNext
      // 
      this.btnNext.Enabled = false;
      this.btnNext.Location = new System.Drawing.Point(437, 352);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 4;
      this.btnNext.Text = "Next >";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(275, 352);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnBack
      // 
      this.btnBack.Location = new System.Drawing.Point(356, 352);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 3;
      this.btnBack.Text = "< Back";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // rbNotAgree
      // 
      this.rbNotAgree.AutoSize = true;
      this.rbNotAgree.Checked = true;
      this.rbNotAgree.Location = new System.Drawing.Point(12, 352);
      this.rbNotAgree.Name = "rbNotAgree";
      this.rbNotAgree.Size = new System.Drawing.Size(96, 17);
      this.rbNotAgree.TabIndex = 0;
      this.rbNotAgree.TabStop = true;
      this.rbNotAgree.Text = "I Do Not Agree";
      this.rbNotAgree.UseVisualStyleBackColor = true;
      this.rbNotAgree.CheckedChanged += new System.EventHandler(this.rbNotAgree_CheckedChanged);
      // 
      // rbAgree
      // 
      this.rbAgree.AutoSize = true;
      this.rbAgree.Location = new System.Drawing.Point(132, 352);
      this.rbAgree.Name = "rbAgree";
      this.rbAgree.Size = new System.Drawing.Size(59, 17);
      this.rbAgree.TabIndex = 1;
      this.rbAgree.Text = "I Agree";
      this.rbAgree.UseVisualStyleBackColor = true;
      this.rbAgree.CheckedChanged += new System.EventHandler(this.rbAgree_CheckedChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 118);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(395, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "You need to agree to the Terms of Service in order to proceed with the installati" +
    "on.";
      // 
      // laBox
      // 
      this.laBox.Location = new System.Drawing.Point(15, 148);
      this.laBox.Name = "laBox";
      this.laBox.ReadOnly = true;
      this.laBox.Size = new System.Drawing.Size(497, 176);
      this.laBox.TabIndex = 7;
      this.laBox.Text = "";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(13, 82);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(103, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Terms of Service";
      // 
      // TermsAndConditionsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(524, 387);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.laBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.rbAgree);
      this.Controls.Add(this.rbNotAgree);
      this.Name = "TermsAndConditionsForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Auto;
      this.Shown += new System.EventHandler(this.Form_Shown);
      this.Controls.SetChildIndex(this.rbNotAgree, 0);
      this.Controls.SetChildIndex(this.rbAgree, 0);
      this.Controls.SetChildIndex(this.btnBack, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.laBox, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.RadioButton rbNotAgree;
    private System.Windows.Forms.RadioButton rbAgree;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.RichTextBox laBox;
    private System.Windows.Forms.Label label2;
  }
}
