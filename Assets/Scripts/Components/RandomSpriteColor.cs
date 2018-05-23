using UnityEngine;
using System.Collections;
using Kernel.Core;
using Scripts.Configs;

namespace Scripts.Components
{
	public class RandomSpriteColor : MonoBehaviour
	{
		[SerializeField]
		protected float _s = 1f;
		[SerializeField]
		protected float _v = 1f;

		private SpriteRenderer _renderer;


		protected void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
		}

		protected void OnEnable()
		{
			_renderer.color = Color.HSVToRGB(UnityEngine.Random.value, _s, _v);
		}
	}
}
