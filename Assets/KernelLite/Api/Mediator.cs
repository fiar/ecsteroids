﻿using System;
using UnityEngine;

namespace Kernel.Api
{
	public abstract class Mediator<T> : MonoBehaviour where T : View
	{
		public bool IsRegistered { get; protected set; }

		public T View { get; protected set; }


		protected virtual void Awake()
		{
			View = GetComponent<T>();
			Debug.AssertFormat(View != null, "View of type {0} not exists at {1}", typeof(T), GetType());

			OnInitialized();
		}

		protected virtual void Start()
		{
			if (!IsRegistered)
			{
				OnRegistered();
				IsRegistered = true;
			}
		}

		protected virtual void OnDestroy()
		{
			OnUnRegistered();
		}

		protected virtual void OnInitialized()
		{
		}

		protected virtual void OnRegistered()
		{
		}

		protected virtual void OnUnRegistered()
		{
		}
	}
}
