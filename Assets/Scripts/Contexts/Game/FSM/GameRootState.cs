﻿using UnityEngine;
using System.Collections;
using Kernel.StateMachine;
using Kernel.UI;

namespace Scripts.Contexts.Game.FSM
{
	public class GameRootState : ConcreteState
	{
		private Form _form;


		protected void Awake()
		{
			_form = UIManager.CreateForm("GameHUD");

			VariablesContainer.Reset();
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
