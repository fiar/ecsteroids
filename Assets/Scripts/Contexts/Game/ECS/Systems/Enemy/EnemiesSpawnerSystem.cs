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
	// TODO: its clone of Asteroids spawner (fix this)
	public class EnemiesSpawnerSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<EnemiesSpawner> EnemiesSpawner;
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
			var enemiesConfig = ConfigManager.Load<EnemiesConfig>();

			_timer += deltaTime;

			if (_timer >= enemiesConfig.SpawnPeriod)
			{
				_timer = 0;

				for (int i = 0; i < _data.Length; i++)
				{
					CreateEnemy(enemiesConfig);
				}
			}
		}

		private void CreateEnemy(EnemiesConfig config)
		{
			var idx = UnityEngine.Random.Range(0, config.Enemies.Length);
			var prefab = config.Enemies[idx];

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

			var enemy = Lean.LeanPool.Spawn(prefab, (Vector2)position, Quaternion.identity);
			enemy.GetComponent<MoveSpeed>().Value = UnityEngine.Random.Range(config.MoveSpeedMinMax.x, config.MoveSpeedMinMax.y);
			enemy.GetComponent<Position2D>().Value = position;
			enemy.GetComponent<Heading2D>().Value = UnityEngine.Random.insideUnitCircle.normalized;
		}
	}
}
