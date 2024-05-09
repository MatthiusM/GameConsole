using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [Header("Character Meshes")]
    [SerializeField] private Mesh robberMesh;
    [SerializeField] private Mesh policeMesh;

    [Header("Character Materials")]
    [SerializeField] private Material robberMaterial;
    [SerializeField] private Material policeMaterial;

    [Header("Robber Specific GameObjects")]
    [SerializeField] private GameObject[] robberObjects;

    [Header("Police Specific GameObjects")]
    [SerializeField] private GameObject[] policeObjects;

    private SkinnedMeshRenderer characterRenderer;
    
    private void Awake()
    {
        characterRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public void SwitchToRobber()
    {
        SwitchCharacter(robberMesh, robberMaterial, robberObjects, policeObjects);
    }

    public void SwitchToPolice()
    {
        SwitchCharacter(policeMesh, policeMaterial, policeObjects, robberObjects);
    }

    private void SwitchCharacter(Mesh enableMesh, Material enableMaterial, GameObject[] enableObjects, GameObject[] disableObjects)
    {
        characterRenderer.sharedMesh = enableMesh;
        characterRenderer.material = enableMaterial;

        foreach (GameObject item in enableObjects)
        {
            item.SetActive(true);
        }

        foreach (GameObject item in disableObjects)
        {
            item.SetActive(false);
        }
    }
}
