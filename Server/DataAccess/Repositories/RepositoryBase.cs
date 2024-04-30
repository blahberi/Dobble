using System.Configuration;

namespace Dobble.Server.DataAccess.Repositories
{
	internal class RepositoryBase
	{
		public RepositoryBase()
		{
			this.ConnectionString = ConfigurationManager.ConnectionStrings["Dobble.Server.Properties.Settings.DobbleConnectionString"].ConnectionString;
		}

		protected string ConnectionString { get; }
	}
}
