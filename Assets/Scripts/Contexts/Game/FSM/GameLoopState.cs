using UnityEngine;
using System.Collections;
using Kernel.StateMachine;
using Scripts.Components;
using Scripts.Utils;
using Kernel.Core;
using Scripts.Configs;
using Scripts.Contexts.Game.ECS.Components;
using Unity.Mathematics;

namespace Scripts.Contexts.Game.FSM
{
	public class GameLoopState : ConcreteState
	{

		protected void Awake()
		{
			var playerConfig = ConfigManager.Load<PlayerConfig>();

			// Create Player
			var player = GameObject.Instantiate<GameObject>(playerConfig.Prefab);
			player.GetComponent<Heading2D>().Value = new float2(0f, 1f);
			player.GetComponent<LaserEnergy>().Value = 1f;
		}

		protected void OnEnter()
		{
		}
	}
}
