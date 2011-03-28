namespace Setup
{
  partial class OxigenExistsForm
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
      this.lblInstructions = new System.Windows.Forms.Label();
      this.rbRepair = new System.Windows.Forms.RadioButton();
      this.rbUninstall = new System.Windows.Forms.RadioButton();
      this.rbAddNewStreams = new System.Windows.Forms.RadioButton();
      this.btnNext = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(13, 82);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(105, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "Existing Software";
      // 
      // lblInstructions
      // 
      this.lblInstructions.AutoSize = true;
      this.lblInstructions.Location = new System.Drawing.Point(12, 118);
      this.lblInstructions.MaximumSize = new System.Drawing.Size(500, 0);
      this.lblInstructions.Name = "lblInstructions";
      this.lblInstructions.Size = new System.Drawing.Size(492, 26);
      this.lblInstructions.TabIndex = 7;
      this.lblInstructions.Text = "Oxigen already exists on your computer. You can either uninstall the existing ins" +
          "tallation or add the new Streams you have subscribed to.";
      // 
      // rbRepair
      // 
      this.rbRepair.AutoSize = true;
      this.rbRepair.Location = new System.Drawing.Point(15, 162);
      this.rbRepair.Name = "rbRepair";
      this.rbRepair.Size = new System.Drawing.Size(56, 17);
      this.rbRepair.TabIndex = 0;
      this.rbRepair.TabStop = true;
      this.rbRepair.Text = "Repair";
      this.rbRepair.UseVisualStyleBackColor = true;
      this.rbRepair.Visible = false;
      this.rbRepair.CheckedChanged += new System.EventHandler(this.rbRepair_CheckedChanged);
      // 
      // rbUninstall
      // 
      this.rbUninstall.AutoSize = true;
      this.rbUninstall.Location = new System.Drawing.Point(15, 195);
      this.rbUninstall.Name = "rbUninstall";
      this.rbUninstall.Size = new System.Drawing.Size(65, 17);
      this.rbUninstall.TabIndex = 1;
      this.rbUninstall.TabStop = true;
      this.rbUninstall.Text = "Uninstall";
      this.rbUninstall.UseVisualStyleBackColor = true;
      this.rbUninstall.CheckedChanged += new System.EventHandler(this.rbUninstall_CheckedChanged);
      // 
      // rbAddNewStreams
      // 
      this.rbAddNewStreams.AutoSize = true;
      this.rbAddNewStreams.Location = new System.Drawing.Point(15, 229);
      this.rbAddNewStreams.Name = "rbAddNewStreams";
      this.rbAddNewStreams.Size = new System.Drawing.Size(108, 17);
      this.rbAddNewStreams.TabIndex = 2;
      this.rbAddNewStreams.TabStop = true;
      this.rbAddNewStreams.Text = "Add new Streams";
      this.rbAddNewStreams.UseVisualStyleBackColor = true;
      this.rbAddNewStreams.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
      // 
      // btnNext
      // 
      this.btnNext.Enabled = false;
      this.btnNext.Location = new System.Drawing.Point(437, 315);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 3;
      this.btnNext.Text = "Next >";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(356, 315);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // OxigenExistsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(524, 350);
      this.Controls.Add(this.rbRepair);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.lblInstructions);
      this.Controls.Add(this.rbUninstall);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.rbAddNewStreams);
      this.Name = "OxigenExistsForm";
      this.Controls.SetChildIndex(this.rbAddNewStreams, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.rbUninstall, 0);
      this.Controls.SetChildIndex(this.lblInstructions, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.rbRepair, 0);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblInstructions;
    private System.Windows.Forms.RadioButton rbRepair;
    private System.Windows.Forms.RadioButton rbUninstall;
    private System.Windows.Forms.RadioButton rbAddNewStreams;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnCancel;
  }
}
