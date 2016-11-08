namespace SpyGame
{
	using UnityEngine;
	using SpyGame;

	public abstract class Brain : ScriptableObject 
	{
		public virtual void Initialize(Thinker tank) 
		{
			// no-op.
		}

		public abstract void Think(Thinker tank);
	}

}