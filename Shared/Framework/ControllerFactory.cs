using System.Collections.Generic;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Factory for creating controllers
	/// </summary>
	/// <typeparam name="TConnectionContext"></typeparam>
	internal class ControllerFactory<TConnectionContext> : IControllerFactory<TConnectionContext>
		where TConnectionContext : ConnectionContext
	{
		/// <summary>
		/// This dictionary holds a controller creator for each path
		/// </summary>
		private readonly Dictionary<string, CreateController<TConnectionContext>> controllerFactories;

		/// <summary>
		/// Constructor
		/// </summary>
		public ControllerFactory()
		{
			this.controllerFactories = new Dictionary<string, CreateController<TConnectionContext>>();
		}

		/// <summary>
		/// Register a controller creator for a given path.
		/// Later, we will be able to create controllers for this path.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="createController"></param>
		public void RegisterController(string path, CreateController<TConnectionContext> createController)
		{
			this.controllerFactories[path] = createController;
		}

		/// <summary>
		/// Create a controller for the given path.
		/// </summary>
		/// <param name="connectionContext"></param>
		/// <param name="path"></param>
		/// <returns></returns>
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
