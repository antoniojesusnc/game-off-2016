namespace SpyGame
{
	using UnityEngine;
	using System.Collections;

	[RequireComponent (typeof (Rigidbody))]
	public class PlayerMovement : MonoBehaviour 
	{

		public float _Speed = 12f;                 // How fast the player moves.


		private Rigidbody _Rigidbody;              // Reference used to move the player.
		private Vector3 _currentDir = new Vector3();

		// Use this for initialization
		void Start () {
			_Rigidbody = GetComponent<Rigidbody> ();
		}

		public void Steer (Vector3 dir)
		{
			// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
			_currentDir = dir * _Speed * Time.fixedDeltaTime;
		}

		private void FixedUpdate ()
		{
			Move ();
		}

		private void Move()
		{
			// Apply this movement to the rigidbody's position.
			_Rigidbody.MovePosition(_Rigidbody.position + _currentDir);
		}
	}
}