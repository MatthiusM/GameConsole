using UnityEngine;
using System;

public class PoliceCollision : MonoBehaviour
{
    public Action onPlayerEnterTrigger;
    public Action onPlayerExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlayerEnterTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlayerExitTrigger?.Invoke();
        }
    }
}
