using UnityEngine;
using System.Collections;
using Kernel.Core;

namespace Scripts.Configs
{
	[CreateAssetMenu(menuName = "Configs/Player", order = Config.Order)]
	public class PlayerConfig : Config
	{
		public GameObject Prefab;
		public float Acceleration = 10f;
		public float Deceleration = 10f;
		public float Torque = 180f;
		public float BulletShootCooldown = 0.5f;
		public float LaserShootPrepare = 1f;
	}
}
