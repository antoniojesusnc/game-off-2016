using UnityEngine;
using System.Collections;

public class DebugPlayerMovement : MonoBehaviour
{

    public float _speed;

    private Transform _player;
    private Vector3 _momentum;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _momentum.Set(0, 0, 0);


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _momentum.Set(-1 + _momentum.x, _momentum.y, _momentum.z);
        } if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _momentum.Set(1 + _momentum.x, _momentum.y, _momentum.z);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _momentum.Set(_momentum.x, _momentum.y, -1 + _momentum.z);
        } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _momentum.Set(_momentum.x, _momentum.y, 1 + _momentum.z);
        }

        if (_momentum.x != 0 || _momentum.z != 0)
            _player.Translate(_momentum * _speed * Time.deltaTime);
    } // Update 
}
