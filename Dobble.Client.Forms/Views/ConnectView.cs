using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dobble.Shared;

namespace Dobble.Client.Forms
{
	internal partial class ConnectView : UserControl
	{
		private readonly GameUIManager gameUIManager;
		bool connecting = false;
		TcpClient client;
		Stream encryptedStream;

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


					await this.InitiateEncryptionAsync();

					this.gameUIManager.ClientConnected(this.client, this.encryptedStream);
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

		private async Task InitiateEncryptionAsync()
		{
			// Receive public key from server asynchronously
			NetworkStream stream = this.client.GetStream();
			StreamReader reader = new StreamReader(stream);
			string publicKey = await reader.ReadLineAsync();

			// Create AES key
			AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
			aes.GenerateKey();
			aes.GenerateIV();

			// Encrypt AES key with RSA public key
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(publicKey);
			byte[] encryptedKey = rsa.Encrypt(aes.Key, false);
			string encryptedKeyString = Convert.ToBase64String(encryptedKey);

			// Send encrypted AES key to server asynchronously
			StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
			await writer.WriteLineAsync(encryptedKeyString);
			await writer.WriteLineAsync(Convert.ToBase64String(aes.IV));

			this.encryptedStream = EncryptedTcp.SetupEncryptedStream(this.client, aes.Key, aes.IV);
		}
	}
}
