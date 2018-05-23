using Kernel.Core;
using Kernel.UI;

namespace Scripts.UI.Forms
{
	internal class FormPauseMediator : FormMediator<FormPause>
	{
		protected override void OnViewOpen()
		{
			base.OnViewOpen();

			View.ContinueClickedEvent += View_ContinueClickedEvent;
			View.RestartClickedEvent += View_RestartClickedEvent;
			View.ExitClickedEvent += View_ExitClickedEvent;
		}

		protected override void OnViewClose()
		{
			base.OnViewClose();

			if (View != null)
			{
				View.ContinueClickedEvent -= View_ContinueClickedEvent;
				View.RestartClickedEvent -= View_RestartClickedEvent;
				View.ExitClickedEvent -= View_ExitClickedEvent;
			}
		}


		#region UI Events
		private void View_ContinueClickedEvent()
		{
			SceneContext.TriggerEvent(Events.ContinueStage);
		}

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