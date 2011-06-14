namespace Setup
{
  partial class RegistrationForm1
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationForm1));
      this.label1 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.btnNext = new System.Windows.Forms.Button();
      this.btnBack = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.txtPassword2 = new System.Windows.Forms.TextBox();
      this.txtPassword1 = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtEmailAddress = new System.Windows.Forms.TextBox();
      this.lblSubtitle = new System.Windows.Forms.Label();
      this.lblProgress = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Size = new System.Drawing.Size(526, 60);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(13, 82);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(127, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "New user registration";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(463, 81);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(34, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "1 of 4";
      // 
      // btnNext
      // 
      this.btnNext.Enabled = false;
      this.btnNext.Location = new System.Drawing.Point(422, 315);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 3;
      this.btnNext.Text = "Next >";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnBack
      // 
      this.btnBack.Location = new System.Drawing.Point(341, 315);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 4;
      this.btnBack.Text = "< Back";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(260, 315);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // txtPassword2
      // 
      this.txtPassword2.Location = new System.Drawing.Point(240, 237);
      this.txtPassword2.Name = "txtPassword2";
      this.txtPassword2.PasswordChar = '*';
      this.txtPassword2.Size = new System.Drawing.Size(204, 20);
      this.txtPassword2.TabIndex = 2;
      this.txtPassword2.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      // 
      // txtPassword1
      // 
      this.txtPassword1.Location = new System.Drawing.Point(240, 211);
      this.txtPassword1.Name = "txtPassword1";
      this.txtPassword1.PasswordChar = '*';
      this.txtPassword1.Size = new System.Drawing.Size(204, 20);
      this.txtPassword1.TabIndex = 1;
      this.txtPassword1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(166, 214);
      this.label5.MaximumSize = new System.Drawing.Size(170, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(53, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Password";
      this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(128, 240);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(91, 13);
      this.label6.TabIndex = 10;
      this.label6.Text = "Confirm Password";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(144, 188);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(75, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "E-mail address";
      // 
      // txtEmailAddress
      // 
      this.txtEmailAddress.Location = new System.Drawing.Point(240, 185);
      this.txtEmailAddress.Name = "txtEmailAddress";
      this.txtEmailAddress.Size = new System.Drawing.Size(204, 20);
      this.txtEmailAddress.TabIndex = 0;
      this.txtEmailAddress.TextChanged += new System.EventHandler(this.textBox_TextChanged);
      // 
      // lblSubtitle
      // 
      this.lblSubtitle.AutoSize = true;
      this.lblSubtitle.Location = new System.Drawing.Point(12, 118);
      this.lblSubtitle.Name = "lblSubtitle";
      this.lblSubtitle.Size = new System.Drawing.Size(378, 13);
      this.lblSubtitle.TabIndex = 13;
      this.lblSubtitle.Tag = "";
      this.lblSubtitle.Text = "Please enter your e-mail address and a password to create an Oxigen account.";
      // 
      // lblProgress
      // 
      this.lblProgress.AutoSize = true;
      this.lblProgress.Location = new System.Drawing.Point(13, 267);
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(0, 13);
      this.lblProgress.TabIndex = 13;
      this.lblProgress.Tag = "";
      // 
      // RegistrationForm1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(524, 350);
      this.Controls.Add(this.lblProgress);
      this.Controls.Add(this.lblSubtitle);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtEmailAddress);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.txtPassword1);
      this.Controls.Add(this.txtPassword2);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Name = "RegistrationForm1";
      this.Shown += new System.EventHandler(this.Form_Shown);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.label6, 0);
      this.Controls.SetChildIndex(this.btnBack, 0);
      this.Controls.SetChildIndex(this.txtPassword2, 0);
      this.Controls.SetChildIndex(this.txtPassword1, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.txtEmailAddress, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      this.Controls.SetChildIndex(this.lblSubtitle, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txtPassword2;
    private System.Windows.Forms.TextBox txtPassword1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtEmailAddress;
    private System.Windows.Forms.Label lblSubtitle;
    private System.Windows.Forms.Label lblProgress;
  }
}
