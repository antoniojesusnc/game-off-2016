namespace SpyGame.Events
{
	using System;

	public class GameEvent
	{
		public GameEvent(object eventType)
			: this(eventType, null)
		{
		}

		public GameEvent(object eventType, object eventData)
		{
			if (eventType == null)
			{
				throw new ArgumentNullException("eventType");
			}

			this.EventType = eventType;
			this.EventData = eventData;
		}

		public object EventData { get; private set; }

		public object EventType { get; private set; }

		public override string ToString()
		{
			return string.Format("Event: {0} - Event data: {1}", this.EventType, this.EventData);
		}
	}
}