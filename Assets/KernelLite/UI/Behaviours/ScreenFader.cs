using DG.Tweening;
using Kernel.Api;
using System;

namespace Kernel.UI.Behaviours
{
	public abstract class ScreenFader : View
	{
		public abstract void FadeIn(Action completeCallback = null);
		public abstract void FadeIn(float duration, Action completeCallback = null);
		public abstract void FadeOut(Action completeCallback = null);
		public abstract void FadeOut(float duration, Action completeCallback = null);
	}
}
