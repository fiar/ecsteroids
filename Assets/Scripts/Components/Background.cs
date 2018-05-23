using UnityEngine;
using System.Collections;
using Kernel.Core;
using Scripts.Configs;

namespace Scripts.Components
{
	public class Background : MonoBehaviour
	{
		private ScrollUV[] _scrollUVs;
		private float _minSpeed;

		protected void Awake()
		{
			_scrollUVs = GetComponentsInChildren<ScrollUV>();

			_minSpeed = ConfigManager.Load<GameConfig>().SpaceScrollMinSpeed;
			SpeedScale = UnityEngine.Random.insideUnitCircle.normalized * _minSpeed;
		}

		public Vector2 SpeedScale
		{
			set
			{
				if (value.magnitude >= _minSpeed)
				{
					foreach (var scroll in _scrollUVs)
					{
						scroll.SpeedScale = value;
					}
				}
			}
		}
	}
}
