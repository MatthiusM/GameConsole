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
            Debug.Log("Jumping");
            Jump();
            stateMachine.Animator.CrossFadeInFixedTime(stateMachine.PlayerAnimatorHashes.GetHash(PlayerStates.Jump), 0.1f);
        }

        private void Jump()
        {
            stateMachine.CharacterController.Move(Vector3.up * jumpForce * Time.deltaTime);

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
            stateMachine.Animator.CrossFadeInFixedTime(stateMachine.PlayerAnimatorHashes.GetHash(PlayerStates.Grounded), 0.1f);
        }
    }
}

