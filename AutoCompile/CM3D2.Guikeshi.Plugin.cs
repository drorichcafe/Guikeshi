using System;
using System.Collections.Generic;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace CM3D2.Guikeshi
{
	[PluginFilter("CM3D2x64"), PluginFilter("CM3D2x86"), PluginName("Guikeshi"), PluginVersion("0.0.0.0")]
	public class Guikeshi : PluginBase
	{
		public class Config
		{
			public KeyCode KeyToggleGUI = KeyCode.Escape;
		}

		private static bool m_enable = false;
		private static Config m_config = new Config();
		private static Dictionary<string, GameObject> m_objects = new Dictionary<string, GameObject>
		{
			{"UI Root", null },
			{"SystemUI Root", null },
		};

		public void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
			m_config = loadXml<Config>(System.IO.Path.Combine(this.DataPath, "Guikeshi.xml"));
		}

		public void LateUpdate()
		{
			if (Input.GetKeyDown(m_config.KeyToggleGUI))
			{
				m_enable = !m_enable;

				foreach (var pair in m_objects)
				{
					if (pair.Value == null) m_objects[ pair.Key ] = GameObject.Find(pair.Key);
					if (m_objects[pair.Key] != null) m_objects[pair.Key].SetActive(m_enable);
				}
			}
		}

		private T loadXml<T>(string path)
		{
			var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
			using (var sr = new System.IO.StreamReader(path, new System.Text.UTF8Encoding(true)))
			{
				return (T)serializer.Deserialize(sr);
			}
		}
	}
}