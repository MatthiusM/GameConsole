using FiniteStateMachine;
using System.Collections;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    public float detectionRadius = 10.0f;

    private void Start()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        if (collider == null)
        {
            return;
        }
        collider.radius = detectionRadius;
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Spotted.");
            AlertNearbyPolice();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Escaped.");
        }
    }

    private void AlertNearbyPolice()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider hit in hits)
        {
            PoliceStateMachine police = hit.GetComponent<PoliceStateMachine>();
            if (police != null)
            {
                Debug.Log("In Pursuit.");
                police.SetCurrentState(new PolicePursueState(police));
                StartCoroutine(ResumePatrol(police));
            }
        }
    }
    IEnumerator ResumePatrol(PoliceStateMachine stateMachine)
    {
        yield return new WaitForSeconds(10);
        if (stateMachine.CurrentState.GetType() == typeof(PolicePursueState))
        {
            stateMachine.SetCurrentState(new PoliceWanderState(stateMachine));
        }

    }
}