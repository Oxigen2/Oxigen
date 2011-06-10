namespace Setup
{
  partial class RegistrationForm3
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
      this.label4 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnBack = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.ddTownCity = new System.Windows.Forms.ComboBox();
      this.ddCountry = new System.Windows.Forms.ComboBox();
      this.ddState = new System.Windows.Forms.ComboBox();
      this.lblState = new System.Windows.Forms.Label();
      this.lblSubtitle = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Size = new System.Drawing.Size(528, 61);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(463, 79);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(34, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "3 of 4";
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
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(422, 315);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 3;
      this.btnNext.Text = "Next >";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(124, 241);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(96, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "Nearest Town/City";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(177, 186);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(43, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Country";
      // 
      // ddTownCity
      // 
      this.ddTownCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ddTownCity.FormattingEnabled = true;
      this.ddTownCity.Location = new System.Drawing.Point(240, 238);
      this.ddTownCity.Name = "ddTownCity";
      this.ddTownCity.Size = new System.Drawing.Size(204, 21);
      this.ddTownCity.TabIndex = 2;
      // 
      // ddCountry
      // 
      this.ddCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ddCountry.FormattingEnabled = true;
      this.ddCountry.Location = new System.Drawing.Point(240, 184);
      this.ddCountry.Name = "ddCountry";
      this.ddCountry.Size = new System.Drawing.Size(204, 21);
      this.ddCountry.TabIndex = 0;
      this.ddCountry.SelectedIndexChanged += new System.EventHandler(this.ddCountry_SelectedIndexChanged);
      // 
      // ddState
      // 
      this.ddState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ddState.Enabled = false;
      this.ddState.FormattingEnabled = true;
      this.ddState.Location = new System.Drawing.Point(240, 211);
      this.ddState.Name = "ddState";
      this.ddState.Size = new System.Drawing.Size(204, 21);
      this.ddState.TabIndex = 1;
      this.ddState.SelectedIndexChanged += new System.EventHandler(this.ddState_SelectedIndexChanged);
      // 
      // lblState
      // 
      this.lblState.AutoSize = true;
      this.lblState.Enabled = false;
      this.lblState.Location = new System.Drawing.Point(188, 214);
      this.lblState.Name = "lblState";
      this.lblState.Size = new System.Drawing.Size(32, 13);
      this.lblState.TabIndex = 9;
      this.lblState.Text = "State";
      // 
      // lblSubtitle
      // 
      this.lblSubtitle.AutoSize = true;
      this.lblSubtitle.Location = new System.Drawing.Point(12, 118);
      this.lblSubtitle.Name = "lblSubtitle";
      this.lblSubtitle.Size = new System.Drawing.Size(132, 13);
      this.lblSubtitle.TabIndex = 12;
      this.lblSubtitle.Tag = "";
      this.lblSubtitle.Text = "Please enter your location.";
      // 
      // RegistrationForm3
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(524, 350);
      this.Controls.Add(this.lblSubtitle);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.lblState);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.ddTownCity);
      this.Controls.Add(this.ddState);
      this.Controls.Add(this.ddCountry);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label1);
      this.Name = "RegistrationForm3";
      this.Shown += new System.EventHandler(this.Form_Shown);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.ddCountry, 0);
      this.Controls.SetChildIndex(this.ddState, 0);
      this.Controls.SetChildIndex(this.ddTownCity, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.lblState, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.btnBack, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.lblSubtitle, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox ddTownCity;
    private System.Windows.Forms.ComboBox ddCountry;
    private System.Windows.Forms.ComboBox ddState;
    private System.Windows.Forms.Label lblState;
    private System.Windows.Forms.Label lblSubtitle;
  }
}
