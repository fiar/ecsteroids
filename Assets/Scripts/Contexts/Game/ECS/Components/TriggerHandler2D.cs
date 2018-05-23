using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Scripts.Contexts.Game.ECS.Components
{
	public class TriggerHandler2D : MonoBehaviour
	{
		public GameObject Value;


		protected void OnTriggerEnter2D(Collider2D collision)
		{
			Value = collision.gameObject;
		}
	}
}