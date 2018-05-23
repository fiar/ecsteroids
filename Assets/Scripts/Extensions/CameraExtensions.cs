using System;

namespace UnityEngine
{
	public static class CameraExtensions
	{
		public static Bounds CalculateOrthographicBounds(this Camera camera)
		{
			float screenAspect = (float)Screen.width / (float)Screen.height;
			float cameraHeight = camera.orthographicSize * 2;
			return new Bounds(
				camera.transform.position,
				new Vector3(cameraHeight * screenAspect, cameraHeight, 0)
			);
		}

		public static Bounds CalculateOrthographicBounds(this Camera camera, Vector3 expandAmount)
		{
			var bounds = camera.CalculateOrthographicBounds();
			bounds.Expand(expandAmount);
			return bounds;
		}
	}
}
