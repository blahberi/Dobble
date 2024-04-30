namespace Dobble.Client.Forms.Views
{
	partial class LoginView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.RegisterLabel = new GruvboxLink();
            this.PasswordText = new GruvboxTextBox();
            this.PasswordLabel = new GruvboxLabel();
            this.UserNameText = new GruvboxTextBox();
            this.UserNameLabel = new GruvboxLabel();
            this.LoginButton = new GruvboxButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.RegisterLabel);
            this.panel1.Controls.Add(this.PasswordText);
            this.panel1.Controls.Add(this.PasswordLabel);
            this.panel1.Controls.Add(this.UserNameText);
            this.panel1.Controls.Add(this.UserNameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 105);
            this.panel1.TabIndex = 0;
            // 
            // RegisterLabel
            // 
            this.RegisterLabel.AutoSize = true;
            this.RegisterLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RegisterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.RegisterLabel.Location = new System.Drawing.Point(0, 88);
            this.RegisterLabel.Name = "RegisterLabel";
            this.RegisterLabel.Size = new System.Drawing.Size(158, 17);
            this.RegisterLabel.TabIndex = 4;
            this.RegisterLabel.TabStop = true;
            this.RegisterLabel.Text = "Don\'t have an account?";
            this.RegisterLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RegisterLabel_LinkClicked);
            // 
            // PasswordText
            // 
            this.PasswordText.Dock = System.Windows.Forms.DockStyle.Top;
            this.PasswordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.PasswordText.Location = new System.Drawing.Point(0, 64);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.Size = new System.Drawing.Size(300, 24);
            this.PasswordText.TabIndex = 3;
            this.PasswordText.UseSystemPasswordChar = true;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PasswordLabel.Location = new System.Drawing.Point(0, 44);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(78, 20);
            this.PasswordLabel.TabIndex = 2;
            this.PasswordLabel.Text = "Password";
            // 
            // UserNameText
            // 
            this.UserNameText.Dock = System.Windows.Forms.DockStyle.Top;
            this.UserNameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.UserNameText.Location = new System.Drawing.Point(0, 20);
            this.UserNameText.Name = "UserNameText";
            this.UserNameText.Size = new System.Drawing.Size(300, 24);
            this.UserNameText.TabIndex = 1;
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.UserNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.UserNameLabel.Location = new System.Drawing.Point(0, 0);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(83, 20);
            this.UserNameLabel.TabIndex = 0;
            this.UserNameLabel.Text = "Username";
            // 
            // LoginButton
            // 
            this.LoginButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LoginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LoginButton.Location = new System.Drawing.Point(0, 115);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(300, 35);
            this.LoginButton.TabIndex = 1;
            this.LoginButton.Text = "Log In";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.LoginButton);
            this.Name = "LoginView";
            this.Size = new System.Drawing.Size(300, 150);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox PasswordText;
		private System.Windows.Forms.Label PasswordLabel;
		private System.Windows.Forms.TextBox UserNameText;
		private System.Windows.Forms.Label UserNameLabel;
		private System.Windows.Forms.Button LoginButton;
		private System.Windows.Forms.LinkLabel RegisterLabel;
	}
}
