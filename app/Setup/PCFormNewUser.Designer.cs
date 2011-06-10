namespace Setup
{
  partial class PCFormNewUser
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
      this.lblSubtitle = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.txtPCName = new System.Windows.Forms.TextBox();
      this.lblPCName = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnBack = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.lblMessage = new System.Windows.Forms.Label();
      this.lblProgress = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // lblSubtitle
      // 
      this.lblSubtitle.AutoSize = true;
      this.lblSubtitle.Location = new System.Drawing.Point(12, 118);
      this.lblSubtitle.MaximumSize = new System.Drawing.Size(500, 0);
      this.lblSubtitle.Name = "lblSubtitle";
      this.lblSubtitle.Size = new System.Drawing.Size(385, 13);
      this.lblSubtitle.TabIndex = 26;
      this.lblSubtitle.Tag = "";
      this.lblSubtitle.Text = "You need to link the current PC to Oxigen to download content for your Streams.";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(13, 82);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(137, 13);
      this.label1.TabIndex = 25;
      this.label1.Text = "Link your PC to Oxigen";
      // 
      // txtPCName
      // 
      this.txtPCName.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
      this.txtPCName.Location = new System.Drawing.Point(203, 190);
      this.txtPCName.Name = "txtPCName";
      this.txtPCName.Size = new System.Drawing.Size(226, 20);
      this.txtPCName.TabIndex = 28;
      // 
      // lblPCName
      // 
      this.lblPCName.AutoSize = true;
      this.lblPCName.Location = new System.Drawing.Point(128, 190);
      this.lblPCName.Name = "lblPCName";
      this.lblPCName.Size = new System.Drawing.Size(52, 13);
      this.lblPCName.TabIndex = 27;
      this.lblPCName.Text = "PC Name";
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(260, 315);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 31;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnBack
      // 
      this.btnBack.Location = new System.Drawing.Point(341, 315);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 23);
      this.btnBack.TabIndex = 30;
      this.btnBack.Text = "< Back";
      this.btnBack.UseVisualStyleBackColor = true;
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(422, 315);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 29;
      this.btnNext.Text = "Next";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // lblMessage
      // 
      this.lblMessage.AutoSize = true;
      this.lblMessage.Location = new System.Drawing.Point(12, 147);
      this.lblMessage.MaximumSize = new System.Drawing.Size(500, 0);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(263, 13);
      this.lblMessage.TabIndex = 32;
      this.lblMessage.Tag = "";
      this.lblMessage.Text = "Please create a profile for your PC by entering a name.";
      // 
      // lblProgress
      // 
      this.lblProgress.AutoSize = true;
      this.lblProgress.Location = new System.Drawing.Point(12, 280);
      this.lblProgress.MaximumSize = new System.Drawing.Size(500, 0);
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(0, 13);
      this.lblProgress.TabIndex = 32;
      this.lblProgress.Tag = "";
      // 
      // PCFormNewUser
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(524, 350);
      this.Controls.Add(this.lblProgress);
      this.Controls.Add(this.lblMessage);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.txtPCName);
      this.Controls.Add(this.lblPCName);
      this.Controls.Add(this.lblSubtitle);
      this.Controls.Add(this.label1);
      this.Name = "PCFormNewUser";
      this.Load += new System.EventHandler(this.PCFormNewUser_Load);
      this.Shown += new System.EventHandler(this.Form_Shown);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.lblSubtitle, 0);
      this.Controls.SetChildIndex(this.lblPCName, 0);
      this.Controls.SetChildIndex(this.txtPCName, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.btnBack, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.lblMessage, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblSubtitle;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPCName;
    private System.Windows.Forms.Label lblPCName;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Label lblMessage;
    private System.Windows.Forms.Label lblProgress;
  }
}