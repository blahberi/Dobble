using Dobble.Server.DataAccess.Interfaces;

namespace Dobble.Server.DataAccess
{
	internal class SqlDataAccess : IDataAccess
	{
		public SqlDataAccess()
		{
			this.UserRepository = new Repositories.UserRepository();
		}

		public IUserRepository UserRepository { get; }
	}
}
