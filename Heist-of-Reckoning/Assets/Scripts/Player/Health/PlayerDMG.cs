using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDMG : MonoBehaviour
{
    public GameObject Bullet;
  
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider Bulletcol)
    {
        if (Bulletcol.CompareTag("Player"))
        {
            Debug.Log("DMG");
            HPUI.HP -= 20;
        }
    }
}
