using UnityEngine;
using System.Collections;
using Kernel.Core;

namespace Scripts.Configs
{
	[CreateAssetMenu(menuName = "Configs/Game", order = Config.Order)]
	public class GameConfig : Config
	{
		public Vector3 BoundsLimitExpand = Vector3.zero;
		public Vector3 BoundsRepeatExpand = Vector3.zero;
		public float SpaceScrollMinSpeed = 0.1f;
		public float SpaceScrollSpeed = 1f;
	}
}
