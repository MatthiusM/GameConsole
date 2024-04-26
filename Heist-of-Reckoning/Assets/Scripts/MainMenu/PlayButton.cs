using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

public class SceneLoader : BaseButton
{
    [SerializeField] 
    private string sceneToLoad;

    protected override void OnClickButton()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}