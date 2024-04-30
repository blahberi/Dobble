using Dobble.Server.DataAccess.Interfaces;

namespace Dobble.Server.DataAccess
{
	internal interface IDataAccess
	{
		IUserRepository UserRepository { get; }
	}
}
