using Kernel.Core;
using Kernel.UI;

namespace Scripts.UI.Forms
{
	internal class FormGameHUDMediator : FormMediator<FormGameHUD>
	{
		protected override void OnViewOpen()
		{
			base.OnViewOpen();

			View.PauseClickedEvent += View_PauseClickedEvent;
		}

		protected override void OnViewClose()
		{
			base.OnViewClose();

			if (View != null)
			{
				View.PauseClickedEvent -= View_PauseClickedEvent;
			}
		}


		#region UI Events
		private void View_PauseClickedEvent()
		{
			SceneContext.TriggerEvent(Events.Pause);
		}
		#endregion
	}
}