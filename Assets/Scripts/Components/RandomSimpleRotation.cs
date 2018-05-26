using UnityEngine;
using System.Collections;
using Kernel.Core;
using Scripts.Configs;

namespace Scripts.Components
{
	public class RandomSimpleRotation : MonoBehaviour
	{
		private float _angle;
		private float _speed;


		protected void OnEnable()
		{
			_angle = UnityEngine.Random.value * 360f;
			_speed = UnityEngine.Random.Range(10f, 100f) * Mathf.Sign(UnityEngine.Random.Range(-1f, 1f));
		}

		protected void LateUpdate()
		{
			_angle += _speed * Time.deltaTime;
			transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
		}
	}
}
