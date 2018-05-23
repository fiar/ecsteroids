using Kernel.Core;
using Kernel.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Contexts.Entry
{
	public class EntryContext : MonoBehaviour
	{

		protected IEnumerator Start()
		{
			// Stop, if Kernel already loaded (editor only situation)
			if (KernelApplication.IsInitialized)
			{
				Debug.LogError("KernelApplication already executed");
				yield break;
			}

			// First load Kernel Scene, then Lobby
			yield return SceneManager.LoadSceneAsync(Scenes.Kernel, LoadSceneMode.Additive);
			while (!KernelApplication.IsLoaded) yield return null;
			yield return SceneManager.LoadSceneAsync(Scenes.Lobby, LoadSceneMode.Additive);

			// Fadeout screen and unload current scene
			SceneManager.UnloadSceneAsync(gameObject.scene);
		}
	}
}
