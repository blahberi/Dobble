namespace Dobble.Client.Forms.Views
{
	partial class LobbyView
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
            this.InviteList = new Dobble.Client.Forms.GruvboxListBox();
            this.InviteButton = new Dobble.Client.Forms.GruvboxButton();
            this.InviteText = new Dobble.Client.Forms.GruvboxTextBox();
            this.AcceptButton = new Dobble.Client.Forms.GruvboxButton();
            this.RejectButton = new Dobble.Client.Forms.GruvboxButton();
            this.gruvboxLabel1 = new Dobble.Client.Forms.GruvboxLabel();
            this.UsernameLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.SignOutButton = new Dobble.Client.Forms.GruvboxButton();
            this.SuspendLayout();
            // 
            // InviteList
            // 
            this.InviteList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(219)))), ((int)(((byte)(178)))));
            this.InviteList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InviteList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.InviteList.FormattingEnabled = true;
            this.InviteList.Location = new System.Drawing.Point(3, 3);
            this.InviteList.Name = "InviteList";
            this.InviteList.Size = new System.Drawing.Size(425, 145);
            this.InviteList.TabIndex = 1;
            this.InviteList.SelectedIndexChanged += new System.EventHandler(this.InviteList_SelectedIndexChanged);
            // 
            // InviteButton
            // 
            this.InviteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(134)))), ((int)(((byte)(155)))));
            this.InviteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InviteButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.InviteButton.Location = new System.Drawing.Point(353, 154);
            this.InviteButton.Name = "InviteButton";
            this.InviteButton.Size = new System.Drawing.Size(75, 23);
            this.InviteButton.TabIndex = 2;
            this.InviteButton.Text = "Invite";
            this.InviteButton.UseVisualStyleBackColor = true;
            this.InviteButton.Click += new System.EventHandler(this.InviteButton_Click);
            // 
            // InviteText
            // 
            this.InviteText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(219)))), ((int)(((byte)(178)))));
            this.InviteText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InviteText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.InviteText.Location = new System.Drawing.Point(3, 156);
            this.InviteText.Name = "InviteText";
            this.InviteText.Size = new System.Drawing.Size(346, 20);
            this.InviteText.TabIndex = 3;
            // 
            // AcceptButton
            // 
            this.AcceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(134)))), ((int)(((byte)(155)))));
            this.AcceptButton.Enabled = false;
            this.AcceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AcceptButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.AcceptButton.Location = new System.Drawing.Point(435, 4);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptButton.TabIndex = 4;
            this.AcceptButton.Text = "Accept";
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // RejectButton
            // 
            this.RejectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(134)))), ((int)(((byte)(155)))));
            this.RejectButton.Enabled = false;
            this.RejectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RejectButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.RejectButton.Location = new System.Drawing.Point(435, 33);
            this.RejectButton.Name = "RejectButton";
            this.RejectButton.Size = new System.Drawing.Size(75, 23);
            this.RejectButton.TabIndex = 5;
            this.RejectButton.Text = "Reject";
            this.RejectButton.UseVisualStyleBackColor = true;
            this.RejectButton.Click += new System.EventHandler(this.RejectButton_Click);
            // 
            // gruvboxLabel1
            // 
            this.gruvboxLabel1.AutoSize = true;
            this.gruvboxLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.gruvboxLabel1.Location = new System.Drawing.Point(434, 59);
            this.gruvboxLabel1.Name = "gruvboxLabel1";
            this.gruvboxLabel1.Size = new System.Drawing.Size(68, 13);
            this.gruvboxLabel1.TabIndex = 6;
            this.gruvboxLabel1.Text = "Signed in as:";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.UsernameLabel.Location = new System.Drawing.Point(434, 72);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(53, 13);
            this.UsernameLabel.TabIndex = 7;
            this.UsernameLabel.Text = "username";
            // 
            // SignOutButton
            // 
            this.SignOutButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(73)))), ((int)(((byte)(52)))));
            this.SignOutButton.FlatAppearance.BorderSize = 0;
            this.SignOutButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.SignOutButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(219)))), ((int)(((byte)(178)))));
            this.SignOutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignOutButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.SignOutButton.Location = new System.Drawing.Point(435, 88);
            this.SignOutButton.Name = "SignOutButton";
            this.SignOutButton.Size = new System.Drawing.Size(75, 23);
            this.SignOutButton.TabIndex = 8;
            this.SignOutButton.Text = "Sign Out";
            this.SignOutButton.UseVisualStyleBackColor = false;
            this.SignOutButton.Click += new System.EventHandler(this.SignOutButton_Click);
            // 
            // LobbyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.gruvboxLabel1);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.SignOutButton);
            this.Controls.Add(this.RejectButton);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.InviteText);
            this.Controls.Add(this.InviteButton);
            this.Controls.Add(this.InviteList);
            this.Name = "LobbyView";
            this.Size = new System.Drawing.Size(518, 230);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private GruvboxListBox InviteList;
		private GruvboxButton InviteButton;
		private GruvboxTextBox InviteText;
		private GruvboxButton AcceptButton;
		private GruvboxButton RejectButton;
		private GruvboxLabel gruvboxLabel1;
		private GruvboxLabel UsernameLabel;
		private GruvboxButton SignOutButton;
	}
}
