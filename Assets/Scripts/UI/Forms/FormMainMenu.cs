using UnityEngine;
using System.Collections;
using Kernel.UI;
using System;

namespace Scripts.UI.Forms
{
	[RequireComponent(typeof(FormMainMenuMediator))]
	public class FormMainMenu : Form
	{
		#region Events
		public event Action NewGameClickedEvent;
		public event Action ContinueClickedEvent;
		public event Action ExitClickedEvent;
		#endregion


		#region UI Events
		public void NewGameClickedExecute()
		{
			if (NewGameClickedEvent != null)
				NewGameClickedEvent.Invoke();
		}

		public void ContinueClickedExecute()
		{
			if (ContinueClickedEvent != null)
				ContinueClickedEvent.Invoke();
		}

		public void ExitClickedExecute()
		{
			if (ExitClickedEvent != null)
				ExitClickedEvent.Invoke();
		}
		#endregion
	}
}
