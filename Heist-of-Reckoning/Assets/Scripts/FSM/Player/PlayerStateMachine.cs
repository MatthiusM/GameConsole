using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerAnimations;

namespace FintieStateMachine
{    public class PlayerStateMachine : FiniteStateMachine
    {
        [field: SerializeField] public InputManager InputManager { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public PlayerAnimatorHashes PlayerAnimatorHashes { get; private set; }

        [SerializeField, Range(1f, 5f)]
        private readonly float movementSpeed = 2.5f;

        private float currentSpeed;

        [SerializeField, Range(1f, 10f)]
        private float rotationSpeed = 8f;


        public float Speed
        {
            get => currentSpeed;
            private set => currentSpeed = value;
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
            currentSpeed = movementSpeed;
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
            currentSpeed = InputManager.IsRunning ? movementSpeed * 2 : movementSpeed;
            Debug.Log(InputManager.IsRunning);
            StartCoroutine(AdjustSpeedCoroutine(currentSpeed, 2f));
        }

        private IEnumerator AdjustSpeedCoroutine(float targetSpeed, float duration)
        {
            float time = 0;
            float startSpeed = Speed;

            while (time < duration)
            {
                Speed = Mathf.Lerp(startSpeed, targetSpeed, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            Speed = targetSpeed;
        }
    }
}


