namespace Setup
{
  partial class RegistrationForm2
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
      this.label4 = new System.Windows.Forms.Label();
      this.btnNext = new System.Windows.Forms.Button();
      this.btnBack = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.ddYear = new System.Windows.Forms.ComboBox();
      this.ddMonth = new System.Windows.Forms.ComboBox();
      this.ddDay = new System.Windows.Forms.ComboBox();
      this.lblSubtitle = new System.Windows.Forms.Label();
      this.rbMale = new System.Windows.Forms.RadioButton();
      this.rbFemale = new System.Windows.Forms.RadioButton();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.txtFirstName = new System.Windows.Forms.TextBox();
      this.txtLastName = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
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
      this.label4.Text = "2 of 4";
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(422, 315);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 7;
      this.btnNext.Text = "Next >";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnBack
      // 
      this.btnBack.Location = new System.Drawing.Point(341, 315);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 8;
      this.btnBack.Text = "< Back";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(260, 315);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 9;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(150, 265);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(68, 13);
      this.label3.TabIndex = 12;
      this.label3.Text = "Date Of Birth";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(176, 229);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(42, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Gender";
      // 
      // ddYear
      // 
      this.ddYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ddYear.FormattingEnabled = true;
      this.ddYear.Location = new System.Drawing.Point(389, 262);
      this.ddYear.Name = "ddYear";
      this.ddYear.Size = new System.Drawing.Size(54, 21);
      this.ddYear.TabIndex = 6;
      this.ddYear.Click += new System.EventHandler(this.ddYear_Click);
      // 
      // ddMonth
      // 
      this.ddMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ddMonth.FormattingEnabled = true;
      this.ddMonth.Location = new System.Drawing.Point(292, 262);
      this.ddMonth.Name = "ddMonth";
      this.ddMonth.Size = new System.Drawing.Size(91, 21);
      this.ddMonth.TabIndex = 5;
      // 
      // ddDay
      // 
      this.ddDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ddDay.FormattingEnabled = true;
      this.ddDay.Location = new System.Drawing.Point(238, 262);
      this.ddDay.Name = "ddDay";
      this.ddDay.Size = new System.Drawing.Size(48, 21);
      this.ddDay.TabIndex = 4;
      // 
      // lblSubtitle
      // 
      this.lblSubtitle.AutoSize = true;
      this.lblSubtitle.Location = new System.Drawing.Point(12, 118);
      this.lblSubtitle.Name = "lblSubtitle";
      this.lblSubtitle.Size = new System.Drawing.Size(125, 13);
      this.lblSubtitle.TabIndex = 8;
      this.lblSubtitle.Tag = "";
      this.lblSubtitle.Text = "Please enter your details.";
      // 
      // rbMale
      // 
      this.rbMale.AutoSize = true;
      this.rbMale.Location = new System.Drawing.Point(238, 229);
      this.rbMale.Name = "rbMale";
      this.rbMale.Size = new System.Drawing.Size(48, 17);
      this.rbMale.TabIndex = 2;
      this.rbMale.TabStop = true;
      this.rbMale.Text = "Male";
      this.rbMale.UseVisualStyleBackColor = true;
      // 
      // rbFemale
      // 
      this.rbFemale.AutoSize = true;
      this.rbFemale.Location = new System.Drawing.Point(298, 229);
      this.rbFemale.Name = "rbFemale";
      this.rbFemale.Size = new System.Drawing.Size(59, 17);
      this.rbFemale.TabIndex = 3;
      this.rbFemale.TabStop = true;
      this.rbFemale.Text = "Female";
      this.rbFemale.UseVisualStyleBackColor = true;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(159, 200);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(58, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Last Name";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(160, 166);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(57, 13);
      this.label6.TabIndex = 9;
      this.label6.Text = "First Name";
      // 
      // txtFirstName
      // 
      this.txtFirstName.Location = new System.Drawing.Point(238, 163);
      this.txtFirstName.Name = "txtFirstName";
      this.txtFirstName.Size = new System.Drawing.Size(205, 20);
      this.txtFirstName.TabIndex = 0;
      // 
      // txtLastName
      // 
      this.txtLastName.Location = new System.Drawing.Point(238, 197);
      this.txtLastName.Name = "txtLastName";
      this.txtLastName.Size = new System.Drawing.Size(205, 20);
      this.txtLastName.TabIndex = 1;
      // 
      // RegistrationForm2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(524, 350);
      this.Controls.Add(this.txtLastName);
      this.Controls.Add(this.txtFirstName);
      this.Controls.Add(this.rbFemale);
      this.Controls.Add(this.rbMale);
      this.Controls.Add(this.lblSubtitle);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.ddYear);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.ddMonth);
      this.Controls.Add(this.ddDay);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label3);
      this.Name = "RegistrationForm2";
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.label6, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.ddDay, 0);
      this.Controls.SetChildIndex(this.ddMonth, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnBack, 0);
      this.Controls.SetChildIndex(this.ddYear, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      this.Controls.SetChildIndex(this.lblSubtitle, 0);
      this.Controls.SetChildIndex(this.rbMale, 0);
      this.Controls.SetChildIndex(this.rbFemale, 0);
      this.Controls.SetChildIndex(this.txtFirstName, 0);
      this.Controls.SetChildIndex(this.txtLastName, 0);
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
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox ddYear;
    private System.Windows.Forms.ComboBox ddMonth;
    private System.Windows.Forms.ComboBox ddDay;
    private System.Windows.Forms.Label lblSubtitle;
    private System.Windows.Forms.RadioButton rbMale;
    private System.Windows.Forms.RadioButton rbFemale;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtFirstName;
    private System.Windows.Forms.TextBox txtLastName;
  }
}
