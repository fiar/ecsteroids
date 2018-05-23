using Kernel.StateMachine.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kernel.StateMachine
{
	public interface IConcreteStateDeclaration
	{
		void Awake();
		void Destroy();
		void Enter(params object[] args);
		void Exit();
		void Update();
	}

	public class ConcreteStateDeclaration<T> : IConcreteStateDeclaration where T : IConcreteState, new()
	{
		public T State { get; protected set; }

		private ConcreteStateInfo _info;

		private bool _isEntered;


		public ConcreteStateDeclaration()
		{
			State = new T();
			_info = ConcreteStateDeclarations.ResolveStateInfo<T>();
		}

		public void Awake()
		{
			if (_info.Awake != null && State != null)
			{
				_info.Awake.Invoke(State, null);
				_info.Awake = null;
			}
		}

		public void Destroy()
		{
			Exit();

			if (_info.OnDestroy != null && State != null)
			{
				_info.OnDestroy.Invoke(State, null);
				_info.OnDestroy = null;
			}
		}

		public void Enter(params object[] args)
		{
			_isEntered = true;

			if (State != null)
			{
				State.Args = args;

				if (_info.OnEnter != null)
				{
					_info.OnEnter.Invoke(State, null);
				}
			}
		}

		public void Exit()
		{
			if (_isEntered)
			{
				_isEntered = false;

				if (_info.OnExit != null && State != null)
				{
					_info.OnExit.Invoke(State, null);
				}
			}
		}

		public void Update()
		{
			if (_info.Update != null && State != null)
			{
				_info.Update.Invoke(State, null);
			}
		}
	}
}
