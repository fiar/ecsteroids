using UnityEngine;
using System.Collections;
using Kernel.StateMachine;
using Scripts.Components;
using Kernel.UI;

namespace Scripts.Contexts.Game.FSM
{
	public class GamePauseState : ConcreteState
	{
		private Form _form;


		protected void Awake()
		{
			_form = UIManager.CreateForm("Pause");
		}

		protected void OnEnter()
		{
			// TODO: Its can be better
			Time.timeScale = 0f;

			if (_form != null)
				_form.Open();
		}

		protected void OnExit()
		{
			// TODO: Its can be better
			Time.timeScale = 1f;

			if (_form != null)
				_form.Close();
		}
	}
}
