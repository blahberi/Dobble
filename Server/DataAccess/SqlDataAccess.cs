using Dobble.Server.DataAccess.Interfaces;

namespace Dobble.Server.DataAccess
{
	internal class SqlDataAccess : IDataAccess
	{
		/// <summary>
		/// Constructor which initializes the SQL data access object.
		/// </summary>
		public SqlDataAccess()
		{
			this.UserRepository = new Repositories.UserRepository();
		}

		public IUserRepository UserRepository { get; }
	}
}
