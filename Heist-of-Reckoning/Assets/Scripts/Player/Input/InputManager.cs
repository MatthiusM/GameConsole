using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    enum ActionName
    {
        Jump,
        Move,
        Crouch,
        Run
    }

    public Action JumpEvent;
    public Action CrouchEvent;
    public Action RunEvent;
    public Vector2 MovementValue { get; private set; }
    public bool IsRunning { get; private set; }

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        if (Enum.TryParse(context.action.name, true, out ActionName actionName))
        {
            switch (actionName)
            {
                case ActionName.Jump:
                    HandleJump(context);
                    break;
                case ActionName.Move:
                    HandleMove(context);
                    break;
                case ActionName.Crouch:
                    HandleCrouch(context);
                    break;
                case ActionName.Run:
                    HandleRun(context);
                    break;
            }
        }
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        InvokeIfPerformed(JumpEvent, context);
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    private void HandleCrouch(InputAction.CallbackContext context)
    {
        InvokeIfPerformed(CrouchEvent, context);
    }

    private void HandleRun(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                IsRunning = true;
                RunEvent?.Invoke();
                break;
            case InputActionPhase.Canceled:
                IsRunning = false;
                RunEvent?.Invoke();
                break;
        }
    }

    private void InvokeIfPerformed(Action eventToInvoke, InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            eventToInvoke?.Invoke();
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
