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
	public class SpaceScrollerSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<PlayerInput> PlayerInput;
			public ComponentArray<Acceleration2D> Acceleration;
		}

		[Inject] private Data _data;

		private Background _background;


		protected override void OnStartRunning()
		{
			base.OnStartRunning();

			_background = GameObject.FindObjectOfType<Background>();
		}


		protected override void OnUpdate()
		{
			if (_data.Length == 0 || _background == null) return;

			float deltaTime = Time.deltaTime;
			var config = ConfigManager.Load<GameConfig>();

			for (int i = 0; i < _data.Length; i++)
			{
				_background.SpeedScale = -_data.Acceleration[i].Value * config.SpaceScrollSpeed;
			}
		}
	}
}
