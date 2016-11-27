using UnityEngine;
using System.Collections;

public class MiniMapEnemyDispatcher : MonoBehaviour
{


    [Header("Cone")]
    public Vector3 _coneOffset;
    public MiniMapDrawCone _conePrefab;

    [Header("Point")]
    public Vector3 _pointOffset;
    public MiniMapEnemy _pointPrefab;

    public void Start()
    {
        int i = 0;
        MiniMapDrawCone coneMiniMap;
        MiniMapEnemy enemyMiniMap;
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (_conePrefab != null)
            {
                coneMiniMap = Instantiate<MiniMapDrawCone>(_conePrefab);
                coneMiniMap.SetCone(enemy.GetComponentInChildren<VisualCone>().GetCone());
                coneMiniMap.transform.SetParent(transform);
                coneMiniMap.GetComponent<MiniMapEnemy>().FollowEnemy(enemy.transform, _coneOffset, true);
            }

            if (_pointPrefab != null)
            {
                enemyMiniMap = Instantiate<MiniMapEnemy>(_pointPrefab);
                enemyMiniMap.transform.SetParent(transform);
                enemyMiniMap.GetComponent<MiniMapEnemy>().FollowEnemy(enemy.transform, _pointOffset, false);
            }
        }
    } // Start
}
