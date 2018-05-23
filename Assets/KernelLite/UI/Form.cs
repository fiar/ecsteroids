using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kernel.Api;
using Kernel.UI.Tweens;

namespace Kernel.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class Form : View
	{
		#region Events
		public event Action OpenEvent;
		public event Action OpenedEvent;
		public event Action CloseEvent;
		public event Action ClosedEvent;
		#endregion

		public FormState State { get { return _state; } }

		[SerializeField]
		private List<UITween> _tweens;

		[SerializeField]
		private bool _showFader;

		//[SerializeField]
		private FormState _state = FormState.Closed;

		private IEnumerator _tweensRoutine;

		private bool _needCloseFader;

		protected CanvasGroup _canvasGroup;

		protected virtual void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnEnable()
		{
			_state = FormState.Closed;
		}

		protected virtual void OnDisable()
		{
			_state = FormState.Closed;
		}

		public virtual void Destroy()
		{
			if (gameObject != null) Destroy(gameObject);
		}

		public virtual void OnOpen(params object[] args) { }
		public virtual void OnOpened() { }
		public virtual void OnClose() { }
		public virtual void OnClosed() { }

		public virtual void Open(params object[] args)
		{
			OpenInternal(true, args);
		}

		public virtual void OpenIgnoreSibling(params object[] args)
		{
			OpenInternal(false, args);
		}

		private void OpenInternal(bool setAsLastSibling, params object[] args)
		{
			if (IsOpen) return;

			_canvasGroup.blocksRaycasts = true;

			WasEnabled = !gameObject.activeSelf;
			gameObject.SetActive(true);

			if (setAsLastSibling)
				transform.SetAsLastSibling();

			if (_showFader)
			{
				_needCloseFader = true;
				if (UIManager.DialogFader != null)
					UIManager.DialogFader.FadeIn(this);
			}

			_state = FormState.Opening;
			OnOpen(args);
			if (OpenEvent != null) OpenEvent.Invoke();

			if (_tweensRoutine != null) StopCoroutine(_tweensRoutine);
			_tweensRoutine = OpenAsync();
			StartCoroutine(_tweensRoutine);
		}

		public virtual void Close()
		{
			Close(false);
		}

		public virtual void Close(bool force)
		{
			if (_needCloseFader && UIManager.DialogFader != null)
				UIManager.DialogFader.FadeOut(this);

			_canvasGroup.blocksRaycasts = false;

			if (force)
			{
				if (_state == FormState.Closed && !gameObject.activeSelf) return;

				OnClose();
				if (CloseEvent != null) CloseEvent();
				OnClosed();
				if (ClosedEvent != null) ClosedEvent.Invoke();

				gameObject.SetActive(false);
			}
			else
			{
				if (IsClose) return;

				_state = FormState.Closing;

				OnClose();
				if (CloseEvent != null) CloseEvent();

				if (_tweensRoutine != null) StopCoroutine(_tweensRoutine);
				_tweensRoutine = CloseAsync();
				StartCoroutine(_tweensRoutine);
			}
		}

		public void UpdateTweens()
		{
			GetComponentsInChildren<UITween>(_tweens);
		}

		public bool ShowFader
		{
			get { return _showFader; }
			set { _showFader = value; }
		}

		public bool IsOpen
		{
			get { return isActiveAndEnabled && (State == FormState.Opening || State == FormState.Opened); }
		}

		public bool IsClose
		{
			get { return !isActiveAndEnabled || State == FormState.Closing || State == FormState.Closed; }
		}

		public bool WasEnabled { get; private set; }

		private bool IsPrefab
		{
			get
			{
#if UNITY_EDITOR
				var prefabType = UnityEditor.PrefabUtility.GetPrefabType(gameObject);
				return (prefabType == UnityEditor.PrefabType.Prefab);
#else
				return false;
#endif
			}
		}

		private IEnumerator OpenAsync()
		{
			if (_tweens.Count > 0)
			{
				foreach (var tween in _tweens)
				{
					if (tween != null && tween.isActiveAndEnabled && tween.StartAction != UITween.TweenStartAction.DoNothing) tween.Open(WasEnabled);
				}

				bool isTweening = true;
				while (isTweening)
				{
					isTweening = false;
					foreach (var tween in _tweens)
					{
						if (tween != null && tween.isActiveAndEnabled && tween.State != UITweenState.Opened)
						{
							isTweening = true;
							break;
						}
					}
					yield return null;
				}
			}

			_state = FormState.Opened;
			_tweensRoutine = null;
			OnOpened();
			if (OpenedEvent != null) OpenedEvent.Invoke();
		}

		private IEnumerator CloseAsync()
		{
			if (_tweens.Count > 0)
			{
				foreach (var tween in _tweens)
				{
					if (tween != null && tween.isActiveAndEnabled) tween.Close();
				}

				bool isTweening = true;
				while (isTweening)
				{
					isTweening = false;
					foreach (var tween in _tweens)
					{
						if (tween != null && tween.isActiveAndEnabled && tween.State != UITweenState.Closed)
						{
							isTweening = true;
							break;
						}
					}
					yield return null;
				}
			}

			_tweensRoutine = null;
			OnClosed();
			if (ClosedEvent != null) ClosedEvent.Invoke();

			gameObject.SetActive(false);
		}
	}
}
