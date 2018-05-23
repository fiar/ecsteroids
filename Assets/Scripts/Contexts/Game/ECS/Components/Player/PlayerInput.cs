using UnityEngine;
using System.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Scripts.Contexts.Game.ECS.Components
{
	public class PlayerInput : MonoBehaviour
	{
		public float2 Axis;
		public float2 Shoot;
		public float FireCooldown;

		public bool Fire => FireCooldown <= 0.0 && math.length(Shoot) > 0.5f;
	}
}
