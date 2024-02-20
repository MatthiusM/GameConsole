using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FintieStateMachine
{
    public class PlayerGroundedState : PlayerState
    {
        private const float dampTime = 0.1f;
        public PlayerGroundedState(PlayerStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {

        }

        public override void Exit()
        {

        }

        public override void Update(float deltaTime)
        {
            Movement(deltaTime);
        }

        private void Movement(float deltaTime)
        {
            Transform cameraTransform = stateMachine.MainCameraTransform;
            Vector2 moveValue = stateMachine.InputManager.MovementValue;

            if (moveValue == Vector2.zero)
            {
                stateMachine.Animator.SetFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerHashes.Speed), 0, dampTime, deltaTime);
                return;
            }

            Vector3 forward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
            Vector3 right = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;

            Vector3 movement = (forward * moveValue.y + right * moveValue.x).normalized;

            stateMachine.Animator.SetFloat(stateMachine.PlayerAnimatorHashes.GetHash(PlayerHashes.Speed), 0.5f, dampTime, deltaTime);

            if (movement != Vector3.zero)
            {
                MovementDirection(movement, deltaTime);
            }

            stateMachine.CharacterController.Move(stateMachine.Speed * deltaTime * movement);
        }

        private void MovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationSpeed);
        }
    }

}
