namespace Dobble.Client.Forms.Views
{
	partial class GameView
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
            this.components = new System.ComponentModel.Container();
            this.WrongTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.Card2 = new Dobble.Client.Forms.UserControls.Card();
            this.Card1 = new Dobble.Client.Forms.UserControls.Card();
            this.NewRoundTimer = new System.Windows.Forms.Timer(this.components);
            this.LastRoundTimer = new System.Windows.Forms.Timer(this.components);
            this.YourUserNameLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.YourScoreLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.OpponentScoreLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.OpponentNameLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.RoundSummaryLabel = new Dobble.Client.Forms.GruvboxLabel();
            this.LeaveGameButton = new Dobble.Client.Forms.GruvboxButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // WrongTimer
            // 
            this.WrongTimer.Interval = 1000;
            this.WrongTimer.Tick += new System.EventHandler(this.WrongTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Card2);
            this.panel1.Controls.Add(this.Card1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 300);
            this.panel1.TabIndex = 2;
            // 
            // Card2
            // 
            this.Card2.BackColor = System.Drawing.Color.Transparent;
            this.Card2.Disabled = false;
            this.Card2.Dock = System.Windows.Forms.DockStyle.Right;
            this.Card2.Location = new System.Drawing.Point(400, 0);
            this.Card2.Name = "Card2";
            this.Card2.Size = new System.Drawing.Size(300, 300);
            this.Card2.TabIndex = 1;
            // 
            // Card1
            // 
            this.Card1.BackColor = System.Drawing.Color.Transparent;
            this.Card1.Disabled = false;
            this.Card1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Card1.Location = new System.Drawing.Point(0, 0);
            this.Card1.Name = "Card1";
            this.Card1.Size = new System.Drawing.Size(300, 300);
            this.Card1.TabIndex = 0;
            // 
            // NewRoundTimer
            // 
            this.NewRoundTimer.Interval = 1000;
            this.NewRoundTimer.Tick += new System.EventHandler(this.NewRoundTimer_Tick);
            // 
            // LastRoundTimer
            // 
            this.LastRoundTimer.Interval = 1000;
            this.LastRoundTimer.Tick += new System.EventHandler(this.LastRoundTimer_Tick);
            // 
            // YourUserNameLabel
            // 
            this.YourUserNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.YourUserNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YourUserNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.YourUserNameLabel.Location = new System.Drawing.Point(0, 0);
            this.YourUserNameLabel.Name = "YourUserNameLabel";
            this.YourUserNameLabel.Size = new System.Drawing.Size(225, 31);
            this.YourUserNameLabel.TabIndex = 3;
            this.YourUserNameLabel.Text = "your username";
            // 
            // YourScoreLabel
            // 
            this.YourScoreLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.YourScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YourScoreLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.YourScoreLabel.Location = new System.Drawing.Point(0, 31);
            this.YourScoreLabel.Name = "YourScoreLabel";
            this.YourScoreLabel.Size = new System.Drawing.Size(225, 20);
            this.YourScoreLabel.TabIndex = 4;
            this.YourScoreLabel.Text = "your score";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.YourScoreLabel);
            this.panel2.Controls.Add(this.YourUserNameLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 107);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.OpponentScoreLabel);
            this.panel3.Controls.Add(this.OpponentNameLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(475, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(225, 107);
            this.panel3.TabIndex = 6;
            // 
            // OpponentScoreLabel
            // 
            this.OpponentScoreLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OpponentScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpponentScoreLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.OpponentScoreLabel.Location = new System.Drawing.Point(0, 31);
            this.OpponentScoreLabel.Name = "OpponentScoreLabel";
            this.OpponentScoreLabel.Size = new System.Drawing.Size(225, 20);
            this.OpponentScoreLabel.TabIndex = 4;
            this.OpponentScoreLabel.Text = "opponent score";
            this.OpponentScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OpponentNameLabel
            // 
            this.OpponentNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OpponentNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpponentNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.OpponentNameLabel.Location = new System.Drawing.Point(0, 0);
            this.OpponentNameLabel.Name = "OpponentNameLabel";
            this.OpponentNameLabel.Size = new System.Drawing.Size(225, 31);
            this.OpponentNameLabel.TabIndex = 3;
            this.OpponentNameLabel.Text = "opponent username";
            this.OpponentNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RoundSummaryLabel
            // 
            this.RoundSummaryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoundSummaryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.RoundSummaryLabel.Location = new System.Drawing.Point(150, 30);
            this.RoundSummaryLabel.Name = "RoundSummaryLabel";
            this.RoundSummaryLabel.Size = new System.Drawing.Size(400, 44);
            this.RoundSummaryLabel.TabIndex = 7;
            this.RoundSummaryLabel.Text = "Summary of the previous round";
            this.RoundSummaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RoundSummaryLabel.Visible = false;
            // 
            // LeaveGameButton
            // 
            this.LeaveGameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(73)))), ((int)(((byte)(52)))));
            this.LeaveGameButton.FlatAppearance.BorderSize = 0;
            this.LeaveGameButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(199)))));
            this.LeaveGameButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(219)))), ((int)(((byte)(178)))));
            this.LeaveGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LeaveGameButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.LeaveGameButton.Location = new System.Drawing.Point(0, 413);
            this.LeaveGameButton.Name = "LeaveGameButton";
            this.LeaveGameButton.Size = new System.Drawing.Size(123, 44);
            this.LeaveGameButton.TabIndex = 8;
            this.LeaveGameButton.Text = "Leave Game";
            this.LeaveGameButton.UseVisualStyleBackColor = false;
            this.LeaveGameButton.Click += new System.EventHandler(this.LeaveGameButton_Click);
            // 
            // GameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.LeaveGameButton);
            this.Controls.Add(this.RoundSummaryLabel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "GameView";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.Size = new System.Drawing.Size(700, 457);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private UserControls.Card Card1;
		private System.Windows.Forms.Timer WrongTimer;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Timer NewRoundTimer;
		private System.Windows.Forms.Timer LastRoundTimer;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private GruvboxButton LeaveGameButton;
		private GruvboxLabel YourUserNameLabel;
		private GruvboxLabel YourScoreLabel;
		private GruvboxLabel OpponentNameLabel;
		private GruvboxLabel OpponentScoreLabel;
		private GruvboxLabel RoundSummaryLabel;
		private UserControls.Card Card2;
	}
}
