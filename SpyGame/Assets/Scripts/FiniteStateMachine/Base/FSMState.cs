using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace FiniteStateMachine
{
    public abstract class FSMState {

        protected FSMController _controller;
        private List<FSMTransition> _transitions;

        public FSMState(FSMController controller)
        {
            _transitions = new List<FSMTransition>();
            _controller = controller;            
        } // FSMState

        public virtual void OnStart()
        {

        } // OnStart

        public virtual void Update(float dt)
        {

        } // Update

        public virtual void OnFinish()
        {

        } // OnFinish

        // Transition methods
        public void AddTransition(FSMTransition newTransition)
        {
            _transitions.Add(newTransition);
        } // AddTransition

        public void RemoveTransition(FSMTransition removeTransition)
        {
            _transitions.Remove(removeTransition);
        } // RemoveTransition

        // gets & sets
        public List<FSMTransition> GetTransitions()
        {
            return _transitions;
        } //ChangeState     

        public Type GetState()
        {
            return GetType();
        }
    }
}