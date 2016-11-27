namespace SpyGame
{
	using UnityEngine;
	using System.Collections;

	[CreateAssetMenu(menuName="Brains/Player Controlled")]
	public class PlayerControlledBrain : Brain
	{

		PlayerMovement movementComponent;

		public override void Initialize(Thinker thinker)
		{
			movementComponent = thinker.GetComponent<PlayerMovement>();
		}

		public override void Think(Thinker thinker)
		{			
			float inputX = Input.GetAxisRaw ("Horizontal");
			float inputZ = Input.GetAxisRaw ("Vertical");

			Vector3 direction = new Vector3 (inputX, 0, inputZ).normalized;

			if (movementComponent != null) 
			{
//				var movement = thinker.GetComponent<PlayerMovement> ();
				movementComponent.Steer (direction);
			}
		}
	}
}