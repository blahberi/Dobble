namespace Dobble.Client.Forms.Views
{
	partial class GameOverView
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
            this.YourUserNameLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.YourScoreLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OpponentNameLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.OpponentScoreLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.WhoWonLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.label3 = new Dobble.Client.Forms.GruvboxLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new Dobble.Client.Forms.GruvboxButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.YourUserNameLabel);
            this.panel1.Controls.Add(this.YourScoreLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 125);
            this.panel1.TabIndex = 0;
            // 
            // YourUserNameLabel
            // 
            this.YourUserNameLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.YourUserNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YourUserNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.YourUserNameLabel.Location = new System.Drawing.Point(0, 48);
            this.YourUserNameLabel.Name = "YourUserNameLabel";
            this.YourUserNameLabel.Size = new System.Drawing.Size(400, 39);
            this.YourUserNameLabel.TabIndex = 2;
            this.YourUserNameLabel.Text = "your username";
            this.YourUserNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // YourScoreLabel
            // 
            this.YourScoreLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.YourScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.YourScoreLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.YourScoreLabel.Location = new System.Drawing.Point(0, 87);
            this.YourScoreLabel.Name = "YourScoreLabel";
            this.YourScoreLabel.Size = new System.Drawing.Size(400, 38);
            this.YourScoreLabel.TabIndex = 3;
            this.YourScoreLabel.Text = "your score";
            this.YourScoreLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.OpponentNameLabel);
            this.panel2.Controls.Add(this.OpponentScoreLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 225);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 125);
            this.panel2.TabIndex = 1;
            // 
            // OpponentNameLabel
            // 
            this.OpponentNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OpponentNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpponentNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.OpponentNameLabel.Location = new System.Drawing.Point(0, 38);
            this.OpponentNameLabel.Name = "OpponentNameLabel";
            this.OpponentNameLabel.Size = new System.Drawing.Size(400, 39);
            this.OpponentNameLabel.TabIndex = 4;
            this.OpponentNameLabel.Text = "opponent username";
            this.OpponentNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // OpponentScoreLabel
            // 
            this.OpponentScoreLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OpponentScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.OpponentScoreLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.OpponentScoreLabel.Location = new System.Drawing.Point(0, 0);
            this.OpponentScoreLabel.Name = "OpponentScoreLabel";
            this.OpponentScoreLabel.Size = new System.Drawing.Size(400, 38);
            this.OpponentScoreLabel.TabIndex = 5;
            this.OpponentScoreLabel.Text = "opponent score";
            this.OpponentScoreLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.WhoWonLabel);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 100);
            this.panel3.TabIndex = 6;
            // 
            // WhoWonLabel
            // 
            this.WhoWonLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.WhoWonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WhoWonLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.WhoWonLabel.Location = new System.Drawing.Point(0, 58);
            this.WhoWonLabel.Name = "WhoWonLabel";
            this.WhoWonLabel.Size = new System.Drawing.Size(400, 38);
            this.WhoWonLabel.TabIndex = 3;
            this.WhoWonLabel.Text = "who won?";
            this.WhoWonLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(400, 58);
            this.label3.TabIndex = 2;
            this.label3.Text = "Game Finished!";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 350);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(400, 50);
            this.panel4.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(134)))), ((int)(((byte)(155)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(400, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "Return To Lobby";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GameOverView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Name = "GameOverView";
            this.Size = new System.Drawing.Size(400, 400);
            this.Load += new System.EventHandler(this.GameOverView_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private GruvboxLabel YourUserNameLabel;
		private System.Windows.Forms.Panel panel2;
		private GruvboxLabel YourScoreLabel;
		private GruvboxLabel OpponentNameLabel;
		private GruvboxLabel OpponentScoreLabel;
		private System.Windows.Forms.Panel panel3;
		private GruvboxLabel WhoWonLabel;
		private GruvboxLabel label3;
		private System.Windows.Forms.Panel panel4;
		private GruvboxButton button1;
	}
}
