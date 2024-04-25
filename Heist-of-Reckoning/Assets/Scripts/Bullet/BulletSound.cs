using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{
    [SerializeField] private PoliceAnimationEvents policeAnimationEvents;
    [SerializeField] private AudioClip shootSound; 
    private AudioSource audioSource;

    private void Awake()
    {
        if (!TryGetComponent<AudioSource>(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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
        if (shootSound == null)
        {
            return;
        }
        audioSource.PlayOneShot(shootSound);
    }
}
