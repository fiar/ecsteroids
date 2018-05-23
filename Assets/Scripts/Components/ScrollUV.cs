using UnityEngine;
using System.Collections;

namespace Scripts.Components
{
	public class ScrollUV : MonoBehaviour
	{
		[SerializeField]
		private float _baseSpeed = 1f;

		private Material _material;


		public Vector2 SpeedScale { get; set; }


		protected void Awake()
		{
			var renderer = GetComponent<Renderer>();
			Debug.Assert(renderer != null, "Require a Renderer component");
			if (renderer != null)
			{
				_material = renderer.material;
			}
		}

		protected void LateUpdate()
		{
			if (_material != null)
			{
				var offset = _material.GetTextureOffset("_MainTex");
				offset.x += _baseSpeed * SpeedScale.x * Time.deltaTime;
				offset.y += _baseSpeed * SpeedScale.y * Time.deltaTime;
				_material.SetTextureOffset("_MainTex", offset);
			}
		}
	}
}
