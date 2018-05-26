using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class FollowPlayerSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<FollowPlayer> FollowPlayer;
			public ComponentArray<Position2D> Position;
			public ComponentArray<Heading2D> Heading;
		}

		public struct Players
		{
			public PlayerInput PlayerInput;
			public Position2D Position;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			var deltaTime = Time.deltaTime;

			var players = GetEntities<Players>();

			for (int i = 0; i < _data.Length; i++)
			{
				foreach (var player in players)
				{
					var direction = math.normalize(player.Position.Value - _data.Position[i].Value);
					if (direction.x != 0f && direction.y != 0f)
					{
						_data.Heading[i].Value = math.lerp(_data.Heading[i].Value, direction, deltaTime);
					}
					break;
				}
			}
		}
	}
}
