using UnityEngine;
using System.Collections;
using Kernel.UI;
using System;
using TMPro;

namespace Scripts.UI.Forms
{
	[RequireComponent(typeof(FormGameOverMediator))]
	public class FormGameOver : Form
	{
		#region Events
		public event Action RestartClickedEvent;
		public event Action ExitClickedEvent;
		#endregion

		[SerializeField]
		private TMP_Text _scoreText;


		public int ScoreText
		{
			set { _scoreText.text = value.ToString(); }
		}

		#region UI Events
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
