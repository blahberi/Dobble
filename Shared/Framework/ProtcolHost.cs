namespace Dobble.Shared.Framework
{
	public static class ProtocolHost
	{
		/// <summary>
		/// Creates a default protocol manager builder
		/// </summary>
		/// <typeparam name="TConnectionContext"></typeparam>
		/// <returns></returns>
		public static IProtocolManagerBuilder<TConnectionContext> CreateDefaultBuilder<TConnectionContext>()
			where TConnectionContext : ConnectionContext, new()
		{
			return new ProtocolManagerBuilder<TConnectionContext>();
		}
	}

}
