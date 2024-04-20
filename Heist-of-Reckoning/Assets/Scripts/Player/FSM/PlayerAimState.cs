using PlayerAnimations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

namespace FiniteStateMachine
{
    public class PlayerAimState : PlayerState
    {
        private Transform camera;

        public PlayerAimState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(stateMachine.PlayerAnimatorHashes.GetHash(PlayerStates.PistolIdle), 0.1f);
            camera = stateMachine.transform.GetComponentInChildren<Camera>().transform;

            stateMachine.InputManager.AimEvent += OnAim;
            stateMachine.InputManager.ShootEvent += OnShoot;
        }

        public override void Exit()
        {

            stateMachine.InputManager.AimEvent -= OnAim;
            stateMachine.InputManager.ShootEvent -= OnShoot;
        }

        public override void Update(float deltaTime)
        {
            if (camera != null)
            {
                float yRotation = camera.eulerAngles.y;

                stateMachine.transform.rotation = Quaternion.Euler(stateMachine.transform.eulerAngles.x, yRotation, stateMachine.transform.eulerAngles.z);
            }
        }

        private void OnAim()
        {
            stateMachine.SetCurrentState(new PlayerGroundedState(stateMachine));
        }

        private void OnShoot()
        {
            stateMachine.Animator.CrossFadeInFixedTime(stateMachine.PlayerAnimatorHashes.GetHash(PlayerStates.Shooting), 0.1f);
        }
    }
}
