using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WinTrigger : MonoBehaviour
{
    public GameObject WinScreen;
    public static bool Won = false;
    void Start()
    {
        if (WinScreen != null)
        {
            WinScreen.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        print("SOMETHING COLLIDED");
        if (other.CompareTag("Player")) 
        {
            print("IT WAS A PLAYER");

            ShowUI();
        }
    }

    void ShowUI()
    {
        if (WinScreen != null)
        {
            WinScreen.gameObject.SetActive(true);
            Won = true;

        }
    }

   
}
