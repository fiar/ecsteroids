using Kernel.Api;
using System;
using UnityEngine;

namespace Kernel.UI
{
	public abstract class FormMediator<T> : Mediator<T> where T : Form
	{

		protected virtual void OnEnable()
		{
			View.OpenEvent += OnViewOpen;
			View.OpenedEvent += OnViewOpened;
			View.CloseEvent += OnViewClose;
			View.ClosedEvent += OnViewClosed;
		}

		protected virtual void OnDisable()
		{
			if (View != null)
			{
				View.OpenEvent -= OnViewOpen;
				View.OpenedEvent -= OnViewOpened;
				View.CloseEvent -= OnViewClose;
				View.ClosedEvent -= OnViewClosed;
			}
		}

		protected virtual void OnViewOpen()
		{
			if (!IsRegistered)
			{
				OnRegistered();
				IsRegistered = true;
			}
		}

		protected virtual void OnViewOpened()
		{
		}

		protected virtual void OnViewClose()
		{
		}

		protected virtual void OnViewClosed()
		{
		}
	}
}
