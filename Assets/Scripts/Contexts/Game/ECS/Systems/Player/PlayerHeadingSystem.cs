using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;
using Kernel.Core;
using Scripts.Configs;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class PlayerHeadingSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
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
				_data.Heading[i].Angle -= _data.Input[i].Axis.x * config.Torque * deltaTime;

				var up = Quaternion.AngleAxis(_data.Heading[i].Angle, Vector3.forward) * Vector3.up;
				_data.Heading[i].Value = new float2(up.x, up.y);
			}
		}
	}
}
