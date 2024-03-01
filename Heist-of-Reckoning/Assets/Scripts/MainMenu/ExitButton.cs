using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
{
    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        ExitGame();
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
