using System.Collections.Generic;

namespace Dobble.Shared.Framework
{
	internal class ControllerFactory<TConnectionContext> : IControllerFactory<TConnectionContext>
		where TConnectionContext : ConnectionContext
	{
		private readonly Dictionary<string, CreateController<TConnectionContext>> controllerFactories;

		public ControllerFactory()
		{
			this.controllerFactories = new Dictionary<string, CreateController<TConnectionContext>>();
		}

		public void RegisterController(string path, CreateController<TConnectionContext> createController)
		{
			this.controllerFactories[path] = createController;
		}

		public IController CreateController(TConnectionContext connectionContext, string path)
		{
			// Look for a controller factory for the given path
			if (this.controllerFactories.TryGetValue(path, out CreateController<TConnectionContext> createController))
			{
				// Create the controller
				return createController(connectionContext);
			}

			return new UnknownRouteController();
		}
	}
}
