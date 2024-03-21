using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    private Vector3[] locations; 
    public float minDistance = 5f;

    public static Vector3 PlayerPosition => FindPlayerPosition();

    void Awake()
    {
        locations = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            locations[i] = transform.GetChild(i).position;
        }
    }

    public Vector3 GetRandomLocation(Vector3 currentPosition)
    {
        Vector3 randomLocation = Vector3.zero;
        int attempts = 0;
        do
        {
            randomLocation = locations[Random.Range(0, locations.Length)];
            attempts++;
            if (attempts > locations.Length * 2)
            {
                Debug.LogError("Could not find a suitable location.");
                return Vector3.zero; 
            }
        }
        while (Vector3.Distance(randomLocation, currentPosition) < minDistance);

        return randomLocation;
    }

    private static Vector3 FindPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player GameObject not found in the scene.");
            return Vector3.zero;
        }

        return player.transform.position;
    }

}
