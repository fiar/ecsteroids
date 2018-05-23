using UnityEngine;
using System.Collections;
using Kernel.UI;
using System;

namespace Scripts.UI.Forms
{
	[RequireComponent(typeof(FormGameHUDMediator))]
	public class FormGameHUD : Form
	{
		#region Events
		public event Action PauseClickedEvent;
		#endregion


		#region UI Events
		public void PauseClickedExecute()
		{
			if (PauseClickedEvent != null)
				PauseClickedEvent.Invoke();
		}
		#endregion
	}
}
