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
        protected void Move(Vector3 movement)
        {
            stateMachine.Agent.SetDestination(movement);
        }

        protected void UpdateAnimationFloat(int hash, float targetSpeed, float deltaTime, float dampTime = 0.1f)
        {
            float currentSpeed = stateMachine.Animator.GetFloat(hash);

            if (Mathf.Abs(currentSpeed - targetSpeed) < Mathf.Epsilon)
            {
                return;
            }

            stateMachine.Animator.SetFloat(hash, targetSpeed, dampTime, deltaTime);
        }
    }
}

