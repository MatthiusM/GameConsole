using PlayerAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace FiniteStateMachine
{
    public abstract class PlayerState : State
    {
        protected new PlayerStateMachine stateMachine;
        
        private float gravity;

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
           
            stateMachine.CharacterController.Move((movement + (Vector3.up * gravity)) * deltaTime);
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

