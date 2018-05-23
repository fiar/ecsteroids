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
		public float SpawnPeriod = 6f;
		public Vector2 MoveSpeedMinMax = Vector2.one;
	}
}
