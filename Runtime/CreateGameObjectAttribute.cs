/*
// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.TestTools;
using Object = System.Object;

namespace CodeSmile.Tests.Tools.Attributes
{
	/// <summary>
	///     Creates a new GameObject instance in the scene for a test method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class CreateGameObjectAttribute : NUnitAttribute, IOuterUnityTestAction
	{
		public const String DefaultName = "Test GameObject";

		private readonly String m_Name;
		private readonly Type[] m_Components;
		private GameObject m_GameObject;

		/// <summary>
		///     Creates a new GameObject instance in the scene for a test method.
		/// </summary>
		/// <param name="name">name of the GameObject, defaults to: CreateGameObjectAttribute.DefaultName</param>
		/// <param name="components">type(s) of components to add to the GameObject</param>
		public CreateGameObjectAttribute(String name = DefaultName, params Type[] components)
		{
			m_Name = name;
			m_Components = components;
		}

		[ExcludeFromCodeCoverage] IEnumerator IOuterUnityTestAction.BeforeTest(ITest test)
		{
			yield return OnBeforeTest();
		}

		[ExcludeFromCodeCoverage] IEnumerator IOuterUnityTestAction.AfterTest(ITest test)
		{
			yield return OnAfterTest();
		}

		private Object OnBeforeTest()
		{
			m_GameObject = new GameObject(m_Name, m_Components);
			return null;
		}

		private Object OnAfterTest()
		{
			if (Application.isPlaying == false)
				GameObject.DestroyImmediate(m_GameObject);
			else
				GameObject.Destroy(m_GameObject);
			return null;
		}
	}
}
*/
