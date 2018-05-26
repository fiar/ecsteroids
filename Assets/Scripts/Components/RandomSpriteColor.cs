using UnityEngine;
using System.Collections;
using Kernel.Core;
using Scripts.Configs;

namespace Scripts.Components
{
	public class RandomSpriteColor : MonoBehaviour
	{
		[SerializeField]
		private float _s = 1f;
		[SerializeField]
		private float _v = 1f;

		private float _h;

		private SpriteRenderer _renderer;


		protected void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
		}

		protected void OnEnable()
		{
			_h = UnityEngine.Random.value;
			_renderer.color = Color.HSVToRGB(_h, _s, _v);
		}

		protected void LateUpdate()
		{
			_h = Mathf.Repeat(_h + Time.deltaTime * 0.02f, 1f);
			_renderer.color = Color.HSVToRGB(_h, _s, _v);
		}
	}
}
