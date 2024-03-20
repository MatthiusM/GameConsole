using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class PoliceState : State
    {
        protected new PoliceStateMachine stateMachine;

        public PoliceState(PoliceStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}

