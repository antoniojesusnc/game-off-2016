namespace SpyGame
{
	using UnityEngine;
	using System.Collections;

	[CreateAssetMenu(menuName="Brains/Player Controlled")]
	public class PlayerControlledBrain : Brain
	{

		public int PlayerNumber;

		private string _movementAxisName;

		private string _turnAxisName;


		public void OnEnable()
		{
			_movementAxisName = "Vertical";
			_turnAxisName     = "Horizontal";
		}

		public override void Think(Thinker thinker)
		{			
			/// !!!TODO: implement me
		}
	}
}