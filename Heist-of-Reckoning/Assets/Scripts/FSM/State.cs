using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class State
    {
        protected FiniteStateMachine stateMachine;
        public State(FiniteStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public abstract void Enter();

        public abstract void Update(float deltaTime);

        public abstract void Exit();
    }
}

