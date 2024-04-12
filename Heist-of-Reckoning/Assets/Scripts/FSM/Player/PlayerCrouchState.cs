using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerAnimations;
using System;

namespace FiniteStateMachine
{
    public class PlayerCrouchState : PlayerState
    {        
        public PlayerCrouchState(PlayerStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {
            stateMachine.InputManager.CrouchEvent += CancelCrouch;
        }

        public override void Exit()
        {
            stateMachine.InputManager.CrouchEvent -= CancelCrouch;
        }

        public override void Update(float deltaTime)
        {
            UpdateAnimationFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerParameters.Posture), 0, deltaTime);
            Movement(deltaTime);

        }

        private void MovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationSpeed);
        }

        private void Movement(float deltaTime)
        {
            Transform cameraTransform = stateMachine.CameraTransform;
            Vector2 moveValue = stateMachine.InputManager.MovementValue;

            if (moveValue == Vector2.zero)
            {
                UpdateAnimationFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerParameters.Speed), CrouchStates.Idle, deltaTime);
            }
            else
            {
                UpdateAnimationFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerParameters.Speed), CrouchStates.Walk, deltaTime);
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

        private void CancelCrouch()
        {
            if (!stateMachine.CharacterController.isGrounded) { return; }
            stateMachine.SetCurrentState(new PlayerGroundedState(stateMachine));
        }

    }
}

