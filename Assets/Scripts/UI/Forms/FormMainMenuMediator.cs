using Kernel.Core;
using Kernel.UI;
using System;

namespace Scripts.UI.Forms
{
	internal class FormMainMenuMediator : FormMediator<FormMainMenu>
	{

		protected override void OnViewOpen()
		{
			base.OnViewOpen();

			View.NewGameClickedEvent += View_NewGameClickedEvent;
			View.ContinueClickedEvent += View_ContinueClickedEvent;
			View.ExitClickedEvent += View_ExitClickedEvent;
		}

		protected override void OnViewClose()
		{
			base.OnViewClose();

			if (View != null)
			{
				View.NewGameClickedEvent -= View_NewGameClickedEvent;
				View.ContinueClickedEvent -= View_ContinueClickedEvent;
				View.ExitClickedEvent -= View_ExitClickedEvent;
			}
		}


		#region UI Events
		private void View_NewGameClickedEvent()
		{
			SceneContext.TriggerEvent(Events.NewGame);
		}

		private void View_ContinueClickedEvent()
		{
			SceneContext.TriggerEvent(Events.Continue);
		}

		private void View_ExitClickedEvent()
		{
			SceneContext.TriggerEvent(Events.Exit);
		}
		#endregion
	}
}