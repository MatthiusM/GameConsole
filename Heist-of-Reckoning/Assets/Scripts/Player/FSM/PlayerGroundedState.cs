using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerAnimations;
using FintieStateMachine;

namespace FiniteStateMachine
{
    public class PlayerGroundedState : PlayerState
    {
        public PlayerGroundedState(PlayerStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {
            stateMachine.InputManager.CrouchEvent += OnCrouch;
            stateMachine.InputManager.JumpEvent += OnJump;
            stateMachine.InputManager.AimEvent += OnAim;
        }

        public override void Exit()
        {
            stateMachine.InputManager.CrouchEvent -= OnCrouch;
            stateMachine.InputManager.JumpEvent -= OnJump;
            stateMachine.InputManager.AimEvent -= OnAim;
        }

        private void OnAim()
        {
            if(!stateMachine.IsPolice) { return; }
            stateMachine.SetCurrentState(new PlayerAimState(stateMachine));
        }

        private void OnJump()
        {
            stateMachine.SetCurrentState(new PlayerJumpState(stateMachine));
        }

        private void OnCrouch()
        {
            if(!stateMachine.CharacterController.isGrounded) { return; }
            stateMachine.SetCurrentState(new PlayerCrouchState(stateMachine));
        }

        public override void Update(float deltaTime)
        {
            UpdateAnimationFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerParameters.Posture), 1, deltaTime);
            Movement(deltaTime);
        }

        private void Movement(float deltaTime)
        {
            Transform cameraTransform = stateMachine.CameraTransform;
            Vector2 moveValue = stateMachine.InputManager.MovementValue;

            if (moveValue == Vector2.zero)
            {
                UpdateAnimationFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerParameters.Speed), GroundedStates.Idle, deltaTime);
            }
            else
            {
                float walkRun = stateMachine.InputManager.IsRunning ? GroundedStates.Run : GroundedStates.Walk;
                UpdateAnimationFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerParameters.Speed), walkRun, deltaTime);
            }

            Vector3 forward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
            Vector3 right = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;

            Vector3 movement = (forward * moveValue.y + right * moveValue.x).normalized;


            if (movement != Vector3.zero)
            {
                MovementDirection(movement, deltaTime);
            }

            Move(movement * stateMachine.WalkSpeed, deltaTime); 
        }

        private void MovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationSpeed);
        }
    }

}
