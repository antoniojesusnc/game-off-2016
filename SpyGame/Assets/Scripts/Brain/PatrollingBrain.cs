using UnityStandardAssets.Characters.ThirdPerson;

namespace SpyGame
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using SpyGame;

	[CreateAssetMenu(menuName="Brains/Patrolling Enemy")]
	public class PatrollingBrain : Brain 
	{
		public List<Transform> waypoints;
		public float speed = 1.0f;
		private int currentWaypoint = 0;

		private ThirdPersonCharacter m_character;
		private Transform m_Cam;

		public override void Initialize(Thinker thinker) 
		{
			m_Cam = Camera.main.transform;
			currentWaypoint = 0;

			thinker.transform.position = waypoints [currentWaypoint].position;
			m_character = thinker.GetComponentInParent<ThirdPersonCharacter> ();
		}

		public override void Think(Thinker thinker)
		{
			Vector3 sourcePosition = thinker.transform.position;
			Vector3 targetPosition = waypoints [GetNextWaypoint()].position;

			Debug.Log ("Cam.forward: " + m_Cam.forward.ToString());

			// Calculate the time needed for the whole distance with the 
			// formula time = distance / speed, then determine the current 
			// time on the path. Using Vector3.Lerp, you interpolate the 
			// current position of the enemy between the segment’s start 
			// and end positions. 
			Vector3 m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			Vector3 dir = (targetPosition - sourcePosition).normalized;
			dir = (dir.z * m_CamForward + dir.x * m_Cam.right) * speed;

			m_character.Move(dir, false, false);

			// Check whether the enemy has reached the endPosition. If yes, 
			// handle these two possible scenarios:
			float dist = Vector3.Distance (thinker.transform.position, targetPosition);
			if (dist < 3) {
				currentWaypoint = GetNextWaypoint();
			}
		}

		int GetNextWaypoint() 
		{
			return (currentWaypoint + 1) % (waypoints.Count);
		}
	}
}