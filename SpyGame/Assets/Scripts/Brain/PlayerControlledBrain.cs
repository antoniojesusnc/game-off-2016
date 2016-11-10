namespace SpyGame
{
	using UnityEngine;
	using System.Collections;

	[CreateAssetMenu(menuName="Brains/Player Controlled")]
	public class PlayerControlledBrain : Brain
	{

		public override void Think(Thinker thinker)
		{			
			float inputX = Input.GetAxisRaw ("Vertical");
			float inputY = Input.GetAxisRaw ("Horizontal");

			Vector3 direction = new Vector3 (inputX, 0, inputY).normalized;

			var movement = thinker.GetComponent<PlayerMovement>();
			movement.Steer(direction);
		}
	}
}