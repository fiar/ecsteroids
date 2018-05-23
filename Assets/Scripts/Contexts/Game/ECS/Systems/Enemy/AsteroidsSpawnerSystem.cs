using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;
using Kernel.Core;
using Scripts.Configs;
using Scripts.Components;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class AsteroidsSpawnerSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<AsteroidsSpawner> AsteroidsSpawner;
		}

		[Inject] private Data _data;

		private float _timer;
		private Bounds _bounds;


		protected override void OnStartRunning()
		{
			base.OnStartRunning();

			var config = ConfigManager.Load<GameConfig>();

			_bounds = OrthoCamera.Main.Camera.CalculateOrthographicBounds(config.BoundsOuterExpand);
		}

		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			var deltaTime = Time.deltaTime;
			var asteroidsConfig = ConfigManager.Load<AsteroidsConfig>();

			_timer -= deltaTime;

			if (_timer <= 0f)
			{
				_timer += asteroidsConfig.SpawnPeriod;

				for (int i = 0; i < _data.Length; i++)
				{
					CreateAsteroid(asteroidsConfig);
				}
			}
		}

		private void CreateAsteroid(AsteroidsConfig config)
		{
			var idx = UnityEngine.Random.Range(0, config.AsteroidsXL.Length);
			var prefab = config.AsteroidsXL[idx];

			Vector2 position = Vector2.zero;
			if (UnityEngine.Random.value > 0.5f)
			{
				position.x = -_bounds.size.x * 0.5f;
				position.y = UnityEngine.Random.Range(-0.5f, 0.5f) * _bounds.size.y;
			}
			else
			{
				position.x = UnityEngine.Random.Range(-0.5f, 0.5f) * _bounds.size.x;
				position.y = -_bounds.size.y * 0.5f;
			}

			var rotateSign = Mathf.Sign(UnityEngine.Random.Range(-1f, 1f));

			var asteroid = Lean.LeanPool.Spawn(prefab, (Vector2)position, Quaternion.identity);
			asteroid.GetComponent<Asteroid>().Big = true;
			asteroid.GetComponent<MoveSpeed>().Value = UnityEngine.Random.Range(config.MoveSpeedMinMax.x, config.MoveSpeedMinMax.y);
			asteroid.GetComponent<Position2D>().Value = position;
			asteroid.GetComponent<Heading2D>().Value = UnityEngine.Random.insideUnitCircle.normalized;
		}
	}
}
