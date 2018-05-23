using UnityEngine;
using System.Collections;

namespace Scripts.Components
{
	public class SceneBounds : MonoBehaviour
	{
		[SerializeField]
		private Transform _leftBorder;
		[SerializeField]
		private Transform _rightBorder;
		[SerializeField]
		private Transform _topBorder;
		[SerializeField]
		private Transform _bottomBorder;


		protected void Awake()
		{
			var orthoCamera = OrthoCamera.Main;
			Debug.Assert(orthoCamera != null, "Main OrthoCamera not exists");

			if (orthoCamera != null)
			{
				UpdateBounds(orthoCamera.Camera);
			}
		}

		private void UpdateBounds(Camera camera)
		{
			var bounds = camera.CalculateOrthographicBounds();

			_leftBorder.position = Vector3.left * bounds.size.x * 0.5f;
			_rightBorder.position = Vector3.right * bounds.size.x * 0.5f;
			_leftBorder.localScale = _rightBorder.localScale = new Vector3(1f, bounds.size.y, 1f);

			_topBorder.position = Vector3.up * bounds.size.y * 0.5f;
			_bottomBorder.position = Vector3.down * bounds.size.y * 0.5f;
			_topBorder.localScale = _bottomBorder.localScale = new Vector3(bounds.size.x, 1f, 1f);
		}
	}
}
