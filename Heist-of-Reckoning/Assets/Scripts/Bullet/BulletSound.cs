using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity; 

public class BulletSound : MonoBehaviour
{
    PoliceAnimationEvents policeAnimationEvents;

    [SerializeField]
    private EventReference shootSoundEvent;

    private void Awake()
    {
        policeAnimationEvents = GetComponent<PoliceAnimationEvents>();
    }

    private void OnEnable()
    {
        policeAnimationEvents.OnShoot += PlayShootSound;
    }

    private void OnDisable()
    {
        policeAnimationEvents.OnShoot -= PlayShootSound;
    }

    private void PlayShootSound()
    {
        RuntimeManager.PlayOneShot(shootSoundEvent, transform.position);
    }
}
