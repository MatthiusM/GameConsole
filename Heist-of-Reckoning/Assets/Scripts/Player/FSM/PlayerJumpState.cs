using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerAnimations;

namespace FintieStateMachine 
{
    public class PlayerJumpState : PlayerState
    {
        private float jumpForce = 8f;
        private float gravity = -9.81f;
        private float verticalVelocity;

        public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            verticalVelocity = jumpForce;
            stateMachine.Animator.CrossFadeInFixedTime(stateMachine.PlayerAnimatorHashes.GetHash(PlayerStates.Jump), 0.1f);
        }

        public override void Update(float deltaTime)
        {
            verticalVelocity += gravity * deltaTime;
            Vector3 moveVector = new Vector3(0, verticalVelocity, 0) * deltaTime;
            stateMachine.CharacterController.Move(moveVector);
            if (stateMachine.CharacterController.isGrounded && verticalVelocity < 0)
            {
                stateMachine.SetCurrentState(new PlayerGroundedState(stateMachine));
            }
        }

        public override void Exit()
        {
            verticalVelocity = 0;
        }
    }
}
