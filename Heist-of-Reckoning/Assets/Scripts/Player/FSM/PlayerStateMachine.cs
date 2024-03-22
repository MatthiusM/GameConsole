using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerAnimations;

namespace FiniteStateMachine
{    public class PlayerStateMachine : FiniteStateMachine
    {
        [field: SerializeField] public InputManager InputManager { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public PlayerAnimatorHashes PlayerAnimatorHashes { get; private set; }
        
        [SerializeField, Range(1f, 5f)]
        private float walkSpeed = 2.5f;

        [SerializeField, Range(0.1f, 1f)]
        private float crouchSpeed = 0.3f;

        private float currentSpeed;

        [SerializeField, Range(1f, 10f)]
        private float rotationSpeed = 8f;

        public float WalkSpeed
        {
            get => currentSpeed;
            private set => currentSpeed = value;
        }

        public float CrouchSpeed
        {
            get => crouchSpeed;
            private set => crouchSpeed = value;
        }

        public float RotationSpeed
        {
            get => rotationSpeed;
            private set => rotationSpeed = Mathf.Clamp(value, 1f, 10f);
        }

        private void Start()
        {
            MainCameraTransform = Camera.main.transform;
            PlayerAnimatorHashes = new PlayerAnimatorHashes();
            SetCurrentState(new PlayerGroundedState(this));
            currentSpeed = walkSpeed;
        }

        private void OnEnable()
        {
            InputManager.RunEvent += AdjustSpeedOverTime;
        }

        private void OnDisable()
        {
            InputManager.RunEvent -= AdjustSpeedOverTime;
        }

        public void AdjustSpeedOverTime()
        {
            currentSpeed = InputManager.IsRunning ? walkSpeed * 2 : walkSpeed;
            StartCoroutine(AdjustSpeedCoroutine(currentSpeed, 2f));
        }

        private IEnumerator AdjustSpeedCoroutine(float targetSpeed, float duration)
        {
            float time = 0;
            float startSpeed = WalkSpeed;

            while (time < duration)
            {
                WalkSpeed = Mathf.Lerp(startSpeed, targetSpeed, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            WalkSpeed = targetSpeed;
        }
    }
}


