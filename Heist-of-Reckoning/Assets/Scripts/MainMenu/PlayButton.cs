using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SceneLoader : MonoBehaviour, IPointerClickHandler
{
    public string sceneToLoad;

    public void OnPointerClick(PointerEventData eventData)
    {
        
        LoadScene();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}