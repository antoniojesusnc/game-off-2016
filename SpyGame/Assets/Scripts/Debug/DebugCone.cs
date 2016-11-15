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

    void Update()
    {

        if (( Mathf.FloorToInt(Time.timeSinceLevelLoad % 2) == 0 ))
        {
            if (!check || Input.GetKeyUp(KeyCode.A))
            {
                check = true;
                if (_objetive != null)
                {
					if (Utils.Math.isInsideCone (_cone, transform.position, transform.forward, _objetive.position)) 
					{
//						Debug.Log ("Player Detected");
						game.EventManager.Emit(TestEvents.TEST01);
					}
                }
            }
        } else
        {
            check = false;
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
