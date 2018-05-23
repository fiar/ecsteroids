using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Kernel.UI.Tweens
{
	public abstract class UITween : MonoBehaviour
	{
		public enum TweenStartAction
		{
			DoNothing,
			StartTween
		}

		#region Events
		public event Action<UITween> OpenedEvent;
		public event Action<UITween> ClosedEvent;
		#endregion

		[SerializeField]
		private TweenStartAction _startAction = TweenStartAction.StartTween;

		protected readonly object _tweenTarget = new object();

		private UITweenState _state = UITweenState.Closed;

		public UITweenState State { get { return _state; } }
		public TweenStartAction StartAction { get { return _startAction; } }

		protected bool WasEnabled { get; private set; }


		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		public void Open(bool wasEnabled)
		{
			DOTween.Kill(_tweenTarget);

			WasEnabled = wasEnabled;
			_state = UITweenState.Opening;

			OnOpen();
		}

		public void Close()
		{
			DOTween.Kill(_tweenTarget);
			_state = UITweenState.Closing;
			OnClose();
		}

		public virtual void ResetTween()
		{
		}

		protected virtual void OnOpen()
		{
		}

		protected virtual void OnClose()
		{
		}

		protected void OpenedEventInvoke()
		{
			_state = UITweenState.Opened;
			if (OpenedEvent != null)
				OpenedEvent.Invoke(this);
		}

		protected void ClosedEventInvoke()
		{
			_state = UITweenState.Closed;
			if (ClosedEvent != null)
				ClosedEvent.Invoke(this);
		}
	}
}
