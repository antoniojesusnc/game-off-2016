using UnityEngine;
using System.Collections;
using Geometry;
using SpyGame;

public class DebugCone : MonoBehaviour
{

    public Cone _cone;

    public Transform _objetive;

    public bool _drawWithGizmo;

    bool check = false;

	Game game;

	void Awake ()
	{
		game = SceneController.Game;
	}

    void FixedUpdate()
    {
        if (_objetive != null)
        {
			if (Utils.Math.isInsideCone (_cone, transform.position, transform.forward, _objetive.position)) 
			{
				RaycastHit hit;

				if (Physics.Raycast (transform.position, (_objetive.position - transform.position), out hit, _cone.getLength ())) 
				{
					if (hit.collider.gameObject.Equals(_objetive.transform.gameObject)) {
						game.EventManager.Emit(TestEvents.TEST01);
					} 
				} 

			}
        }
    }

    public void OnDrawGizmos()
    {

        if (!_drawWithGizmo)
            return;
        float halfAngle = _cone.getAngle() * 0.5f;
        for (float i = -halfAngle; i <= halfAngle; ++i)
        {
            //Gizmos.DrawLine(transform.position, transform.position+transform.forward * _cone.getLength());
            Vector3 dir = Quaternion.Euler(0, i, 0) * transform.forward;
            Gizmos.DrawLine(transform.position, transform.position + dir * _cone.getLength());
        }
    }
}
