namespace Dobble.Client.Forms
{
	partial class ConnectView
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
            this.IPLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.IPText = new Dobble.Client.Forms.GruvboxTextBox();
            this.PortText = new Dobble.Client.Forms.GruvboxTextBox();
            this.PortLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.ConnectButton = new Dobble.Client.Forms.GruvboxButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.IPLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.IPLabel.Location = new System.Drawing.Point(0, 0);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(24, 20);
            this.IPLabel.TabIndex = 0;
            this.IPLabel.Text = "IP";
            // 
            // IPText
            // 
            this.IPText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(219)))), ((int)(((byte)(178)))));
            this.IPText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IPText.Dock = System.Windows.Forms.DockStyle.Top;
            this.IPText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.IPText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.IPText.Location = new System.Drawing.Point(0, 20);
            this.IPText.Name = "IPText";
            this.IPText.Size = new System.Drawing.Size(300, 26);
            this.IPText.TabIndex = 1;
            // 
            // PortText
            // 
            this.PortText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(219)))), ((int)(((byte)(178)))));
            this.PortText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PortText.Dock = System.Windows.Forms.DockStyle.Top;
            this.PortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PortText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.PortText.Location = new System.Drawing.Point(0, 66);
            this.PortText.Name = "PortText";
            this.PortText.Size = new System.Drawing.Size(300, 26);
            this.PortText.TabIndex = 4;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.PortLabel.Location = new System.Drawing.Point(0, 46);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(38, 20);
            this.PortLabel.TabIndex = 3;
            this.PortLabel.Text = "Port";
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(134)))), ((int)(((byte)(155)))));
            this.ConnectButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ConnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.ConnectButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ConnectButton.Location = new System.Drawing.Point(0, 102);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(300, 30);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PortText);
            this.panel1.Controls.Add(this.PortLabel);
            this.panel1.Controls.Add(this.IPText);
            this.panel1.Controls.Add(this.IPLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 92);
            this.panel1.TabIndex = 6;
            // 
            // ConnectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ConnectButton);
            this.Name = "ConnectView";
            this.Size = new System.Drawing.Size(300, 132);
            this.Load += new System.EventHandler(this.ConnectControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
		private GruvboxLabel IPLabel;
		private GruvboxTextBox IPText;
		private GruvboxTextBox PortText;
		private GruvboxLabel PortLabel;
		private GruvboxButton ConnectButton;
	}
}
