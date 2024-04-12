using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField] private LayerMask[] playerLayers; // Layers to exclude from culling, except for the current player
    private int playerCount = 0;

    void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        if (playerCount >= playerLayers.Length)
        {
            Debug.LogError("Maximum number of players exceeded.");
            return;
        }

        Transform playerTransform = player.gameObject.transform;
        int layerToAdd = (int)Mathf.Log(playerLayers[playerCount].value, 2);

        Debug.Log($"Assigning player {playerCount + 1} to layer {layerToAdd}");

        CinemachineFreeLook freeLookCam = playerTransform.GetComponentInChildren<CinemachineFreeLook>();
        freeLookCam.gameObject.layer = layerToAdd;

        Camera playerCamera = playerTransform.GetComponentInChildren<Camera>();

        playerCamera.cullingMask = ~0; 
        for (int i = 0; i < playerLayers.Length; i++)
        {
            if (i != playerCount) 
                playerCamera.cullingMask &= ~(1 << (int)Mathf.Log(playerLayers[i].value, 2));
        }

        CinemachineInputHandler inputHandler = playerTransform.GetComponentInChildren<CinemachineInputHandler>();
        inputHandler.horizontal = player.actions.FindAction("Look");

        Debug.Log($"Player {playerCount + 1} components set: CinemachineFreeLook layer {layerToAdd}, Camera cullingMask modified, CinemachineInputHandler action set");

        playerCount++;
    }
}
