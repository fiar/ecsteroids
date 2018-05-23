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
		//private Background _background;
		//private PlayerShip _playerShip;

		//private AsteroidsSpawner _asteroidsSpawner;


		protected void Awake()
		{
			var playerConfig = ConfigManager.Load<PlayerConfig>();

			var player = ResourcesUtilities.InstantiateFromResource<GameObject>(playerConfig.ResourcePath);

			//_background = GameObject.FindObjectOfType<Background>();
			//Debug.Assert(_background != null, "Background not exists at scene");

			//_playerShip = GameObject.FindObjectOfType<PlayerShip>();
			//Debug.Assert(_playerShip != null, "PlayerShip not exists at scene");

			//_asteroidsSpawner = new AsteroidsSpawner();
		}

		protected void OnEnter()
		{
		}

		protected void Update()
		{
			// Cool scroll background
			//if (_background != null)
			//	_background.SpeedScale = _playerShip.ShipVelocity.Velocity;

			//_playerShip.UpdateShip();
			//_asteroidsSpawner.UpdateSpawner();
		}
	}
}
