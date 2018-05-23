using UnityEngine;
using System.Collections;
using Kernel.UI;
using System;
using TMPro;

namespace Scripts.UI.Forms
{
	[RequireComponent(typeof(FormGameHUDMediator))]
	public class FormGameHUD : Form
	{
		#region Events
		public event Action PauseClickedEvent;
		#endregion

		[SerializeField]
		private TMP_Text _scoreText;
		[SerializeField]
		private TMP_Text _laserEnergyText;


		public int ScoreText
		{
			set { _scoreText.text = "Score: " + value.ToString(); }
		}

		public float LaserEnergyText
		{
			set { _laserEnergyText.text = (value * 100).ToString("N1") + "% : Laser"; }
		}

		public Color LaserEnergyColor
		{
			set { _laserEnergyText.color = value; }
		}


		#region UI Events
		public void PauseClickedExecute()
		{
			if (PauseClickedEvent != null)
				PauseClickedEvent.Invoke();
		}
		#endregion
	}
}
