using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Scripts.Contexts.Game.ECS.Components;
using System.Collections.Generic;

namespace Scripts.Contexts.Game.ECS.Systems
{
	public class GlowPlayerSystem : ComponentSystem
	{
		public struct Data
		{
			public int Length;
			public ComponentArray<Glow> Glow;
			public ComponentArray<SpriteRenderer> SpriteRenderer;
		}

		[Inject] private Data _data;


		protected override void OnUpdate()
		{
			if (_data.Length == 0) return;

			var deltaTime = Time.deltaTime;

			for (int i = 0; i < _data.Length; i++)
			{
				// TODO: so ugly
				var force = _data.SpriteRenderer[i].transform.GetComponentInParent<Acceleration2D>().Value / 10f;

				var color = _data.SpriteRenderer[i].color;
				color.a = math.length(force);
				_data.SpriteRenderer[i].color = color;
			}
		}
	}
}
