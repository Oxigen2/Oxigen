namespace Setup
{
  partial class CredentialsForm
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
      this.label1 = new System.Windows.Forms.Label();
      this.lblSubtitle = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.txtEmailAddress = new System.Windows.Forms.TextBox();
      this.btnBack = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lblProgress = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
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
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(13, 82);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(131, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Login to your account";
      // 
      // lblSubtitle
      // 
      this.lblSubtitle.AutoSize = true;
      this.lblSubtitle.Location = new System.Drawing.Point(12, 118);
      this.lblSubtitle.MaximumSize = new System.Drawing.Size(500, 0);
      this.lblSubtitle.Name = "lblSubtitle";
      this.lblSubtitle.Size = new System.Drawing.Size(262, 13);
      this.lblSubtitle.TabIndex = 6;
      this.lblSubtitle.Text = "Please enter your e-mail address and password below.";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(141, 186);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(72, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Email address";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(160, 213);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(53, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Password";
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(240, 210);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(146, 20);
      this.txtPassword.TabIndex = 1;
      this.txtPassword.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      // 
      // txtEmailAddress
      // 
      this.txtEmailAddress.Location = new System.Drawing.Point(240, 183);
      this.txtEmailAddress.Name = "txtEmailAddress";
      this.txtEmailAddress.Size = new System.Drawing.Size(146, 20);
      this.txtEmailAddress.TabIndex = 0;
      this.txtEmailAddress.TextChanged += new System.EventHandler(this.textBox_TextChanged);
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
      // lblProgress
      // 
      this.lblProgress.AutoSize = true;
      this.lblProgress.Location = new System.Drawing.Point(13, 267);
      this.lblProgress.MaximumSize = new System.Drawing.Size(500, 0);
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(0, 13);
      this.lblProgress.TabIndex = 6;
      // 
      // CredentialsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(524, 350);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblSubtitle);
      this.Controls.Add(this.lblProgress);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.txtEmailAddress);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.txtPassword);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.label4);
      this.Name = "CredentialsForm";
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.txtPassword, 0);
      this.Controls.SetChildIndex(this.btnBack, 0);
      this.Controls.SetChildIndex(this.txtEmailAddress, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.lblSubtitle, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblSubtitle;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtEmailAddress;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label lblProgress;
  }
}
