using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;
using Kernel.Core;
using Scripts.Configs;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class PlayerShootSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<PlayerInput> PlayerInput;
			public ComponentArray<Position2D> Position;
			public ComponentArray<Heading2D> Heading;
			public ComponentArray<LaserEnergy> LaserEnergy;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			var deltaTime = Time.deltaTime;
			var playerConfig = ConfigManager.Load<PlayerConfig>();
			var projectilesConfig = ConfigManager.Load<ProjectilesConfig>();

			for (int i = 0; i < _data.Length; i++)
			{
				var input = _data.PlayerInput[i];
				if (input.IsBulletFire)
				{
					input.IsBulletFire = false;

					CreateBullet(
						projectilesConfig.BulletPrefab, projectilesConfig.BulletDamage, projectilesConfig.BulletSpeed,
						_data.Position[i].Value, _data.Heading[i].Value
					);
				}

				if (input.IsLaserFire)
				{
					input.IsLaserFire = false;

					if (_data.LaserEnergy[i].Value >= projectilesConfig.LaserShootEnergy)
					{
						_data.LaserEnergy[i].Value -= projectilesConfig.LaserShootEnergy;

						CreateBullet(
							projectilesConfig.LaserPrefab, projectilesConfig.LaserDamage, projectilesConfig.LaserSpeed,
							_data.Position[i].Value, _data.Heading[i].Value
						);
					}
				}
			}
		}

		private void CreateBullet(GameObject prefab, int damage, float speed, float2 position, float2 heading)
		{
			var bullet = Lean.LeanPool.Spawn(prefab, (Vector2)position, Quaternion.identity);
			bullet.GetComponent<Bullet>().Damage = damage;
			bullet.GetComponent<MoveSpeed>().Value = speed;
			bullet.GetComponent<Position2D>().Value = position;
			bullet.GetComponent<Heading2D>().Value = heading;
		}
	}
}
