using UnityEngine;
using System.Collections;
using Kernel.StateMachine;
using Scripts.Components;
using Kernel.UI;

namespace Scripts.Contexts.Lobby.FSM
{
	public class MainMenuState : ConcreteState
	{
		private Form _form;


		protected void Awake()
		{
			_form = UIManager.CreateForm("MainMenu");
		}

		protected void OnEnter()
		{
			if (_form != null)
				_form.Open();
		}

		protected void OnExit()
		{
			if (_form != null)
				_form.Close();
		}
	}
}
