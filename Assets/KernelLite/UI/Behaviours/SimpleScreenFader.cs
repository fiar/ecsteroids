using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Kernel.UI.Behaviours
{
	[RequireComponent(typeof(CanvasGroup))]
	public class SimpleScreenFader : ScreenFader
	{
		public const float DefaultDuration = 1f;
		public const float FadeOutDelay = 0.33f;
		public const Ease TweenEase = Ease.OutQuad;


		private CanvasGroup _canvasGroup;
		private GraphicRaycaster[] _raycasters;

		private object _tweenTarget = new object();


		protected void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		public override void FadeIn(Action completeCallback = null)
		{
			FadeIn(DefaultDuration, completeCallback);
		}

		public override void FadeIn(float duration, Action completeCallback = null)
		{
			// TODO: Bad hack (blocksRaycasts not work)
			_raycasters = FindObjectsOfType<GraphicRaycaster>();
			foreach (var raycaster in _raycasters)
			{
				raycaster.enabled = false;
			}

			DOTween.Kill(_tweenTarget);

			_canvasGroup.alpha = 0f;
			_canvasGroup.blocksRaycasts = true; // not work
			gameObject.SetActive(true);
			_canvasGroup.DOFade(1f, duration)
				.SetEase(TweenEase)
				.SetTarget(_tweenTarget)
				.SetUpdate(true)
				.OnComplete(() => { if (completeCallback != null) completeCallback(); });
		}

		public override void FadeOut(Action completeCallback = null)
		{
			FadeOut(DefaultDuration, completeCallback);
		}

		public override void FadeOut(float duration, Action completeCallback = null)
		{
			DOTween.Kill(_tweenTarget);

			// TODO: Bad hack (blocksRaycasts not work)
			if (_raycasters != null)
			{
				foreach (var raycaster in _raycasters)
				{
					if (raycaster != null) raycaster.enabled = true;
				}
				_raycasters = null;
			}

			_canvasGroup.alpha = 1f;
			gameObject.SetActive(true);
			_canvasGroup.blocksRaycasts = false; // not work
			_canvasGroup.DOFade(0f, duration)
				.SetDelay(FadeOutDelay)
				.SetEase(TweenEase)
				.SetTarget(_tweenTarget)
				.SetUpdate(true)
				.OnComplete(() =>
				{
					if (completeCallback != null) completeCallback();
					gameObject.SetActive(false);
				});
		}
	}
}
