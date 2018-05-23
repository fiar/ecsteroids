using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kernel.Core
{
	[DefaultExecutionOrder(-1001)]
	public class KernelApplication : MonoBehaviour
	{
		// Version
		public const string Version = "1.0b1";

		public static bool IsInitialized { get { return _instance != null; } }
		public static bool IsLoaded { get; private set; }

		private static KernelApplication _instance;

		public static List<object> Args { get; set; }

		private int _configurationsInProgress;


		protected void Awake()
		{
			_instance = this;

			RegisterServices();
		}

		protected void Start()
		{
			StartCoroutine(ConfigureAsync());
		}

		protected void OnDestroy()
		{
			ResetLocator(true);
			_instance = null;
		}

		private IEnumerator ConfigureAsync()
		{
			var configurations = GetComponentsInChildren<IKernelConfiguration>();
			_configurationsInProgress = configurations.Length;
			foreach (var configuration in configurations)
			{
				StartCoroutine(ConfigureAsync(configuration));
			}
			while (_configurationsInProgress > 0)
			{
				yield return null;
			}
			IsLoaded = true;
		}

		private IEnumerator ConfigureAsync(IKernelConfiguration configuration)
		{
			yield return StartCoroutine(configuration.Configure());
			--_configurationsInProgress;
		}

		private void ResetLocator(bool destroy = false)
		{
			//Locator.Reset();
			//if (destroy) Locator.Destroy();
			GC.Collect();
		}

		private void RegisterServices()
		{
			//Locator.RegisterSingletons();
		}

		/// <summary>
		/// Нужно подождать, пока выгрузится предыдущая сцена, прежде чем загружать новую,
		/// иначе Locator.Reset() сбросит сервисы в новой сцене.
		/// Для загрузки новых сцен нужно использовать <see cref="KernelApplication.LoadScene"/>, в не <see cref="SceneManager.LoadScene"/>.
		/// </summary>
		/// <param name="scenes"></param>
		/// <param name="args">Arguments</param>
		/// <returns></returns>
		public static void LoadScene(string scene, params object[] args)
		{
			Debug.Assert(_instance != null);

			_instance.StartCoroutine(_instance.LoadSceneAsyncRoutine(scene, args));
		}

		private IEnumerator LoadSceneAsyncRoutine(string scene, params object[] args)
		{
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				var s = SceneManager.GetSceneAt(i);
				if (s.name != gameObject.scene.name)
				{
					Debug.Log("Unload scene: " + s.name);
					yield return SceneManager.UnloadSceneAsync(s.name);
				}
			}

			ResetLocator();

			Args = new List<object>(args);

			Debug.Log("Load scene: " + scene);
			yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
		}
	}
}
