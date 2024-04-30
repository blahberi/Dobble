using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Dobble.Client.Forms
{
	internal partial class ConnectView : UserControl
	{
		private readonly GameUIManager gameUIManager;
		bool connecting = false;
		TcpClient client;

		public ConnectView(GameUIManager gameUIManager)
		{
			this.InitializeComponent();
			this.gameUIManager = gameUIManager;
		}

		private void ConnectControl_Load(object sender, EventArgs e)
		{
			// Read the default IP from the settings and set it in the text box
			this.IPText.Text = Properties.Settings.Default.ServerIP;
			this.PortText.Text = Properties.Settings.Default.ServerPort.ToString();
		}

		private async void ConnectButton_Click(object sender, EventArgs e)
		{
			if (this.connecting)
			{
				this.client.Dispose();
			}
			else
			{
				try
				{
					// Connect
					this.connecting = true;
					this.ConnectButton.Text = "Cancel";
					this.IPText.Enabled = false;
					this.PortText.Enabled = false;

					this.client = new TcpClient();

					await this.client.ConnectAsync(this.IPText.Text, int.Parse(this.PortText.Text));

					this.gameUIManager.ClientConnected(this.client);
				}
				catch (Exception)
				{
					this.Disconnect();
				}
			}
		}

		private void Disconnect()
		{
			// Disconnect
			this.connecting = false;
			this.ConnectButton.Text = "Connect";
			this.IPText.Enabled = true;
			this.PortText.Enabled = true;
		}
	}
}
