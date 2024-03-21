using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAnimationEvents : MonoBehaviour
{
    public event Action OnShoot;

    public void Shoot()
    {
        OnShoot?.Invoke();
    }
}
