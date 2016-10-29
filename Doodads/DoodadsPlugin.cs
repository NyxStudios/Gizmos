using System;
using System.Collections.Generic;
using Doodads.Commands;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Doodads
{
	/// <summary>
	/// The main plugin class should always be decorated with an ApiVersion attribute. The current API Version is 1.22
	/// </summary>
	[ApiVersion(1, 25)]
	public class DoodadsPlugin : TerrariaPlugin
	{
		/// <summary>
		/// The name of the plugin.
		/// </summary>
		public override string Name => "TShock Doodad Commands";

		/// <summary>
		/// The version of the plugin in its current state.
		/// </summary>
		public override Version Version => new Version(0, 1, 0);

		/// <summary>
		/// The author(s) of the plugin.
		/// </summary>
		public override string Author => "NyxStudios and Contributors";

		/// <summary>
		/// A short, one-line, description of the plugin's purpose.
		/// </summary>
		public override string Description => "Fun commands that have been removed from the core TShock plugin.";

		/// <summary>
		/// A List of command handlers for this Gizmo.
		/// </summary>
		private List<ICommandHandler> _commandHandlers = new List<ICommandHandler>();

		/// <summary>
		/// The plugin's constructor
		/// Set your plugin's order (optional) and any other constructor logic here
		/// </summary>
		public DoodadsPlugin(Main game) : base(game)
		{
			_commandHandlers.Add(new Firework());
		}

		/// <summary>
		/// Performs plugin initialization logic.
		/// Add your hooks, config file read/writes, etc here
		/// </summary>
		public override void Initialize()
		{
			TShock.Initialized += InitializeCommands;
		}

		/// <summary>
		/// Initialize all the commands in this gizmo.
		/// </summary>
		public void InitializeCommands()
		{
			TShock.Initialized -= InitializeCommands;
			foreach (var command in _commandHandlers)
			{
				command.RegisterCommand();
			}
		}

		/// <summary>
		/// Performs plugin cleanup logic
		/// Remove your hooks and perform general cleanup here
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (var command in _commandHandlers)
				{
					command.DeregisterCommand();
				}
				_commandHandlers = null;
			}
			base.Dispose(disposing);
		}
	}
}
