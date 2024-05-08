using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : OneShotAudioPlayer
{
    private CharacterController controller;
    public float stepInterval = 0.3f;
    private float stepTimer;

    void Start()
    {
        if (!TryGetComponent<CharacterController>(out controller))
        {
            Debug.LogError("No CharacterController");
        }
    }

    void Update()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0)
            {
                base.PlayClip();
                stepTimer = Random.Range(0.2f, 0.4f);
            }
        }
    }
}
