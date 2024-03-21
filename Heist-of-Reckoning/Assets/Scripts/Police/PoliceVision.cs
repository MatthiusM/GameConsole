using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceVision : MonoBehaviour
{
    [SerializeField]private float visionRange = 10f;
    [SerializeField] private float fieldOfViewAngle = 30f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float heightOffset = 1f;
    private readonly float detectionTimeThreshold = 0.5f;

    private float detectionTime = 0f;
    private bool isPlayerDetected = false;

    public Action OnPlayerDetected;

    void Update()
    {
        DetectPlayer();
        HandleDetection();
    }

    void HandleDetection()
    {
        if (!isPlayerDetected)
        {
            detectionTime = 0f;
            isPlayerDetected = false;
            return;
        }

        detectionTime += Time.deltaTime;
        if (detectionTime < detectionTimeThreshold)
        {
            isPlayerDetected = false;
            return;
        }

        OnPlayerDetected?.Invoke();
        detectionTime = 0f;
        isPlayerDetected = false;
    }

    List<Vector3> CalculateRayDirections()
    {
        List<Vector3> rayDirections = new();
        int rayCount = 10;
        float angleStep = fieldOfViewAngle / rayCount;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 rayDirection = Quaternion.AngleAxis(-fieldOfViewAngle / 2 + angleStep * i, transform.up) * transform.forward;
            rayDirections.Add(rayDirection);
        }

        return rayDirections;
    }

    void DetectPlayer()
    {
        Vector3 origin = transform.position + Vector3.up * heightOffset;
        var rayDirections = CalculateRayDirections();

        foreach (Vector3 rayDirection in rayDirections)
        {
            if (Physics.Raycast(origin, rayDirection, out RaycastHit hit, visionRange, playerMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isPlayerDetected = true;
                    Debug.Log(isPlayerDetected);
                    break;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Vector3 origin = transform.position + Vector3.up * heightOffset;
        var rayDirections = CalculateRayDirections();

        Gizmos.color = Color.blue;

        foreach (Vector3 rayDirection in rayDirections)
        {
            Gizmos.DrawRay(origin, rayDirection * visionRange);
        }
    }
}
