using UnityEngine;
using System.Collections;

public class MiniMapSurveillanceCameraDispatcher : MonoBehaviour
{
    [Header("Cone")]
    public Vector3 _coneOffset;
    public MiniMapDrawCone _conePrefab;

    [Header("SurveillanceCamera")]
    public Vector3 _offset;
    public MiniMapSurveillanceCamera _prefab;

    public void Start()
    {
        MiniMapDrawCone coneMiniMap;
        MiniMapSurveillanceCamera cameraMiniMap;
        foreach (var camera in GameObject.FindObjectsOfType<SurveillanceCamera>())
        {
            if (_conePrefab != null)
            {
                coneMiniMap = Instantiate<MiniMapDrawCone>(_conePrefab);
                coneMiniMap.SetCone(camera.GetComponentInChildren<VisualCone>().GetCone());
                coneMiniMap.transform.SetParent(transform);
                coneMiniMap.GetComponent<MiniMapEnemy>().FollowEnemy(camera.transform, _coneOffset, true);
            }

            if (_prefab != null)
            {
                cameraMiniMap = Instantiate<MiniMapSurveillanceCamera>(_prefab);
                cameraMiniMap.transform.SetParent(transform);
                cameraMiniMap.transform.position = camera.transform.position;
            }
        }
    } // Start
}
