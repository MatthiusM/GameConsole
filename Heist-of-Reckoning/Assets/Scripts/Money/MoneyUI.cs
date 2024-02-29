using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI MoneyUIText;
    float MoneyUICount;
    

    void Start()
    {
        //Checks if it can find the Text box
        if (MoneyUIText == null)
        {
            Debug.LogError("MoneyUIText is not assigned!");
        }
        
    }

    void Update()
    {
        MoneyUICount = MoneySpawner.MoneyUI;
        
        MoneyUIText.text = MoneyUICount.ToString("N0");
    }
}
