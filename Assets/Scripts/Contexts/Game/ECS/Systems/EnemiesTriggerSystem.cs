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
	public class EnemiesTriggerSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<EnemyBase> PlayerInput;
			public ComponentArray<TriggerHandler2D> TriggerHandler;
			public ComponentArray<Position2D> Position;
			public ComponentArray<Heading2D> Heading;
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
					if (gameObject.GetComponent<EnemyBase>() != null)
					{
						var p = (Vector2)_data.Position[i].Value - (Vector2)gameObject.transform.position;
						_data.Heading[i].Value = p.normalized;
						_data.TriggerHandler[i].Value = null;
					}
				}
			}
		}
	}
}
