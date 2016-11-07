namespace SpyGame 
{
	using UnityEngine;
	using System.Collections;
	using SpyGame.Events;

	public class Game
	{
		private readonly EventManager eventManager;

		private float accumulatedTime;

		public Game()
		{
			this.eventManager = new EventManager();
			this.Running = false;
			this.TimeElapsed = 0.0f;
		}

		public EventManager EventManager
		{
			get
			{
				return this.eventManager;
			}
		}
			
		public bool Running { get; private set; }

		public float TimeElapsed { get; private set; }

		public float UpdatePeriod { get; set; }


		public void Update(float dt)
		{
			if (!this.Running)
			{
				return;
			}

			if (this.UpdatePeriod > 0)
			{
				// Fixed update.
				this.accumulatedTime += dt;

				while (this.accumulatedTime >= this.UpdatePeriod)
				{
					this.UpdateGame(this.UpdatePeriod);
					this.accumulatedTime -= this.UpdatePeriod;
				}
			}
			else
			{
				this.UpdateGame(dt);
			}

			this.TimeElapsed += dt;
		}


		private void UpdateGame(float dt)
		{
			this.eventManager.ProcessEvents(dt);
		}
	}
}