using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;

    public Action JumpEvent;

    public Action CrouchEvent;

    public Action RunEvent;
    
    public Vector2 MovementValue { get; private set; }

    public bool IsRunning { get; private set; }

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }

        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }
    private void InvokeIfPerformed(Action eventToInvoke, InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        eventToInvoke?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        InvokeIfPerformed(JumpEvent, context);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        InvokeIfPerformed(CrouchEvent, context);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                IsRunning = true;
                RunEvent.Invoke();
                break;
            case InputActionPhase.Canceled:
                IsRunning = false;
                RunEvent.Invoke();
                break;
        }        
    }
}
