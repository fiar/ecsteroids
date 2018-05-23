using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;
using Scripts.Components;
using Scripts.Configs;
using Kernel.Core;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class ProjectileTriggerSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<Projectile> Projectile;
			public ComponentArray<TriggerHandler2D> TriggerHandler;
			public ComponentArray<Position2D> Position;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			float deltaTime = Time.deltaTime;

			var toDestroy = new List<GameObject>();
			for (int i = 0; i < _data.Length; i++)
			{
				var gameObject = _data.TriggerHandler[i].Value;
				if (gameObject != null)
				{
					if (gameObject.GetComponent<Asteroid>() != null)
					{
						toDestroy.Add(_data.TriggerHandler[i].gameObject);
						toDestroy.Add(gameObject);

						if (!_data.Projectile[i].Laser)
						{
							var asteroid = gameObject.GetComponent<Asteroid>();
							if (asteroid.Big)
							{
								DestructBigAsteroid(
									gameObject.GetComponent<Position2D>().Value,
									gameObject.GetComponent<Heading2D>().Value
								);
							}
						}
					}
					else if (gameObject.GetComponent<Enemy>() != null)
					{
						toDestroy.Add(_data.TriggerHandler[i].gameObject);
						toDestroy.Add(gameObject);
					}
					_data.TriggerHandler[i].Value = null;
				}
			}

			foreach (var go in toDestroy)
			{
				Lean.LeanPool.Despawn(go);
			}
		}

		// TODO: move to asteroids spawner
		private void DestructBigAsteroid(float2 position, float2 heading)
		{
			var config = ConfigManager.Load<AsteroidsConfig>();

			var idx = UnityEngine.Random.Range(0, config.AsteroidsL.Length);
			var prefab = config.AsteroidsL[idx];

			for (int i = 0; i < 2; i++)
			{
				var asteroid = Lean.LeanPool.Spawn(prefab, (Vector2)position, Quaternion.identity);
				asteroid.GetComponent<Asteroid>().Big = false;
				asteroid.GetComponent<MoveSpeed>().Value = UnityEngine.Random.Range(config.MoveSpeedMinMax.x, config.MoveSpeedMinMax.y);
				asteroid.GetComponent<Position2D>().Value = position;
				asteroid.GetComponent<Heading2D>().Value = heading + (float2)UnityEngine.Random.insideUnitCircle.normalized;
			}
		}
	}
}
