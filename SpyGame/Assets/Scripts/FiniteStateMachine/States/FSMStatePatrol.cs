using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

using FiniteStateMachine;
public class FSMPatrolState : FSMState
{
    private float _speed;
    new FSMControllerEnemy _controller;

    public FSMPatrolState(FSMController controller) : base(controller)
    {
        _controller = controller as FSMControllerEnemy;
    } // FSMPatrolState

    public override void OnStart()
    {
        base.OnStart();
    } // OnStart

    public override void Update(float dt)
    {
        base.Update(dt);

        if (( _controller.GetNextPosition() - _controller.GetUnit().position ).sqrMagnitude < 
            _controller.GetNodePresition()* _controller.GetNodePresition())
        {
            _controller.ReachNextPoint();
        }

        _speed = _controller._speed;
        _controller.GetUnit().Translate(Vector3.forward * _speed * dt);
    } // Update

}
