using Kernel.UI.Behaviours;
using System;
using UnityEngine;

namespace Kernel.UI
{
	[RequireComponent(typeof(Canvas))]
	public class UIBehaviour : MonoBehaviour
	{
		[SerializeField]
		private bool _disableChildsOnStart;
		[Space]
		[SerializeField]
		private RectTransform _container;
		[SerializeField]
		private ScreenFader _screenFaderPrefab;
		[SerializeField]
		private DialogFader _dialogFaderPrefab;

		protected void Awake()
		{
			Canvas = GetComponent<Canvas>();

#if UNITY_EDITOR
			if (_disableChildsOnStart)
			{
				foreach (Transform t in transform)
				{
					t.gameObject.SetActive(false);
				}
			}
#endif

			if (_screenFaderPrefab != null)
			{
				ScreenFader = Instantiate<ScreenFader>(_screenFaderPrefab);
				ScreenFader.name = "~ScreenFader";
				DontDestroyOnLoad(ScreenFader);
				ScreenFader.gameObject.SetActive(false);
			}

			if (_dialogFaderPrefab != null)
			{
				DialogFader = Instantiate<DialogFader>(_dialogFaderPrefab);
				DialogFader.name = "~DialogFader";
				DialogFader.gameObject.SetActive(false);
				DontDestroyOnLoad(DialogFader);
			}
		}

		public Canvas Canvas { get; private set; }

		public ScreenFader ScreenFader { get; private set; }

		public DialogFader DialogFader { get; private set; }

		public RectTransform Container
		{
			get { return _container; }
		}

#if UNITY_EDITOR
		protected void Reset()
		{
			if (_container == null)
				_container = transform as RectTransform;
		}
#endif
	}
}
