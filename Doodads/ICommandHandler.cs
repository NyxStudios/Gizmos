using TShockAPI;

namespace Doodads
{
	interface ICommandHandler
	{
		void RegisterCommand();
		void HandleCommand(CommandArgs args);
		void DeregisterCommand();
	}
}
