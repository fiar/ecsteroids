using UnityEngine;
using System.Collections;

namespace Scripts.Components
{
	// TODO: replace with something better
	public class ZeroSimpleRotation : MonoBehaviour
	{

		protected void Update()
		{
			transform.rotation = Quaternion.identity;
		}
	}
}
