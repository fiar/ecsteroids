using Kernel.Core;
using Kernel.UI;
using Scripts.Contexts.Game;

namespace Scripts.UI.Forms
{
	internal class FormGameOverMediator : FormMediator<FormGameOver>
	{
		protected override void OnViewOpen()
		{
			base.OnViewOpen();

			View.ScoreText = VariablesContainer.Score;

			View.RestartClickedEvent += View_RestartClickedEvent;
			View.ExitClickedEvent += View_ExitClickedEvent;
		}

		protected override void OnViewClose()
		{
			base.OnViewClose();

			if (View != null)
			{
				View.RestartClickedEvent -= View_RestartClickedEvent;
				View.ExitClickedEvent -= View_ExitClickedEvent;
			}
		}


		#region UI Events
		private void View_RestartClickedEvent()
		{
			SceneContext.TriggerEvent(Events.RestartStage);
		}

		private void View_ExitClickedEvent()
		{
			SceneContext.TriggerEvent(Events.ExitStage);
		}
		#endregion
	}
}