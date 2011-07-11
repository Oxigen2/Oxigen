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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmailAddressNew = new System.Windows.Forms.TextBox();
            this.txtPassword1New = new System.Windows.Forms.TextBox();
            this.txtPassword2New = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.newUser = new System.Windows.Forms.Panel();
            this.existingUser = new System.Windows.Forms.Panel();
            this.txtEmailAddressExisting = new System.Windows.Forms.TextBox();
            this.txtPasswordExisting = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rbYes = new System.Windows.Forms.RadioButton();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.lblProgress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.newUser.SuspendLayout();
            this.existingUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 118);
            this.label1.MaximumSize = new System.Drawing.Size(420, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(420, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Oxigen allows you to register multiple computers to a single account (e.g. your h" +
    "ome PC and your work PC).";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Are you an existing user?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Email address";
            // 
            // txtEmailAddressNew
            // 
            this.txtEmailAddressNew.Location = new System.Drawing.Point(122, 30);
            this.txtEmailAddressNew.Name = "txtEmailAddressNew";
            this.txtEmailAddressNew.Size = new System.Drawing.Size(204, 20);
            this.txtEmailAddressNew.TabIndex = 11;
            // 
            // txtPassword1New
            // 
            this.txtPassword1New.Location = new System.Drawing.Point(122, 56);
            this.txtPassword1New.Name = "txtPassword1New";
            this.txtPassword1New.PasswordChar = '*';
            this.txtPassword1New.Size = new System.Drawing.Size(204, 20);
            this.txtPassword1New.TabIndex = 12;
            // 
            // txtPassword2New
            // 
            this.txtPassword2New.Location = new System.Drawing.Point(122, 82);
            this.txtPassword2New.Name = "txtPassword2New";
            this.txtPassword2New.PasswordChar = '*';
            this.txtPassword2New.Size = new System.Drawing.Size(204, 20);
            this.txtPassword2New.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Confirm Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 59);
            this.label5.MaximumSize = new System.Drawing.Size(170, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Password";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(10, 5);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(378, 13);
            this.lblSubtitle.TabIndex = 17;
            this.lblSubtitle.Tag = "";
            this.lblSubtitle.Text = "Please enter your e-mail address and a password to create an Oxigen account.";
            // 
            // newUser
            // 
            this.newUser.Controls.Add(this.label4);
            this.newUser.Controls.Add(this.label5);
            this.newUser.Controls.Add(this.lblSubtitle);
            this.newUser.Controls.Add(this.label6);
            this.newUser.Controls.Add(this.txtEmailAddressNew);
            this.newUser.Controls.Add(this.txtPassword2New);
            this.newUser.Controls.Add(this.txtPassword1New);
            this.newUser.Location = new System.Drawing.Point(12, 194);
            this.newUser.Name = "newUser";
            this.newUser.Size = new System.Drawing.Size(391, 115);
            this.newUser.TabIndex = 18;
            this.newUser.Visible = false;
            // 
            // existingUser
            // 
            this.existingUser.Controls.Add(this.txtEmailAddressExisting);
            this.existingUser.Controls.Add(this.txtPasswordExisting);
            this.existingUser.Controls.Add(this.label8);
            this.existingUser.Controls.Add(this.label9);
            this.existingUser.Controls.Add(this.label7);
            this.existingUser.Location = new System.Drawing.Point(16, 184);
            this.existingUser.Name = "existingUser";
            this.existingUser.Size = new System.Drawing.Size(351, 92);
            this.existingUser.TabIndex = 19;
            this.existingUser.Visible = false;
            // 
            // txtEmailAddressExisting
            // 
            this.txtEmailAddressExisting.Location = new System.Drawing.Point(118, 40);
            this.txtEmailAddressExisting.Name = "txtEmailAddressExisting";
            this.txtEmailAddressExisting.Size = new System.Drawing.Size(204, 20);
            this.txtEmailAddressExisting.TabIndex = 9;
            // 
            // txtPasswordExisting
            // 
            this.txtPasswordExisting.Location = new System.Drawing.Point(118, 66);
            this.txtPasswordExisting.Name = "txtPasswordExisting";
            this.txtPasswordExisting.PasswordChar = '*';
            this.txtPasswordExisting.Size = new System.Drawing.Size(204, 20);
            this.txtPasswordExisting.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Email address";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Password";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 15);
            this.label7.MaximumSize = new System.Drawing.Size(500, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Please enter your e-mail address and password below.";
            // 
            // rbYes
            // 
            this.rbYes.AutoSize = true;
            this.rbYes.Location = new System.Drawing.Point(157, 161);
            this.rbYes.Name = "rbYes";
            this.rbYes.Size = new System.Drawing.Size(43, 17);
            this.rbYes.TabIndex = 19;
            this.rbYes.TabStop = true;
            this.rbYes.Text = "Yes";
            this.rbYes.UseVisualStyleBackColor = true;
            this.rbYes.CheckedChanged += new System.EventHandler(this.rbYes_CheckedChanged);
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Location = new System.Drawing.Point(206, 161);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(39, 17);
            this.rbNo.TabIndex = 20;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "No";
            this.rbNo.UseVisualStyleBackColor = true;
            this.rbNo.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(22, 315);
            this.lblProgress.MaximumSize = new System.Drawing.Size(500, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 13);
            this.lblProgress.TabIndex = 21;
            // 
            // ExistingUserPromptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 350);
            this.Controls.Add(this.newUser);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.existingUser);
            this.Controls.Add(this.rbNo);
            this.Controls.Add(this.rbYes);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Name = "ExistingUserPromptForm";
            this.Load += new System.EventHandler(this.ExistingUserPromptForm_Load);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.rbYes, 0);
            this.Controls.SetChildIndex(this.rbNo, 0);
            this.Controls.SetChildIndex(this.existingUser, 0);
            this.Controls.SetChildIndex(this.lblProgress, 0);
            this.Controls.SetChildIndex(this.newUser, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.newUser.ResumeLayout(false);
            this.newUser.PerformLayout();
            this.existingUser.ResumeLayout(false);
            this.existingUser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmailAddressNew;
        private System.Windows.Forms.TextBox txtPassword1New;
        private System.Windows.Forms.TextBox txtPassword2New;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel newUser;
        private System.Windows.Forms.Panel existingUser;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEmailAddressExisting;
        private System.Windows.Forms.TextBox txtPasswordExisting;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbYes;
        private System.Windows.Forms.RadioButton rbNo;
        private System.Windows.Forms.Label lblProgress;
    }
}