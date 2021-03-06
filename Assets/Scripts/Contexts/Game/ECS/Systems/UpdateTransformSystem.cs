﻿using Scripts.Contexts.Game.ECS.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class UpdateTransformSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			[ReadOnly] public ComponentArray<Position2D> Position;
			[ReadOnly] public ComponentArray<Heading2D> Heading;
			public ComponentArray<Transform> Output;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			for (int i = 0; i < _data.Length; i++)
			{
				var angle = Mathf.Atan2(_data.Heading[i].Value.y, _data.Heading[i].Value.x) * Mathf.Rad2Deg - 90;
				var position = _data.Position[i].Value;
				_data.Output[i].position = new float3(position.x, position.y, 0f);
				_data.Output[i].rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}
		}
	}
}
