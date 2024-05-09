using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


public class SceneLoader : BaseButton
{
    [SerializeField] 
    private string sceneToLoad;

    

    protected override void OnClickButton()
    {
        
        int randomNumber = Random.Range(1, 5);
        SceneManager.LoadScene(randomNumber);
    }
}