using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;

    public Action JumpEvent;
    
    public Vector2 MovementValue { get; private set; }

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

    void Update()
    {
        
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
}
