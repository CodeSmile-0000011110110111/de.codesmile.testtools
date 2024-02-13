/*
// Copyright (C) 2021-2023 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace CodeSmile.Tests.Tools.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class LoadSceneAttribute : NUnitAttribute, IOuterUnityTestAction
	{
		private String m_ScenePath;

		public LoadSceneAttribute(String sceneName) => SetScenePath(sceneName);

		IEnumerator IOuterUnityTestAction.BeforeTest(ITest test)
		{
			if (EditorApplication.isPlaying)
				yield return EditorSceneManager.LoadSceneAsyncInPlayMode(m_ScenePath,
					new LoadSceneParameters(LoadSceneMode.Single));

			EditorSceneManager.OpenScene(m_ScenePath);
			yield return null;
		}

		IEnumerator IOuterUnityTestAction.AfterTest(ITest test) { yield return null; }

		private void SetScenePath(String sceneName)
		{
			const String sceneExtension = ".unity";
			m_ScenePath = Path.HasExtension(sceneName) ? sceneName : $"{sceneName}{sceneExtension}";
		}
	}
}
*/
