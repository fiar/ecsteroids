using UnityEngine;
using System.Collections;
using Kernel.Core;

namespace Scripts.Configs
{
	[CreateAssetMenu(menuName = "Configs/Projectiles", order = Config.Order)]
	public class ProjectilesConfig : Config
	{
		public GameObject BulletPrefab;
		public float BulletSpeed = 1f;

		[Space]
		public GameObject LaserPrefab;
		public float LaserSpeed = 1f;
		public float LaserShootEnergy = 0.2f;
		public float LaserRestoreSpeed = 3f;
	}
}
