using Kernel.Core;
using Kernel.UI;
using Scripts.Configs;
using Scripts.Contexts.Game;
using UnityEngine;

namespace Scripts.UI.Forms
{
	internal class FormGameHUDMediator : FormMediator<FormGameHUD>
	{
		private ProjectilesConfig _config;


		protected override void OnViewOpen()
		{
			base.OnViewOpen();

			_config = ConfigManager.Load<ProjectilesConfig>();

			VariablesContainer.ScoreChanged += VariablesContainer_ScoreChanged;
			VariablesContainer.LaserEnergyChanged += VariablesContainer_LaserEnergyChanged;

			UpdateValues();

			View.PauseClickedEvent += View_PauseClickedEvent;
		}

		protected override void OnViewClose()
		{
			base.OnViewClose();

			VariablesContainer.ScoreChanged -= VariablesContainer_ScoreChanged;
			VariablesContainer.LaserEnergyChanged -= VariablesContainer_LaserEnergyChanged;

			if (View != null)
			{
				View.PauseClickedEvent -= View_PauseClickedEvent;
			}
		}

		private void UpdateValues()
		{
			View.ScoreText = VariablesContainer.Score;
			View.LaserEnergyText = VariablesContainer.LaserEnergy;
			View.LaserEnergyColor = (VariablesContainer.LaserEnergy >= _config.LaserShootEnergy) ? Color.white : Color.red;
		}


		#region Events
		private void VariablesContainer_LaserEnergyChanged(float v)
		{
			UpdateValues();
		}

		private void VariablesContainer_ScoreChanged(int v)
		{
			UpdateValues();
		}
		#endregion

		#region UI Events
		private void View_PauseClickedEvent()
		{
			SceneContext.TriggerEvent(Events.Pause);
		}
		#endregion
	}
}