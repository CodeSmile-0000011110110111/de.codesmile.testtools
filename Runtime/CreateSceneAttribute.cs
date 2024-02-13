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
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace CodeSmile.Tests.Tools.Attributes
{
	/// <summary>
	///     Creates a new empty scene for a unit test method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class CreateEmptySceneAttribute : CreateSceneAttribute
	{
		/// <summary>
		///     Creates a new empty scene for a unit test method.
		/// </summary>
		/// <param name="scenePath">
		///     if non-empty, the scene will be saved as an asset for tests that verify correctness of
		///     save/load of a scene's contents. The saved scene asset is deleted after the test ran.
		/// </param>
		public CreateEmptySceneAttribute(string scenePath = null)
			: base(scenePath) {}
	}

	/// <summary>
	///     Creates a new default scene (with Camera + Direct Light) for a unit test method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class CreateDefaultSceneAttribute : CreateSceneAttribute
	{
		/// <summary>
		///     Creates a new default scene (with Camera + Direct Light) for a unit test method.
		///     Caution: the scene contents will be deleted with the exception of the default game objects named
		///     "Main Camera" and "Directional Light". Any changes to these two objects will persist between tests.
		///     If you rename these objects they will be deleted and not restored for other tests.
		/// </summary>
		/// <param name="scenePath">
		///     if non-empty, the scene will be saved as an asset for tests that verify correctness of save/load of a scene's
		///     contents. The saved scene asset is deleted after the test ran.
		/// </param>
		public CreateDefaultSceneAttribute(string scenePath = null)
			: base(scenePath, NewSceneSetup.DefaultGameObjects) {}
	}

	public abstract class CreateSceneAttribute : NUnitAttribute, IOuterUnityTestAction
	{
		private readonly NewSceneSetup m_Setup;
		private String m_ScenePath;

		private static Boolean IsObjectNamedLikeADefaultObject(GameObject rootGameObject) =>
			rootGameObject.name == "Main Camera" || rootGameObject.name == "Directional Light";

		protected CreateSceneAttribute(String scenePath = null, NewSceneSetup setup = NewSceneSetup.EmptyScene)
		{
			m_Setup = setup;
			SetupAndVerifyScenePath(scenePath);
		}

		[ExcludeFromCodeCoverage] IEnumerator IOuterUnityTestAction.BeforeTest(ITest test)
		{
			yield return OnBeforeTest();
		}

		[ExcludeFromCodeCoverage] IEnumerator IOuterUnityTestAction.AfterTest(ITest test)
		{
			yield return OnAfterTest();
		}

		private IEnumerator OnBeforeTest()
		{
			if (Application.isPlaying)
				RuntimeLoadScene();
			else
				EditorCreateNewScene();

			return null;
		}

		private IEnumerator OnAfterTest()
		{
			if (Application.isPlaying)
				RuntimeLoadScene();
			else
				EditorCleanupScene();

			return null;
		}

		private void EditorCleanupScene()
		{
			var activeScene = SceneManager.GetActiveScene();
			foreach (var rootGameObject in activeScene.GetRootGameObjects())
			{
				if (ShouldSkipDefaultObjects(rootGameObject))
					continue;

				if (Application.isPlaying == false)
					GameObject.DestroyImmediate(rootGameObject);
				else
					GameObject.Destroy(rootGameObject);
			}

			if (IsScenePathValid())
				DeleteTestScene();
		}

		private Boolean ShouldSkipDefaultObjects(GameObject rootGameObject) =>
			m_Setup == NewSceneSetup.DefaultGameObjects &&
			IsObjectNamedLikeADefaultObject(rootGameObject);

		[ExcludeFromCodeCoverage]
		private void DeleteTestScene()
		{
			Debug.Log($"Deleting test scene from: {m_ScenePath}");
			if (AssetDatabase.DeleteAsset(m_ScenePath) != true)
				throw new UnityException($"AssetDatabase failed to delete test scene in: '{m_ScenePath}'");
		}

		private void EditorCreateNewScene()
		{
			var activeScene = SceneManager.GetActiveScene();
			var sceneName = CreateTestSceneName();
			if (activeScene.name != sceneName)
			{
				var scene = EditorSceneManager.NewScene(m_Setup);
				scene.name = sceneName;

				if (IsScenePathValid())
					SaveTestScene(scene);
			}
		}

		[ExcludeFromCodeCoverage]
		private void SaveTestScene(Scene scene)
		{
			Debug.Log($"Saving '{scene.name}' to {m_ScenePath} ...");
			if (EditorSceneManager.SaveScene(scene, m_ScenePath) == false)
				throw new UnityException($"EditorSceneManager failed to save test scene to: '{m_ScenePath}'");
		}

		private void RuntimeLoadScene()
		{
			// var sceneName = m_Setup == NewSceneSetup.EmptyScene
			// 	? TestNames.EmptyTestScene
			// 	: TestNames.DefaultObjectsTestScene;
			// SceneManager.LoadScene(sceneName);

			// TODO: implement
			throw new NotImplementedException();
		}

		private void SetupAndVerifyScenePath(String scenePath)
		{
			m_ScenePath = String.IsNullOrWhiteSpace(scenePath) == false ? scenePath : null;
			if (m_ScenePath != null)
			{
				PrefixAssetsPathIfNeeded();
				AppendSceneExtensionIfNeeded();
			}
		}

		private void PrefixAssetsPathIfNeeded()
		{
			if (m_ScenePath.StartsWith("Assets") == false)
				m_ScenePath = "Assets/" + m_ScenePath;
		}

		private void AppendSceneExtensionIfNeeded()
		{
			if (m_ScenePath.EndsWith(".unity") == false)
				m_ScenePath += ".unity";
		}

		private Boolean IsScenePathValid() => String.IsNullOrWhiteSpace(m_ScenePath) == false;

		private String CreateTestSceneName()
		{
			var name = m_ScenePath != null ? Path.GetFileName(m_ScenePath) : m_Setup.ToString();
			return $"Test [{GetType().Name.Replace("Attribute", "")}] {name}";
		}
	}
}
*/
