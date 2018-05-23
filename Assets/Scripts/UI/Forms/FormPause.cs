using UnityEngine;
using System.Collections;
using Kernel.UI;
using System;

namespace Scripts.UI.Forms
{
	[RequireComponent(typeof(FormPauseMediator))]
	public class FormPause : Form
	{
		#region Events
		public event Action ContinueClickedEvent;
		public event Action RestartClickedEvent;
		public event Action ExitClickedEvent;
		#endregion


		#region UI Events
		public void ContinueClickedExecute()
		{
			if (ContinueClickedEvent != null)
				ContinueClickedEvent.Invoke();
		}

		public void RestartClickedExecute()
		{
			if (RestartClickedEvent != null)
				RestartClickedEvent.Invoke();
		}

		public void ExitClickedExecute()
		{
			if (ExitClickedEvent != null)
				ExitClickedEvent.Invoke();
		}
		#endregion
	}
}
