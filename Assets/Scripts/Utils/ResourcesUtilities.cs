using System;
using UnityEngine;

namespace Scripts.Utils
{
	public static class ResourcesUtilities
	{

		public static T InstantiateFromResource<T>(string path, Transform parent = null, bool worldPositionsStay = true) where T : UnityEngine.Object
		{
			var resource = Resources.Load<T>(path);
			if (resource == null) return null;
			var obj = GameObject.Instantiate<T>(resource, parent, worldPositionsStay);
			obj.name = resource.name;
			return obj;
		}

		public static T InstantiateFromResource<T>(string path, Vector3 position, Quaternion rotation, Transform parent = null) where T : UnityEngine.Object
		{
			var resource = Resources.Load<T>(path);
			if (resource == null) return null;
			var obj = GameObject.Instantiate<T>(resource, position, rotation, parent);
			obj.name = resource.name;
			return obj;
		}
	}
}
