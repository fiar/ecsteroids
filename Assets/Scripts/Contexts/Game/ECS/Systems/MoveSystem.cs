using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class MoveSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<MoveSpeed> MoveSpeed;
			public ComponentArray<Position2D> Position;
			public ComponentArray<Heading2D> Heading;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			var deltaTime = Time.deltaTime;

			for (int i = 0; i < _data.Length; i++)
			{
				_data.Position[i].Value += _data.Heading[i].Value * _data.MoveSpeed[i].Value * deltaTime;
			}
		}
	}
}
