using System.Linq;
using Terraria;
using TShockAPI;

namespace Doodads.Commands
{
	class Firework : ICommandHandler
	{
		private readonly Command _command;

		public Firework()
		{
			_command = new Command(Permissions.annoy, HandleCommand, "firework")
			{
				HelpText = "Spawns fireworks at a player."
			};
		}

		public void RegisterCommand()
		{
			TShockAPI.Commands.ChatCommands.Add(_command);
		}

		public void HandleCommand(CommandArgs args)
		{
			if (args.Parameters.Count < 1)
			{
				args.Player.SendErrorMessage("Invalid syntax! Proper syntax: {0}firework <player> [red|green|blue|yellow]", TShockAPI.Commands.Specifier);
				return;
			}

			var players = TShock.Utils.FindPlayer(args.Parameters[0]);
			if (players.Count == 0)
				args.Player.SendErrorMessage("Invalid player!");
			else if (players.Count > 1)
				TShock.Utils.SendMultipleMatchError(args.Player, players.Select(p => p.Name));
			else
			{
				int type = 167;
				if (args.Parameters.Count > 1)
				{
					if (args.Parameters[1].ToLower() == "green")
						type = 168;
					else if (args.Parameters[1].ToLower() == "blue")
						type = 169;
					else if (args.Parameters[1].ToLower() == "yellow")
						type = 170;
				}

				var ply = players[0];
				int p = Projectile.NewProjectile(ply.TPlayer.position.X, ply.TPlayer.position.Y - 64f, 0f, -8f, type, 0, (float)0);
				Main.projectile[p].Kill();
				args.Player.SendSuccessMessage("Launched Firework on {0}.", ply.Name);
			}
		}

		public void DeregisterCommand()
		{
			TShockAPI.Commands.ChatCommands.Remove(_command);
		}
	}
}
