using System.Windows.Forms;

namespace Dobble.Client.Forms.Views
{
	internal partial class RegisterView : UserControl
	{
		private readonly GameUIManager gameUIManager;

		public RegisterView(GameUIManager gameUIManager)
		{
			this.InitializeComponent();
			this.gameUIManager = gameUIManager;
		}

		private async void RegisterButton_Click(object sender, System.EventArgs e)
		{
			string username = this.UserNameText.Text;
			string password = this.PasswordText.Text;
			string email = this.EmailText.Text;
			string firstName = this.FirstNameText.Text;
			string lastName = this.LastNameText.Text;
			string country = this.CountryText.Text;
			string city = this.CityText.Text;
			string gender = this.GenderCombo.Text;

			this.RegisterButton.Enabled = false;
			await this.gameUIManager.TryRegister(username, password, email, firstName, lastName, country, city, gender);
			this.RegisterButton.Enabled = true;
		}

		private void LoginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.gameUIManager.ClientReturnToLogin();
		}
	}
}
