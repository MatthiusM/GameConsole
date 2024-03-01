using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    public GameObject LoseScreen;
    public static bool Lost = false;
    void Start()
    {
        if (LoseScreen != null)
        {
            LoseScreen.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (MoneySpawner.MoneyUI == -999999)
        //{
        //    LoseScreen.gameObject.SetActive(true);
        //    Lost = true;
        //}
    }
}
