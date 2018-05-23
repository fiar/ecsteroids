using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;
using Scripts.Components;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class SceneBoundsRepeatSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<SceneBoundsRepeat> SceneBoundsRepeat;
			public ComponentArray<Position2D> Position;
		}

		[Inject] private Data _data;

		private Vector2 _size;
		private Vector2 _halfSize;


		protected override void OnStartRunning()
		{
			base.OnStartRunning();

			var bounds = OrthoCamera.Main.Camera.CalculateOrthographicBounds(new Vector3(2f, 2f, 0f));
			_size = bounds.size;
			_halfSize = _size * 0.5f;
		}

		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			float deltaTime = Time.deltaTime;

			for (int i = 0; i < _data.Length; i++)
			{
				var v = _data.Position[i].Value;
				v.x = Mathf.Repeat(v.x + _halfSize.x, _size.x) - _halfSize.x;
				v.y = Mathf.Repeat(v.y + _halfSize.y, _size.y) - _halfSize.y;
				_data.Position[i].Value = v;
			}
		}
	}
}
