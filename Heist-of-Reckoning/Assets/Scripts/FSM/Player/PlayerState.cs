using PlayerAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace FintieStateMachine
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
            Debug.Log(stateMachine.CharacterController.isGrounded);
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

        protected bool IsTransitioningToAnimation(int animationHash)
        {
            if (stateMachine.Animator.IsInTransition(0))
            {
                AnimatorStateInfo nextAnimationStateInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

                if (nextAnimationStateInfo.shortNameHash == animationHash)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

