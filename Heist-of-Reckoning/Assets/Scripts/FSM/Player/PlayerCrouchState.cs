using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerAnimations;
using System;

namespace FintieStateMachine
{
    public class PlayerCrouchState : PlayerState
    {
        private const float dampTime = 0.1f;
        
        public PlayerCrouchState(PlayerStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {
            Debug.Log("CrouchState");
            stateMachine.InputManager.CrouchEvent += CancelCrouch;
            stateMachine.Animator.Play(stateMachine.PlayerAnimatorHashes.GetHash(PlayerStates.Crouch));
        }

        public override void Exit()
        {
            stateMachine.InputManager.CrouchEvent -= CancelCrouch;
        }

        public override void Update(float deltaTime)
        {
            Transform cameraTransform = stateMachine.MainCameraTransform;
            Vector2 moveValue = stateMachine.InputManager.MovementValue;

            if (moveValue == Vector2.zero)
            {
                stateMachine.Animator.SetFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerParameters.Speed), CrouchStates.Idle, dampTime, deltaTime);
            }
            else
            {
                stateMachine.Animator.SetFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerParameters.Speed), CrouchStates.Walk, dampTime, deltaTime);
            }

            Vector3 forward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
            Vector3 right = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;

            Vector3 movement = (forward * moveValue.y + right * moveValue.x).normalized;


            if (movement != Vector3.zero)
            {
                MovementDirection(movement, deltaTime);
            }

            Move(movement * stateMachine.CrouchSpeed, deltaTime);
        }

        private void MovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationSpeed);
        }

        private void CancelCrouch()
        {
            stateMachine.Animator.Play(stateMachine.PlayerAnimatorHashes.GetHash(PlayerStates.Grounded));
            stateMachine.SetCurrentState(new PlayerGroundedState(stateMachine));
        }

    }
}

