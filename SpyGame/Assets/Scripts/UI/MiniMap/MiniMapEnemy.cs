using UnityEngine;
using System.Collections;

public class MiniMapEnemy : MonoBehaviour
{
    Transform _enemy;
    Vector3 _offset;
    bool _alsoRotation;

    public void FollowEnemy(Transform enemy, Vector3 offset, bool alsoRotation)
    {
        _enemy = enemy;
        _offset = offset;
        _alsoRotation = alsoRotation;
    } // FollowEnemy

    void Update()
    {
        if (_enemy != null)
        {
            transform.position = _enemy.position + _offset;
            if (_alsoRotation)
            {
                transform.rotation = _enemy.rotation;
            }
        }
    } // Update
}
