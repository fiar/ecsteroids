using System;
using UnityEngine;

namespace Scripts
{
	public static class Layers
	{
		// Builtin Layer 0
		public static int Default { get { return 0; } }
		// Builtin Layer 1
		public static int TransparentFX { get { return 1; } }
		// Builtin Layer 2
		public static int IgnoreRaycast { get { return 2; } }
		// Builtin Layer 3
		//public static int BuiltinLayer3 { get { return 3; } }
		// Builtin Layer 4
		public static int Water { get { return 4; } }
		// Builtin Layer 5
		public static int UI { get { return 5; } }
		// Builtin Layer 6
		//public static int BuiltinLayer6 { get { return 6; } } 
		// Builtin Layer 7
		//public static int BuiltinLayer7 { get { return 7; } } 
		// SceneBounds
		public static int SceneBounds { get { return 8; } } 
	}

	public static class LayerNames
	{
		// Builtin Layer 0
		public static string Default { get { return LayerMask.LayerToName(0); } }
		// Builtin Layer 1
		public static string TransparentFX { get { return LayerMask.LayerToName(1); } }
		// Builtin Layer 2
		public static string IgnoreRaycast { get { return LayerMask.LayerToName(2); } }
		// Builtin Layer 3
		//public static string BuiltinLayer3 { get { return LayerMask.LayerToName(3); } }
		// Builtin Layer 4
		public static string Water { get { return LayerMask.LayerToName(4); } }
		// Builtin Layer 5
		public static string UI { get { return LayerMask.LayerToName(5); } }
		// Builtin Layer 6
		//public static string BuiltinLayer6 { get { return LayerMask.LayerToName(6); } } 
		// Builtin Layer 7
		//public static string BuiltinLayer7 { get { return LayerMask.LayerToName(7); } } 
		// SceneBounds
		public static string SceneBounds { get { return LayerMask.LayerToName(8); } } 
	}
}
