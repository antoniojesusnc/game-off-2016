
namespace SpyGame
{
	using UnityEngine;
	using System.Collections.Generic;
	using SpyGame.Characters;

	public class Patrol : MonoBehaviour 
	{
		public List<Transform> waypoints;
		public float speed = 0.5f;
		private int currentWaypoint = 0;

		private ThirdPersonCharacter m_character;
		private Transform m_Cam;

		void Awake()
		{
			m_Cam = Camera.main.transform;
			currentWaypoint = 0;

			transform.position = waypoints [currentWaypoint].position;
			m_character = GetComponent<ThirdPersonCharacter> ();
		}

		public void FixedUpdate()
		{
			Vector3 sourcePosition = transform.position;
			Vector3 targetPosition = waypoints [GetNextWaypoint()].position;

			Vector3 m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			Vector3 dir = (targetPosition - sourcePosition).normalized;
			dir = (dir.z * m_CamForward + dir.x * m_Cam.right) * speed;

			m_character.Move(dir, false, false);

			float dist = Vector3.Distance (transform.position, targetPosition);
			if (dist < 2) {
				currentWaypoint = GetNextWaypoint();
			}
		}

		private int GetNextWaypoint() 
		{
			return (currentWaypoint + 1) % (waypoints.Count);
		}

	}
}