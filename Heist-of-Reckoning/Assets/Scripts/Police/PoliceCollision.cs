using UnityEngine;
using System;
using FiniteStateMachine;

public class PoliceCollision : MonoBehaviour
{
    public Action onPlayerEnterTrigger;
    public Action onPlayerExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStateMachine playerStateMachine = other.GetComponent<PlayerStateMachine>();
        if (other.gameObject.CompareTag("Player") && playerStateMachine != null && !playerStateMachine.IsPolice)
        {
            onPlayerEnterTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerStateMachine playerStateMachine = other.GetComponent<PlayerStateMachine>();
        if (other.gameObject.CompareTag("Player") && playerStateMachine != null && !playerStateMachine.IsPolice)
        {
            onPlayerExitTrigger?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerStateMachine playerStateMachine = other.GetComponent<PlayerStateMachine>();
        if (other.gameObject.CompareTag("Player") && playerStateMachine != null && !playerStateMachine.IsPolice)
        {
            onPlayerEnterTrigger?.Invoke();
        }
    }
}
