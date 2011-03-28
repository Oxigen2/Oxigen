namespace LibraryTester
{
  partial class Starter
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
      this.btnSerializerDeserializer = new System.Windows.Forms.Button();
      this.btnFilePermitChecker = new System.Windows.Forms.Button();
      this.btnEncryptBatch = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnSerializerDeserializer
      // 
      this.btnSerializerDeserializer.Location = new System.Drawing.Point(8, 41);
      this.btnSerializerDeserializer.Name = "btnSerializerDeserializer";
      this.btnSerializerDeserializer.Size = new System.Drawing.Size(139, 23);
      this.btnSerializerDeserializer.TabIndex = 2;
      this.btnSerializerDeserializer.Text = "Serializer / Deserializer";
      this.btnSerializerDeserializer.UseVisualStyleBackColor = true;
      this.btnSerializerDeserializer.Click += new System.EventHandler(this.btnSerializerDeserializer_Click);
      // 
      // btnFilePermitChecker
      // 
      this.btnFilePermitChecker.Location = new System.Drawing.Point(9, 70);
      this.btnFilePermitChecker.Name = "btnFilePermitChecker";
      this.btnFilePermitChecker.Size = new System.Drawing.Size(138, 23);
      this.btnFilePermitChecker.TabIndex = 9;
      this.btnFilePermitChecker.Text = "File Permission Checker";
      this.btnFilePermitChecker.UseVisualStyleBackColor = true;
      this.btnFilePermitChecker.Click += new System.EventHandler(this.btnFilePermitChecker_Click);
      // 
      // btnEncryptBatch
      // 
      this.btnEncryptBatch.Location = new System.Drawing.Point(8, 99);
      this.btnEncryptBatch.Name = "btnEncryptBatch";
      this.btnEncryptBatch.Size = new System.Drawing.Size(139, 23);
      this.btnEncryptBatch.TabIndex = 10;
      this.btnEncryptBatch.Text = "Encrypt Batch";
      this.btnEncryptBatch.UseVisualStyleBackColor = true;
      this.btnEncryptBatch.Click += new System.EventHandler(this.btnEncryptBatch_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(8, 128);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(137, 23);
      this.button1.TabIndex = 15;
      this.button1.Text = "File Rights Check";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(8, 12);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(139, 23);
      this.button2.TabIndex = 16;
      this.button2.Text = "View Encrypted Text Files";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // Starter
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(157, 163);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.btnEncryptBatch);
      this.Controls.Add(this.btnFilePermitChecker);
      this.Controls.Add(this.btnSerializerDeserializer);
      this.Name = "Starter";
      this.Text = "Starter";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnSerializerDeserializer;
    private System.Windows.Forms.Button btnFilePermitChecker;
    private System.Windows.Forms.Button btnEncryptBatch;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
  }
}