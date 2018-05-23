using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;
using Scripts.Components;
using Kernel.Core;
using Scripts.Configs;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class SceneBoundsLimitSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<SceneBoundsLimit> SceneBoundsLimit;
			public ComponentArray<Position2D> Position;
			public ComponentArray<Acceleration2D> Acceleration;
		}

		[Inject] private Data _data;

		private Vector2 _halfSize;


		protected override void OnStartRunning()
		{
			base.OnStartRunning();

			var config = ConfigManager.Load<GameConfig>();

			var bounds = OrthoCamera.Main.Camera.CalculateOrthographicBounds(config.BoundsInnerExpand);
			_halfSize = bounds.size * 0.5f;
		}

		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			float deltaTime = Time.deltaTime;

			for (int i = 0; i < _data.Length; i++)
			{
				var v = _data.Position[i].Value;
				v.x = math.clamp(v.x, -_halfSize.x, _halfSize.x);
				v.y = math.clamp(v.y, -_halfSize.y, _halfSize.y);
				_data.Position[i].Value = v;

				var acceleration = _data.Acceleration[i].Value;
				if (_halfSize.x - math.abs(v.x) == 0f) acceleration.x = 0f;
				if (_halfSize.y - math.abs(v.y) == 0f) acceleration.y = 0f;
				_data.Acceleration[i].Value = acceleration;
			}
		}
	}
}
