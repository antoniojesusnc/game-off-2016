using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine;
using System;

public class FSMControllerEnemy : FSMController
{

    public List<Transform> _patrolPointReferences;
    public Transform _unit;
    public float _nodePrecisionMod;

    // unit speed, set here for now
    public float _speed;

    private List<Vector3> _patrollingPath;
    private Grid _grid;
    private int _patrollingPointIndex;

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        CalculatePatrollingPath();
        SetInitialVars();

        base.Init();
    } // Start

    /// <summary>
    /// Calculate the Pratolling points depending of the public points for the path
    /// </summary>
    private void CalculatePatrollingPath()
    {
        // get the point based on the transformObjects
        List<Vector3> _vector3PointReferences = new List<Vector3>();
        for (int i = 0; i < _patrolPointReferences.Count; ++i)
        {
            _vector3PointReferences.Add(_patrolPointReferences[i].position);
        }

        // calculate the nodes path and set in a list
        _grid = GameObject.FindGameObjectWithTag("GridParent").GetComponent<GridFactory>().getGrid();
        List< Vector2 > _path = new List<Vector2>();
        for (int i = 0; i < _patrolPointReferences.Count - 1; ++i)
        {
            _path.AddRange(AStartSearch.Search(_grid, _vector3PointReferences[i + 1], _vector3PointReferences[i]));
        }
        _path.AddRange(AStartSearch.Search(_grid, _vector3PointReferences[0], _vector3PointReferences[_vector3PointReferences.Count - 1]));

        // get the path with the real points
        _patrollingPath = new List<Vector3>();
        int pathSize = _path.Count;
        for (int i = 0; i < pathSize; i++)
        {
            _patrollingPath.Add(GridUtils.worldFromPoint(_grid, _path[i]));
        }
    } // CalculatePatrollingPath

    private void SetInitialVars()
    {
        // setting the precion for detecting if reach some point
        if (_nodePrecisionMod > 0)
        {
            _nodePrecisionMod = _grid.getNodeSize() * _nodePrecisionMod;
        } else
        {
            _nodePrecisionMod = _grid.getNodeSize();
        }

        // setting initial values for the unit
        _unit.position = _patrollingPath[0];
        _patrollingPointIndex = 1;
        _unit.LookAt(_patrollingPath[_patrollingPointIndex]);

    } // SetInitialVars

    public override void ConstructFSM()
    {
        List<Vector3> _vector3Reference = new List<Vector3>(); ;
        for (int i = 0; i < _patrolPointReferences.Count; ++i)
        {
            _vector3Reference.Add(_patrolPointReferences[i].position);
        }
        _currentState = new FSMPatrolState(this);
        _states = new List<FSMState>();
        _states.Add(_currentState);
    } // ConstructFSM

    /// <summary>
    /// method call when reach the next Node
    /// </summary>
    public void ReachNextPoint()
    {
        _patrollingPointIndex = _patrollingPointIndex + 1 >= _patrollingPath.Count ? 0 : _patrollingPointIndex +1;
        _unit.LookAt(_patrollingPath[_patrollingPointIndex]);
    } // ReachPoint

    ///////////////////////////////////////
    ///             GETS & SETS         ///
    ///////////////////////////////////////
    public List<Vector3> GetPratollingPath()
    {
        return _patrollingPath;
    } // GetPratollingPath

    public int GetPatrollingPointIndex()
    {
        return _patrollingPointIndex;
    } // GetPatrollingPointIndex
    
    public void SetPatrollingPointIndex(int newIndex)
    {
        _patrollingPointIndex = newIndex;
    } // SetPatrollingPointIndex

    public Vector3 GetNextPosition()
    {
        return _patrollingPath[_patrollingPointIndex];
    } // GetNextPosition

    public Transform GetUnit()
    {
        return _unit;
    } // GetUnit

    public float GetNodePresition()
    {
        return _nodePrecisionMod;
    } // GetNodePresition
}
