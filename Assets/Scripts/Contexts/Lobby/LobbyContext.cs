using Kernel.Core;
using Kernel.StateMachine;
using Kernel.UI;
using Scripts.Contexts.Lobby.FSM;
using System;
using UnityEngine;

namespace Scripts.Contexts.Lobby
{
	[DefaultExecutionOrder(-1000)]
	public class LobbyContext : SceneContext
	{

		protected override void StartContext()
		{
			base.StartContext();

			UIManager.ScreenFader.FadeOut();

			FSM.ChangeState("Root");
		}

		protected override void StopContext()
		{
			base.StopContext();
		}

		protected override IAbstractState BuildStateMachine()
		{
			var rootState = new ConcreteStateDeclaration<LobbyRootState>();
			var mainMenuState = new ConcreteStateDeclaration<MainMenuState>();

			return new StateMachineBuilder()
				///
				.State("Root", rootState)
				.Enter(state => state.ChangeState("MainMenu"))
					// MainMenu
					.State("MainMenu", mainMenuState)
						.Event(Events.NewGame, state => { NewGame(); state.Parent.ChangeState("Empty"); })
						.Event(Events.Continue, state => { ContinueGame(); state.Parent.ChangeState("Empty"); })
						.Event(Events.Exit, state => { ExitGame(); state.Parent.ChangeState("Empty"); })
					.End()
					// Empty - need to exist from previous state (because Form close on exit)
					.State("Empty")
					.End()
				.End()
				.Build();
		}

		private void NewGame()
		{
			UIManager.ScreenFader.FadeIn(() =>
			{
				KernelApplication.LoadScene(Scenes.Game);
			});
		}

		private void ContinueGame()
		{
			UIManager.ScreenFader.FadeIn(() =>
			{
				KernelApplication.LoadScene(Scenes.Game);
			});
		}

		private void ExitGame()
		{
			UIManager.ScreenFader.FadeIn(() =>
			{
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
			});
		}
	}
}