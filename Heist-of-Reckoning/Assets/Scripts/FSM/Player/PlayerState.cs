using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace FintieStateMachine
{
    public abstract class PlayerState : State
    {
        private float gravity;

        protected new PlayerStateMachine stateMachine;

        public PlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void Move(Vector3 movement, float deltaTime)
        {            
            if (gravity < 0f && stateMachine.CharacterController.isGrounded)
            {
                gravity = Physics.gravity.y * deltaTime;
            }
            else
            {
                gravity += Physics.gravity.y * deltaTime;
            }
           
            stateMachine.CharacterController.Move((movement + Vector3.up * gravity) * deltaTime);
        }
    }
}

