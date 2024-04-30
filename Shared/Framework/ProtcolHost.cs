namespace Dobble.Shared.Framework
{
	public static class ProtocolHost
	{
		public static IProtocolManagerBuilder<TConnectionContext> CreateDefaultBuilder<TConnectionContext>()
			where TConnectionContext : ConnectionContext, new()
		{
			return new ProtocolManagerBuilder<TConnectionContext>();
		}
	}

}
