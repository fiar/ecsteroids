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
	public class PlayerTriggerSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<PlayerInput> PlayerInput;
			public ComponentArray<TriggerHandler2D> TriggerHandler;
			public ComponentArray<Position2D> Position;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			float deltaTime = Time.deltaTime;
			var gameConfig = ConfigManager.Load<GameConfig>();

			var toDestroy = new List<GameObject>();
			for (int i = 0; i < _data.Length; i++)
			{
				var gameObject = _data.TriggerHandler[i].Value;
				if (gameObject != null)
				{
					if (gameObject.GetComponent<EnemyBase>() != null)
					{
						toDestroy.Add(_data.TriggerHandler[i].gameObject);
						toDestroy.Add(gameObject);
						Lean.LeanPool.Spawn(
							gameConfig.Explosions[UnityEngine.Random.Range(0, gameConfig.Explosions.Length)],
							gameObject.transform.position,
							Quaternion.identity
						);

						// FSM Event
						SceneContext.TriggerEvent(Events.GameOver);
					}
				}
			}

			foreach (var go in toDestroy)
			{
				Lean.LeanPool.Despawn(go);
			}
		}
	}
}
