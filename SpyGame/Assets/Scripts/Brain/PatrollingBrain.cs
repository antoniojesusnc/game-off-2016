namespace SpyGame
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using SpyGame;

	[CreateAssetMenu(menuName="Brains/Patrolling Enemy")]
	public class PatrollingBrain : Brain 
	{
		public List<Transform> m_Waypoints;
		public Transform m_Unit;
		public float m_nodePrecisionMod;
		public float m_Speed;

		private List<Vector3> m_PatrolPath;
		private Grid m_grid;
		private int m_CurrentWaypoint;

		public override void Initialize(Thinker thinker) 
		{
			m_Unit = thinker.transform;

			CalculatePatrollingPath();
			SetInitialVars();
		}

		public override void Think(Thinker thinker)
		{
			Vector3 targetPosition  = m_PatrolPath[m_CurrentWaypoint];
			Vector3 currentPosition = m_Unit.position;
			float distanceToTarget  = Vector3.Distance (targetPosition, currentPosition); 

			if (distanceToTarget < m_nodePrecisionMod)
			{
				ReachNextPoint();
			}

			thinker.transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);
		}


		/// <summary>
		/// Calculate the Pratolling points depending of the public points for the path
		/// </summary>
		private void CalculatePatrollingPath()
		{
			// get the point based on the transformObjects
			List<Vector3> _vector3PointReferences = new List<Vector3>();
			for (int i = 0; i < m_Waypoints.Count; ++i)
			{
				_vector3PointReferences.Add(m_Waypoints[i].position);
			}

			// calculate the nodes path and set in a list
			m_grid = GameObject.FindGameObjectWithTag("GridParent").GetComponent<GridFactory>().getGrid();
			List< Vector2 > _path = new List<Vector2>();
			for (int i = 0; i < m_Waypoints.Count - 1; ++i)
			{
				_path.AddRange(AStartSearch.Search(m_grid, _vector3PointReferences[i + 1], _vector3PointReferences[i]));
			}
			_path.AddRange(AStartSearch.Search(m_grid, _vector3PointReferences[0], _vector3PointReferences[_vector3PointReferences.Count - 1]));

			// get the path with the real points
			m_PatrolPath = new List<Vector3>();
			int pathSize = _path.Count;
			for (int i = 0; i < pathSize; i++)
			{
				m_PatrolPath.Add(GridUtils.worldFromPoint(m_grid, _path[i]));
			}
		} 

		private void SetInitialVars()
		{
			// setting the precion for detecting if reach some point
			if (m_nodePrecisionMod > 0)
			{
				m_nodePrecisionMod = m_grid.getNodeSize() * m_nodePrecisionMod;
			} else
			{
				m_nodePrecisionMod = m_grid.getNodeSize();
			}

			// setting initial values for the unit
			m_Unit.position = m_PatrolPath[0];
			m_CurrentWaypoint = 1;
			m_Unit.LookAt(m_PatrolPath[m_CurrentWaypoint]);

		} 

 
		/// <summary>
		/// method call when reach the next Node
		/// </summary>
		public void ReachNextPoint()
		{
			m_CurrentWaypoint = m_CurrentWaypoint + 1 >= m_PatrolPath.Count ? 0 : m_CurrentWaypoint +1;
			m_Unit.LookAt(m_PatrolPath[m_CurrentWaypoint]);
		} 



//		public List<Transform> waypoints;
//		public float speed = 1.0f;
//		private int currentWaypoint = 0;
//		private float lastWaypointSwitchTime;
//
//		/// <summary>
//		/// Initialize the specified thinker.
//		/// </summary>
//		/// <param name="thinker">Thinker.</param>
//		public virtual void Initialize(Thinker thinker) 
//		{
//			lastWaypointSwitchTime = Time.time;
//		}
//
//
//		// Update is called once per frame
//		/// <summary>
//		/// Think for the given thinker. Kind of equivalent to Update()
//		/// </summary>
//		/// <param name="thinker">Thinker</param>
//		public override void Think (Thinker thinker) 
//		{
//			// From the waypoints array, you retrieve the 
//			// start and end position for the current path segment.
//			Vector3 startPosition = waypoints [currentWaypoint].position;
//			Vector3 endPosition = waypoints [currentWaypoint + 1].position;
//
//			// Calculate the time needed for the whole distance with the 
//			// formula time = distance / speed, then determine the current 
//			// time on the path. Using Vector3.Lerp, you interpolate the 
//			// current position of the enemy between the segment’s start 
//			// and end positions. 
//			float pathLength = Vector3.Distance (startPosition, endPosition);
//			float totalTimeForPath = pathLength / speed;
//			float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
////
////			GameObject gameObject = thinker.gameObject;
////
////			gameObject.transform.position = 
////				Vector3.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
////
////			// Check whether the enemy has reached the endPosition. If yes, 
////			// handle these two possible scenarios:
////			if (gameObject.transform.position.Equals(endPosition)) {
////				if (currentWaypoint < waypoints.Count - 2) {
////					// The enemy is not yet at the last waypoint, so increase 
////					// currentWaypoint and update lastWaypointSwitchTime. Later, 
////					// you’ll add code to rotate the enemy so it points in the 
////					// direction it’s moving, too. 
////					currentWaypoint++;
////					lastWaypointSwitchTime = Time.time;
////					// TODO: Rotate into move direction
////				} else {
////					// The enemy reached the last waypoint, so this destroys it 
////					// and triggers a sound effect. Later you’ll add code to 
////					// decrease the player’s health, too.
//////					Destroy(gameObject);
//////
//////					AudioSource audioSource = gameObject.GetComponent<AudioSource>();
//////					AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
////					// TODO: deduct health
////				}
////			}
//		}
	}
}