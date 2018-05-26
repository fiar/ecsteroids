using UnityEngine;
using System.Collections;

namespace Scripts.Components
{
	// TODO: replace with something better
	public class ZeroSimpleRotation : MonoBehaviour
	{

		protected void LateUpdate()
		{
			transform.rotation = Quaternion.identity;
		}
	}
}
