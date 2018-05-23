using UnityEngine;
using System.Collections;
using Unity.Entities;
using Scripts.Contexts.Game.ECS.Components;
using Unity.Mathematics;
using Scripts.Configs;
using Kernel.Core;
using UnityEngine.EventSystems;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class PlayerInputSystem : ComponentSystem
	{
		struct PlayerData
		{
			public PlayerInput Input;
		}

		public struct Data
		{
			public int Length;
			public ComponentArray<PlayerInput> Input;
			public ComponentArray<LaserEnergy> LaserEnergy;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			var deltaTime = Time.deltaTime;
			var playerConfig = ConfigManager.Load<PlayerConfig>();
			var projectilesConfig = ConfigManager.Load<ProjectilesConfig>();

			for (int i = 0; i < _data.Length; ++i)
			{
				var input = _data.Input[i];

				input.Axis.x = Input.GetAxis("Horizontal");
				input.Axis.y = Input.GetAxis("Vertical");

				var button1 = Input.GetButton("Fire1");
				var button2 = Input.GetButton("Fire2");

				if (!input.IsBulletFire)
				{
					if (!Input.GetMouseButton(0) || !EventSystem.current.IsPointerOverGameObject())
					{
						input.BulletCooldown = Mathf.Max(0.0f, input.BulletCooldown - deltaTime);
						if (input.BulletCooldown <= 0f && button1)
						{
							input.IsBulletFire = true;
							input.BulletCooldown = playerConfig.BulletShootCooldown;
						}
					}
				}

				if (!input.IsLaserFire)
				{
					if (!button1 && button2 && _data.LaserEnergy[i].Value >= projectilesConfig.LaserShootEnergy)
					{
						input.LaserPrepare += deltaTime;
						if (input.LaserPrepare >= playerConfig.LaserShootPrepare)
						{
							input.IsLaserFire = true;
							input.LaserPrepare = 0f;
						}
					}
					else
					{
						input.LaserPrepare = Mathf.Max(0.0f, input.LaserPrepare - deltaTime);
					}
				}
			}
		}
	}
}
