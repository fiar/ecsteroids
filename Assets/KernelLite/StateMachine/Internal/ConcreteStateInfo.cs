using System;
using System.Reflection;

namespace Kernel.StateMachine.Internal
{
	public class ConcreteStateInfo
	{
		public MethodInfo Awake { get; set; }
		public MethodInfo OnDestroy { get; set; }
		public MethodInfo OnEnter { get; set; }
		public MethodInfo OnExit { get; set; }
		public MethodInfo Update { get; set; }
	}
}
