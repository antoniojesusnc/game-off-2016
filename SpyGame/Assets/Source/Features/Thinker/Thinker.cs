	using SpyGame;
	using UnityEngine;
	using System.Collections;

	public class Thinker : MonoBehaviour 
	{
		public Brain brain;

		void Start () 
		{
			brain.Initialize (this);
		}

		void FixedUpdate ()
		{
			brain.Think (this);
		}

	}