using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerAnimations;


namespace FintieStateMachine
{
    public class PlayerJumpState : PlayerState
    {
        private float jumpForce = 1000f;

        public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            Jump();
            stateMachine.Animator.CrossFadeInFixedTime(stateMachine.PlayerAnimatorHashes.GetHash(PlayerStates.Jump), 0.1f);
        }

        private void Jump()
        {
            stateMachine.CharacterController.Move(jumpForce * Time.deltaTime * Vector3.up);

        }

        public override void Update(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);

            if (stateMachine.CharacterController.isGrounded)
            {
                stateMachine.SetCurrentState(new PlayerGroundedState(stateMachine));
            }
        }
        public override void Exit()
        {
            
        }
    }
}

