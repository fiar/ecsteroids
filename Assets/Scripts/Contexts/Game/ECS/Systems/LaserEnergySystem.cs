using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;
using Kernel.Core;
using Scripts.Configs;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class LaserEnergySystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<LaserEnergy> LaserEnergy;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			var deltaTime = Time.deltaTime;
			var config = ConfigManager.Load<ProjectilesConfig>();

			for (int i = 0; i < _data.Length; i++)
			{
				_data.LaserEnergy[i].Value = Mathf.Clamp01(_data.LaserEnergy[i].Value + config.LaserRestoreSpeed * deltaTime);
			}
		}
	}
}
