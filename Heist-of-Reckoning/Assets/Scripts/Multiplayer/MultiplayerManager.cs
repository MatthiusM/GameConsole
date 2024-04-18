using Cinemachine;
using FiniteStateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField] private LayerMask[] cameraLayers;
    private int playerCount = 0;

    void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();

        GameObject playerGameObject = GameObject.Find("Player");
        if (playerGameObject == null)
        {
            Debug.LogError("Player GameObject not found in the scene.");
            return;
        }

        SetupPlayerComponents(playerGameObject);
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += ConfigurePlayer; //line 28
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= ConfigurePlayer;
    }

    private void SetupPlayerComponents(GameObject playerGameObject)
    {
        if (!playerGameObject.TryGetComponent(out PlayerInput playerInput))
        {
            Debug.LogError("PlayerInput component not found on the Player GameObject.");
            return;
        }

        ConfigurePlayer(playerInput);
    }

    private void ConfigurePlayer(PlayerInput player)
    {
        if (playerCount >= cameraLayers.Length)
        {
            Debug.LogError("Maximum number of players exceeded.");
            return;
        }

        ConfigureCamera(player);
        ConfigureInput(player);

        if (playerCount == 0)
        {
            if (player.gameObject.TryGetComponent(out PlayerStateMachine playerStateMachine))
            {
                playerStateMachine.IsPolice = true;
            }

            CharacterSwitcher characterSwitcher = player.gameObject.GetComponentInChildren<CharacterSwitcher>();
            if (characterSwitcher)
            {
                characterSwitcher.SwitchToPolice();
            }
        }

        playerCount++;
    }

    private void ConfigureCamera(PlayerInput player)
    {
        Transform playerTransform = player.gameObject.transform;
        CinemachineFreeLook freeLookCam = playerTransform.GetComponentInChildren<CinemachineFreeLook>();
        Camera playerCamera = playerTransform.GetComponentInChildren<Camera>();
        int layerToAdd = (int)Mathf.Log(cameraLayers[playerCount].value, 2);

        freeLookCam.gameObject.layer = layerToAdd;
        playerCamera.cullingMask = ~0;

        for (int i = 0; i < cameraLayers.Length; i++)
        {
            if (i != playerCount)
            {
                int layerNumberToExclude = (int)Mathf.Log(cameraLayers[i].value, 2);
                playerCamera.cullingMask &= ~(1 << layerNumberToExclude);
            }
        }

        Debug.Log($"Camera for player {playerCount + 1} set: CinemachineFreeLook layer {layerToAdd}, Camera cullingMask modified.");
    }

    private void ConfigureInput(PlayerInput player)
    {
        Transform playerTransform = player.gameObject.transform;
        CinemachineInputHandler inputHandler = playerTransform.GetComponentInChildren<CinemachineInputHandler>();
        inputHandler.horizontal = player.actions.FindAction("Look");

        Debug.Log($"Input for player {playerCount + 1} configured: CinemachineInputHandler action set.");
    }
}
