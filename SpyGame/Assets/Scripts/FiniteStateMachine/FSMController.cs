﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FiniteStateMachine {
    public abstract class FSMController : MonoBehaviour  {

        // all states
        protected List<FSMState> _states;

        // current state
        FSMState _currentState;
        List<FSMTransition> _currentStateTran;

        void Start()
        {
            ConstructFSM();
        }

        public abstract void ConstructFSM();

        void Update() {
            Update(Time.deltaTime);
        } // Update

        public void Update(float dt)
        {
            _currentState.Update(dt);

            for (int i = _currentStateTran.Count; i >= 0; --i) {
                if (_currentStateTran[i].CanNextState()) {
                    ChageState( _currentStateTran[i].GetNextState() );
                    return;
                }
            }
        } // Update

        public void ChageState(System.Type newState)
        {
            if(_currentState != null)
                _currentState.OnFinish();

            _currentState = GetState(newState);
            if (_currentState == null) {
                Debug.LogError("The state " + newState.ToString() + "doesnt exist in the FSM");
                return;
            }

            _currentStateTran = _currentState.GetTransitions();
            _currentState.OnStart();
        } // ChageState

        public List<FSMState> GetAllStates()
        {
            return _states;
        } // GetStates

        public FSMState GetState(System.Type state)
        {
            
            for (int i = _states.Count; i >= 0; --i) {
                if( _states[i].GetState() == state){
                    return _states[i];
                }
            }
            
            return null;
        } // GetState
    }
}