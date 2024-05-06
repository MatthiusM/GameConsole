using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OneShotAudioPlayer : MonoBehaviour
{
    public enum PlayMode { Shuffle, Random }

    [SerializeField]
    private List<AudioClip> audioClips = new();
    private int currentIndex = 0; 

    [SerializeField]
    private PlayMode playMode;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (playMode == PlayMode.Shuffle)
        {
            ShuffleClips(); 
        }
    }

    protected void PlayClip()
    {
        if (audioClips.Count == 0) return;
        AudioClip clip = GetNextClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetNextClip()
    {
        switch (playMode)
        {
            case PlayMode.Shuffle:
                if (currentIndex >= audioClips.Count)
                {
                    ShuffleClips();
                    currentIndex = 0; 
                }
                return audioClips[currentIndex++]; 

            case PlayMode.Random:
                return audioClips[Random.Range(0, audioClips.Count)]; 

            default:
                return null;
        }
    }

    private void ShuffleClips()
    {
        if (playMode != PlayMode.Shuffle) return;

        for (int i = audioClips.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (audioClips[j], audioClips[i]) = (audioClips[i], audioClips[j]);
        }
    }
}
