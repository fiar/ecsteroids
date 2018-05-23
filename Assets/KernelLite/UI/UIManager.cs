using Kernel.UI.Behaviours;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kernel.UI
{
	public class UIManager
	{
		#region Instance
		private static UIManager _instance;

		public static UIManager Instance
		{
			get
			{
				if (_instance == null) _instance = new UIManager();
				return _instance;
			}
		}

		public static bool IsInstantiated
		{
			get
			{
				return (_instance != null);
			}
		}
		#endregion

		public UIBehaviour Behaviour { get; private set; }

		private List<Form> _forms;

		public static ScreenFader ScreenFader
		{
			get { return IsInstantiated ? Instance.Behaviour.ScreenFader : null; }
		}

		public static DialogFader DialogFader
		{
			get { return IsInstantiated ? Instance.Behaviour.DialogFader : null; }
		}


		public static void Initialize()
		{
			Instance.Initialize_Internal();
		}

		public static void Destroy()
		{
			if (!IsInstantiated) return;
			Instance.Destroy_Internal();
		}

		public static Form CreateForm(string name, Transform parent = null)
		{
			if (!IsInstantiated) return null;
			return Instance.CreateForm_Internal(name, parent);
		}


		#region Internal
		private void Initialize_Internal()
		{
			Behaviour = GameObject.FindObjectOfType<UIBehaviour>();
			Debug.Assert(Behaviour != null, "UIBehaviour not found");

			_forms = new List<Form>();

			var forms = Behaviour.transform.GetComponentsInChildren<Form>();
			foreach (var form in forms)
			{
				_forms.Add(form);
			}

			Canvas.ForceUpdateCanvases();
		}

		private void Destroy_Internal()
		{
			if (_forms != null)
			{
				foreach (var form in _forms)
				{
					if (form != null)
					{
						form.Destroy();
					}
				}
				_forms.Clear();
			}
		}

		private Form CreateForm_Internal(string name, Transform parent)
		{
			var resource = Resources.Load<Form>("UI/Forms/" + name);
			Debug.Assert(resource != null, "Form (" + name + ") not found");

			var form = GameObject.Instantiate<Form>(resource);
			form.name = name;
			form.transform.SetParent((parent == null) ? Behaviour.Container : parent, false);
			form.gameObject.SetActive(false);

			return form;
		}
		#endregion
	}
}
