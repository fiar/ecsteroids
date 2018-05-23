using System;
using UnityEngine;
using System.Collections;
using Kernel.UI.Tweens;
using DG.Tweening;
using System.Collections.Generic;

namespace Kernel.UI.Behaviours
{
	public class SimpleDialogFader : DialogFader
	{
		public const float DefaultDuration = 1f;
		public const float FadeOutDelay = 0.1f;
		public const Ease TweenEase = Ease.OutQuad;

		[SerializeField]
		private CanvasGroup _primaryFader;
		[SerializeField]
		private CanvasGroup _secondaryFader;

		private Dictionary<Form, CanvasGroup> _faders = new Dictionary<Form, CanvasGroup>();


		protected void Awake()
		{
		}

		public override void FadeIn(Form form, Action completeCallback = null)
		{
			FadeIn(form, DefaultDuration, completeCallback);
		}

		public override void FadeIn(Form form, float duration, Action completeCallback = null)
		{
			var fader = GameObject.Instantiate<CanvasGroup>(_faders.Count == 0 ? _primaryFader : _secondaryFader);
			fader.transform.SetParent(form.transform.parent, false);
			_faders.Add(form, fader);

			var sibling = form.transform.GetSiblingIndex();
			fader.transform.SetSiblingIndex(sibling);

			fader.alpha = 0f;
			fader.blocksRaycasts = true;
			gameObject.SetActive(true);
			fader.DOFade(1f, duration)
				.SetEase(TweenEase)
				.SetTarget(fader)
				.SetUpdate(true)
				.OnComplete(() => { if (completeCallback != null) completeCallback(); });
		}

		public override void FadeOut(Form form, Action completeCallback = null)
		{
			FadeOut(form, DefaultDuration, completeCallback);
		}

		public override void FadeOut(Form form, float duration, Action completeCallback = null)
		{
			Debug.Assert(_faders.ContainsKey(form));

			if (_faders.ContainsKey(form))
			{
				var fader = _faders[form];

				DOTween.Kill(fader);

				gameObject.SetActive(true);
				fader.blocksRaycasts = false;
				fader.DOFade(0f, duration)
					.SetDelay(FadeOutDelay)
					.SetEase(TweenEase)
					.SetTarget(fader)
					.SetUpdate(true)
					.OnComplete(() =>
					{
						if (completeCallback != null) completeCallback();
						Destroy(fader.gameObject);
					});
				_faders.Remove(form);
			}
		}
	}
}
