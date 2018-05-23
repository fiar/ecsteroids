using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kernel.StateMachine.Internal
{
	public static class ConcreteStateDeclarations
	{

		public static ConcreteStateInfo ResolveStateInfo<T>() where T : IConcreteState
		{
			var type = typeof(T);

			var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

			return new ConcreteStateInfo
			{
				Awake = type.GetMethod("Awake", flags),
				OnDestroy = type.GetMethod("OnDestroy", flags),
				OnEnter = type.GetMethod("OnEnter", flags),
				OnExit = type.GetMethod("OnExit", flags),
				Update = type.GetMethod("Update", flags)
			};
		}
	}
}
