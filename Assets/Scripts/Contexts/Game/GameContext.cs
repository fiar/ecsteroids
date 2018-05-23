using Kernel.Core;
using Kernel.StateMachine;
using Kernel.UI;
using Scripts.Contexts.Game.ECS.Components;
using Scripts.Contexts.Game.FSM;
using System;
using Unity.Entities;
using Unity.Transforms;
using Unity.Transforms2D;
using UnityEngine;

namespace Scripts.Contexts.Game
{
	public class GameContext : SceneContext
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
			var rootState = new ConcreteStateDeclaration<GameRootState>();
			var gameLoopState = new ConcreteStateDeclaration<GameLoopState>();
			var gamePauseState = new ConcreteStateDeclaration<GamePauseState>();
			var gameOverState = new ConcreteStateDeclaration<GameOverState>();

			return new StateMachineBuilder()
				///
				.State("Root", rootState)
					.Enter(state => state.ChangeState("GameLoop"))
					//
					.State("GameLoop", gameLoopState)
						.Event(Events.Pause, state => state.Parent.ChangeState("Pause"))
						.Event(Events.Back, state => state.Parent.ChangeState("Pause"))
						.Event(Events.GameOver, state => state.Parent.ChangeState("GameOver"))
					.End()
					//
					.State("Pause", gamePauseState)
						.Event(Events.Back, state => state.Parent.ChangeState("GameLoop"))
						.Event(Events.ContinueStage, state => state.Parent.ChangeState("GameLoop"))
						.Event(Events.RestartStage, state => { RestartStage(); state.Parent.ChangeState("Empty"); })
						.Event(Events.ExitStage, state => { ExitStage(); state.Parent.ChangeState("Empty"); })
						.Event(Events.GameOver, state => state.Parent.ChangeState("GameOver")) // TODO: need "TriggerEventUpwards" method (not duplicate event)
					.End()
					//
					.State("GameOver", gameOverState)
						.Event(Events.ExitStage, state => { ExitStage(); state.Parent.ChangeState("Empty"); })
						.Event(Events.Back, state => { ExitStage(); state.Parent.ChangeState("Empty"); })
						.Event(Events.RestartStage, state => { RestartStage(); state.Parent.ChangeState("Empty"); })
					.End()
					// Empty - need to exist from previous state (because Form close on exit)
					.State("Empty")
					.End()
				.End()
				.Build();
		}

		private void RestartStage()
		{
			UIManager.ScreenFader.FadeIn(() =>
			{
				KernelApplication.LoadScene(Scenes.Game);
			});
		}

		private void ExitStage()
		{
			UIManager.ScreenFader.FadeIn(() =>
			{
				KernelApplication.LoadScene(Scenes.Lobby);
			});
		}
	}
}