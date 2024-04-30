using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dobble.Client.Forms
{
	public partial class MainForm : Form
	{
		private readonly GameUIManager gameManager;
		private UserControl activeControl;
		private readonly int padding = 50;

		public MainForm()
		{
			this.gameManager = new GameUIManager();
			this.InitializeComponent();
			this.LoadControl(this.gameManager.ActiveControl);
			this.gameManager.OnActiveControlChanged += () => this.LoadControl(this.gameManager.ActiveControl);
			this.Resize += this.MainForm_Resize;

			this.BackColor = GruvboxTheme.Background;
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			this.CenterControl(this.activeControl);
		}

		private void LoadControl(UserControl newControl)
		{
			if (this.activeControl != null)
			{
				this.Controls.Remove(this.activeControl);
				this.activeControl.Dispose();
			}

			this.activeControl = newControl;
			this.Controls.Add(this.activeControl);
			this.CenterControl(this.activeControl);
			int width = this.activeControl.Size.Width + this.padding;
			int height = this.activeControl.Size.Height + this.padding;
			this.MinimumSize = new Size(width, height);

		}

		private void CenterControl(Control control)
		{
			control.Left = (this.ClientSize.Width - control.Width) / 2;
			control.Top = (this.ClientSize.Height - control.Height) / 2;
		}
	}
}
