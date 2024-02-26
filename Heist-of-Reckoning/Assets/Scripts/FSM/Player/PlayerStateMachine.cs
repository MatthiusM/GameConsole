using FintieStateMachine;
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

        [SerializeField, Range(0f, 10f)]
        private float movementSpeed = 5f;

        [SerializeField, Range(1f, 10f)]
        private float rotationSpeed = 8f;

        public float Speed
        {
            get => movementSpeed;
            private set => movementSpeed = Mathf.Clamp(value, 0f, 10f);
        }

        public float RotationSpeed
        {
            get => rotationSpeed;
            private set => rotationSpeed = Mathf.Clamp(value, 0f, 10f);
        }

        private void Start()
        {
            MainCameraTransform = Camera.main.transform;
            PlayerAnimatorHashes = new PlayerAnimatorHashes();
            SetCurrentState(new PlayerGroundedState(this));
        }

    }

}


