using UnityEngine;
using System.Collections;
using Kernel.Core;

namespace Scripts.Configs
{
	[CreateAssetMenu(menuName = "Configs/Player", order = Config.Order)]
	public class PlayerConfig : Config
	{
		public string ResourcePath;
		public float Acceleration = 10f;
		public float Deceleration = 10f;
		public float Torque = 180f;
	}
}
