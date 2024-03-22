using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPUI : MonoBehaviour
{
    public Slider HPBar;
    public static int HP = 100;
    private int MaxHP = 100;
    void Start()
    {
        HPBar.maxValue = MaxHP;
        HPBar.value = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = HP;

        if (HP < 0)
        {
            HPBar.value = 0;
        }
    }
}
