using UnityEngine;
using System.Collections;
using System.Linq;

namespace Scripts.Components
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class OrthoCamera : MonoBehaviour
	{
		#region Main Instance
		private static OrthoCamera _main;
		public static OrthoCamera Main
		{
			get
			{
				if (_main == null)
				{
					var cameras = FindObjectsOfType<OrthoCamera>();
					_main = cameras.FirstOrDefault(x => x.tag == Tags.MainCamera.ToString());
				}
				return _main;
			}
		}
		#endregion

		[SerializeField]
		private float _width = 1024f;
		[SerializeField]
		private float _height = 768f;
		[SerializeField]
		private float _referencePixelPerUnit = 16f;
		[SerializeField, Range(0f, 1f)]
		private float _match = 0f;
		[SerializeField]
		private float _scaleFactor = 1f;

		private Camera _camera;


		protected void Awake()
		{
			if (tag == Tags.MainCamera.ToString())
			{
				_main = this;
			}
		}

		public float ScaleFactor
		{
			get { return _scaleFactor; }
			set { _scaleFactor = value; }
		}

		protected void Update()
		{
			UpdateOrthoSize();
		}

		private void UpdateOrthoSize()
		{
			float width = _width / 2 / _referencePixelPerUnit / _scaleFactor * Ratio;
			float height = _height / 2 / _referencePixelPerUnit / _scaleFactor;
			Camera.orthographicSize = Mathf.Lerp(width, height, _match);
		}

		public Camera Camera
		{
			get
			{
				if (_camera == null) _camera = GetComponent<Camera>();
				return _camera;
			}
		}

		public float Ratio
		{
			get { return (float)Screen.height / (float)Screen.width; }
		}
	}
}
