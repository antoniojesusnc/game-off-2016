using UnityEngine;
using System.Collections;

namespace FiniteStateMachine
{
    public abstract class FSMTransition {

        protected FSMController _controller;
        protected System.Type _nextState;

        public FSMTransition(FSMController controller, System.Type nextState)
        {
            _controller = controller;
            _nextState = nextState;
        } // FSMState

        public abstract bool CanNextState();

        // gets & sets
        public System.Type GetNextState()
        {
            return _nextState;
        } // GetNextState
    }
}