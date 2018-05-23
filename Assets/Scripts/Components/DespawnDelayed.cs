using System;
using UnityEngine;

namespace Scripts.Components
{

	public class DespawnDelayed : MonoBehaviour
	{
		[SerializeField]
		private float _delay = 1f;

		private float _timer;


		protected void OnEnable()
		{
			_timer = 0f;
		}

		protected void Update()
		{
			_timer += Time.deltaTime;
			if (_timer >= _delay)
			{
				Lean.LeanPool.Despawn(gameObject);
			}
		}
	}
}
