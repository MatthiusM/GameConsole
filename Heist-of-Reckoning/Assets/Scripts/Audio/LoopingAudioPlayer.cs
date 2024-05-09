using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LoopingAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip loopClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected void StartLoop()
    {
        if (loopClip == null) return;
        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    protected void StopLoop()
    {
        audioSource.Stop();
    }
}
