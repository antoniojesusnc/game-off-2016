namespace SpyGame.Events
{
	using System;
	using System.Collections.Generic;

	public class EventManager
	{
		private readonly List<GameEvent> currentEvents;

		private readonly Dictionary<object, EventDelegate> listeners;

		private readonly List<GameEvent> newEvents;

		private EventDelegate allEventListeners;

		private bool isProcessing;


		public EventManager()
		{
			this.newEvents = new List<GameEvent>();
			this.currentEvents = new List<GameEvent>();
			this.listeners = new Dictionary<object, EventDelegate>();
		}


		public delegate void EventDelegate(GameEvent e);

		public delegate void UnhandledEventDelegate(object eventType);

		public event UnhandledEventDelegate UnhandledEvent;


		public int EventCount
		{
			get
			{
				return this.newEvents.Count;
			}
		}


		public void Emit(object eventType, object eventData = null)
		{
			this.Emit(new GameEvent(eventType, eventData));
		}

		public void Emit(GameEvent e)
		{
			this.ProcessEvent(e);
		}

		public int ProcessEvents()
		{
			return this.ProcessEvents(0.0f);
		}

		public int ProcessEvents(float dt)
		{
			// If events are currently processed, we have to return as the events are
			// otherwise processed in the wrong order.
			if (this.isProcessing)
			{
				return 0;
			}

			this.isProcessing = true;

			int processedEvents = 0;

			// Process queues events.
			while (this.newEvents.Count > 0)
			{
				this.currentEvents.AddRange(this.newEvents);
				this.newEvents.Clear();

				foreach (GameEvent e in this.currentEvents)
				{
					this.ProcessEvent(e);
					++processedEvents;
				}
				this.currentEvents.Clear();
			}
				
			this.isProcessing = false;

			return processedEvents;
		}

		public void QueueEvent(object eventType)
		{
			this.QueueEvent(eventType, null);
		}

		public void QueueEvent(object eventType, object eventData)
		{
			this.QueueEvent(new GameEvent(eventType, eventData));
		}

		public void QueueEvent(GameEvent e)
		{
			this.newEvents.Add(e);
		}


		public void RegisterListener(object eventType, EventDelegate callback)
		{
			if (eventType == null)
			{
				throw new ArgumentNullException("eventType");
			}

			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}

			if (this.listeners.ContainsKey(eventType))
			{
				this.listeners[eventType] += callback;
			}
			else
			{
				this.listeners[eventType] = callback;
			}
		}

		public void RegisterListener(EventDelegate callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}

			this.allEventListeners += callback;
		}


		public void RemoveListener(object eventType, EventDelegate callback)
		{
			if (eventType == null)
			{
				throw new ArgumentNullException("eventType");
			}

			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}

			if (this.listeners.ContainsKey(eventType))
			{
				this.listeners[eventType] -= callback;
			}
		}

		public void RemoveListener(EventDelegate callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}

			this.allEventListeners -= callback;
		}


		private void OnUnhandledEvent(object eventType)
		{
			var handler = this.UnhandledEvent;

			if (handler != null)
			{
				handler(eventType);
			}
		}

		private void ProcessEvent(GameEvent e)
		{
			// Check for listeners to all events.
			EventDelegate eventListeners = this.allEventListeners;
			if (eventListeners != null)
			{
				eventListeners(e);
			}

			if (this.listeners.TryGetValue(e.EventType, out eventListeners))
			{
				if (eventListeners != null)
				{
					eventListeners(e);
				}
				else
				{
					this.OnUnhandledEvent(e.EventType);
				}
			}
			else
			{
				this.OnUnhandledEvent(e.EventType);
			}
		}
	}
}