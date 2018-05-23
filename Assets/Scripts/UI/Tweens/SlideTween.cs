using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kernel.UI.Tweens;

namespace Scripts.UI.Tweens
{
	[AddComponentMenu("UI/Tweens/SlideTween")]
	[RequireComponent(typeof(CanvasGroup))]
	public class SlideTween : UITween
	{
		[SerializeField]
		private Vector3 _openFrom = Vector3.zero;
		[SerializeField]
		private Vector3 _closeTo = Vector3.zero;
		[SerializeField]
		private float _openDelay = 0f;
		[SerializeField]
		private float _closeDelay = 0f;
		[SerializeField]
		private float _duration = 0.2f;
		[SerializeField, Range(0f, 1f)]
		private float _fromAlpha = 0f;
		[SerializeField, Range(0f, 1f)]
		private float _toAlpha = 1f;
		[SerializeField]
		private Ease _ease = Ease.OutQuad;

		private Vector3 _cachedPosition;
		private bool _initialized = false;
		private CanvasGroup _canvasGroup;


		protected void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			Debug.AssertFormat(_canvasGroup != null, "GameObject: {0}, Tween: {1}, Required CanvasGroup", gameObject, GetType().Name);
		}

		public override void ResetTween()
		{
			base.ResetTween();

			_cachedPosition = transform.localPosition;
			transform.localPosition = _openFrom;
			if (_canvasGroup != null)
			{
				_canvasGroup.alpha = _fromAlpha;
			}
		}

		protected override void OnOpen()
		{
			base.OnOpen();

			if (!_initialized)
			{
				_initialized = true;
			}

			StopAllCoroutines();
			StartCoroutine(OpenAsync());
		}

		private IEnumerator OpenAsync()
		{
			transform.localPosition = _openFrom;
			transform.DOLocalMove(_cachedPosition, _duration)
				.SetDelay(_openDelay)
				.SetEase(_ease)
				.SetUpdate(true)
				.SetTarget(_tweenTarget);

			if (_canvasGroup != null)
			{
				_canvasGroup.alpha = _fromAlpha;
				_canvasGroup.DOFade(_toAlpha, _duration)
					.SetDelay(_openDelay)
					.SetEase(_ease)
					.SetUpdate(true)
					.SetTarget(_tweenTarget);
			}

			while (DOTween.IsTweening(_tweenTarget)) yield return null;

			OpenedEventInvoke();
		}

		protected override void OnClose()
		{
			base.OnClose();

			StopAllCoroutines();
			StartCoroutine(CloseAsync());
		}

		private IEnumerator CloseAsync()
		{
			transform.DOLocalMove(_closeTo, _duration)
				.SetDelay(_closeDelay)
				.SetEase(_ease)
				.SetUpdate(true)
				.SetTarget(_tweenTarget);

			if (_canvasGroup != null)
			{
				_canvasGroup.DOFade(_fromAlpha, _duration)
					.SetDelay(_closeDelay)
					.SetEase(_ease)
					.SetUpdate(true)
					.SetTarget(_tweenTarget);
			}

			while (DOTween.IsTweening(_tweenTarget)) yield return null;

			ClosedEventInvoke();
		}
	}
}
