using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FintieStateMachine
{
    public abstract class PlayerState : State
    {
        protected new PlayerStateMachine stateMachine;

        public PlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}

