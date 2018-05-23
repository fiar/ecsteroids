using System;
using UnityEngine;

namespace Kernel.Core
{
	[CreateAssetMenu(menuName = "Configs/KernelLoader", order = Config.Order)]
	public class KernelLoaderConfig : Config
	{
#if UNITY_EDITOR
		public UnityEditor.SceneAsset KernelScene;
#endif

		[Space]
		[SerializeField, Multiline]
		private string _scenesPattern;

		#region Properties
		public string ScenesPattern { get { return _scenesPattern; } }
		#endregion
	}
}
