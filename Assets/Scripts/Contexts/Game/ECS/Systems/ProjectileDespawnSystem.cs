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
	public class ProjectileDespawnSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<Projectile> Projectile;
			public ComponentArray<Position2D> Position;
		}

		[Inject] private Data _data;

		private Bounds _bounds;


		protected override void OnStartRunning()
		{
			base.OnStartRunning();

			var config = ConfigManager.Load<GameConfig>();

			_bounds = OrthoCamera.Main.Camera.CalculateOrthographicBounds(config.BoundsOuterExpand);
		}

		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			float deltaTime = Time.deltaTime;

			var toDestroy = new List<GameObject>();
			for (int i = 0; i < _data.Length; i++)
			{
				if (!_bounds.Contains((Vector2)_data.Position[i].Value))
				{
					toDestroy.Add(_data.Projectile[i].gameObject);
				}
			}

			foreach (var go in toDestroy)
			{
				Lean.LeanPool.Despawn(go);
			}
		}
	}
}
