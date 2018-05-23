using UnityEngine;
using System.Collections;
using Kernel.Core;

namespace Scripts.Configs
{
	[CreateAssetMenu(menuName = "Configs/Enemies", order = Config.Order)]
	public class EnemiesConfig : Config
	{
		public GameObject[] Enemies;

		[Space]
		public float SpawnPeriod = 6f;
		public Vector2 MoveSpeedMinMax = Vector2.one;
	}
}
