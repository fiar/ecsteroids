using Kernel.Core;
using Kernel.StateMachine;
using Kernel.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kernel.Core
{
	public abstract class SceneContext : MonoBehaviour
	{
		#region Instance
		public static SceneContext Current { get; private set; }
		#endregion

		public IAbstractState FSM { get; private set; }


		protected void Awake()
		{
			if (!KernelApplication.IsInitialized)
			{
				Debug.LogError("Load <b>Kernel</b> scene and set it as <b>Active</b>");
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#endif
			}

			Current = this;

			OnInitialized();
		}

		protected void Start()
		{
			SceneManager.SetActiveScene(gameObject.scene);
			StartCoroutine(StartAsync());
		}

		protected IEnumerator StartAsync()
		{
			while (!KernelApplication.IsLoaded) yield return null;

			var roots = gameObject.scene
				.GetRootGameObjects()
				.Where(x => x.GetComponent<SceneRoot>() != null);

			foreach (var root in roots)
			{
				root.gameObject.SetActive(true);
			}

			UIManager.Initialize();

			FSM = BuildStateMachine();

			StartContext();
		}

		protected void OnDestroy()
		{
			UIManager.Destroy();

			StopContext();

			if (FSM != null)
			{
				FSM.Dispose();
			}
		}

		protected virtual IAbstractState BuildStateMachine()
		{
			return null;
		}

		public static void TriggerEvent(string name)
		{
			if (Current != null && Current.FSM != null)
			{
				Current.FSM.TriggerEvent(name);
			}
		}

		protected virtual void Update()
		{
			if (FSM != null)
			{
				FSM.Update();

				if (Input.GetKeyDown(KeyCode.Escape))
				{
					FSM.TriggerEvent("BACK");
				}
			}
		}

		protected virtual void OnInitialized() { }

		protected virtual void StartContext() { }

		protected virtual void StopContext() { }
	}
}
