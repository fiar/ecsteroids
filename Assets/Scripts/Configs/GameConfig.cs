using UnityEngine;
using System.Collections;
using Kernel.Core;

namespace Scripts.Configs
{
	[CreateAssetMenu(menuName = "Configs/Game", order = Config.Order)]
	public class GameConfig : Config
	{
		public Vector3 BoundsInnerExpand = Vector3.zero;
		public Vector3 BoundsOuterExpand = Vector3.zero;
		public float SpaceScrollMinSpeed = 0.1f;
		public float SpaceScrollSpeed = 1f;
	}
}
