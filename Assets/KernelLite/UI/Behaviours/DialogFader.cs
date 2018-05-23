using Kernel.Api;
using System;
using UnityEngine;

namespace Kernel.UI.Behaviours
{
	public abstract class DialogFader : View
	{
		public abstract void FadeIn(Form form, Action completeCallback = null);
		public abstract void FadeIn(Form form, float duration, Action completeCallback = null);
		public abstract void FadeOut(Form form, Action completeCallback = null);
		public abstract void FadeOut(Form form, float duration, Action completeCallback = null);
	}
}
