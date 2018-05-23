using UnityEngine;
using System.Collections;
using Unity.Entities;
using Scripts.Contexts.Game.ECS.Components;
using Unity.Mathematics;

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
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			var deltaTime = Time.deltaTime;

			for (int index = 0; index < _data.Length; ++index)
			{
				var input = _data.Input[index];

				input.Axis.x = Input.GetAxis("Horizontal");
				input.Axis.y = Input.GetAxis("Vertical");
				input.Shoot.x = Input.GetAxis("Horizontal");
				input.Shoot.y = Input.GetAxis("Vertical");

				input.FireCooldown = Mathf.Max(0.0f, input.FireCooldown - deltaTime);
			}
		}
	}
}
