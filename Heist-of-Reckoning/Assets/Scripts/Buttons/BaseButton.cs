using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BaseButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        if(TryGetComponent<Button>(out button))
        {
            Debug.LogError("no button compoeonet");
        }
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClickButton);
    }

    protected virtual void OnClickButton()
    {
        throw new NotImplementedException();
    }



}
