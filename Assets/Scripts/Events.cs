using UnityEngine;
using System.Collections;

namespace Scripts
{
	public static class Events
	{
		// Shared
		public const string Back = "BACK";

		// Lobby
		public const string NewGame = "NEW_GAME";
		public const string Continue = "CONTINUE";
		public const string Exit = "EXIT";

		// Game
		public const string Pause = "PAUSE";
		public const string ContinueStage = "CONTINUE_STAGE";
		public const string RestartStage = "RESTART_STAGE";
		public const string ExitStage = "EXIT_STAGE";
		public const string GameOver = "GAME_OVER";
	}
}
