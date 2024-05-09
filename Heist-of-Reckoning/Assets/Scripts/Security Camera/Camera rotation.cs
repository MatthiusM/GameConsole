using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerarotation : MonoBehaviour
{
    public float speed = 50.0f;
    private float range = 60.0f; 
    private float startingAngle = 0.0f;
    private float currentAngle = 0.0f;
    private int direction = 1; 
    void Start()
    {
        startingAngle = transform.eulerAngles.y;
        currentAngle = 0.0f;
    }

    void Update()
    {
        currentAngle += direction * speed * Time.deltaTime;
        if (currentAngle > range)
        {
            currentAngle = range;
            direction = -1;
        }
        else if (currentAngle < -range)
        {
            currentAngle = -range;
            direction = 1; 
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, startingAngle + currentAngle, transform.eulerAngles.z);
    }
}

