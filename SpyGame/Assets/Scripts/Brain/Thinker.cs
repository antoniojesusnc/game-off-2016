namespace SpyGame
{
	using UnityEngine;
	using System.Collections;

	public class Thinker : MonoBehaviour 
	{
		public Brain brain;

		void Awake()
		{
			enabled = false;
		}

		void OnEnable()
		{
			if (!brain)
			{
				enabled = false;
				return;
			}

			brain.Initialize(this);
		}

		void Update ()
		{
			brain.Think(this);
		}

		public void Setup(Transform spawnPoint)
		{
			transform.position = spawnPoint.position;
			transform.rotation = spawnPoint.rotation;

			enabled = true;
		}
	}
}