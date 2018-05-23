using UnityEngine;
using System.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Scripts.Contexts.Game.ECS.Components
{
	public class PlayerInput : MonoBehaviour
	{
		public float2 Axis;

		public float BulletCooldown;
		public bool IsBulletFire;

		public float LaserPrepare;
		public bool IsLaserFire;
	}
}
