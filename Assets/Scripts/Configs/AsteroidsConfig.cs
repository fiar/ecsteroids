using UnityEngine;
using System.Collections;
using Kernel.Core;

namespace Scripts.Configs
{
	[CreateAssetMenu(menuName = "Configs/Asteroids", order = Config.Order)]
	public class AsteroidsConfig : Config
	{
		public GameObject[] AsteroidsXL;
		public GameObject[] AsteroidsL;

		[Space]
		public float AsteroidsSpawnPeriod = 6f;
		public Vector2 MoveSpeedMinMax = Vector2.one;
		public Vector2 AngularSpeedMinMax = Vector2.one;
		public int AsteroidHealth = 1;
		public int UFOHealth = 3;
	}
}
