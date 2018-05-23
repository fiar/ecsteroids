using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;
using Kernel.Core;
using Scripts.Configs;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class PlayerAccelerationSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<Acceleration2D> Acceleration;
			public ComponentArray<Heading2D> Heading;
			public ComponentArray<PlayerInput> Input;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			float deltaTime = Time.deltaTime;
			var config = ConfigManager.Load<PlayerConfig>();

			for (int i = 0; i < _data.Length; i++)
			{
				if (_data.Input[i].Axis.y > 0f)
				{
					_data.Acceleration[i].Value += _data.Input[i].Axis.y * _data.Heading[i].Value * config.Acceleration * deltaTime;
				}
				else
				{
					_data.Acceleration[i].Value = math.lerp(_data.Acceleration[i].Value, new float2(0f, 0f), config.Deceleration * Time.deltaTime);
				}
			}
		}
	}
}
