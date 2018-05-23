using System;

namespace Kernel.StateMachine
{
	public interface IConcreteState
	{
		object[] Args { get; set; }
	}

	public abstract class ConcreteState : IConcreteState
	{
		public object[] Args { get; set; }

		// void Awake();
		// void OnDestroy();
		// void OnEnter();
		// void OnExit();
		// void Update();
	}
}
