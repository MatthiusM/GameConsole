using System.Collections;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private float forwardForce = 100f;
    [SerializeField] private float despawnDuration = 3f;

    [SerializeField] private LayerMask ignoreLayer;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * forwardForce);
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DespawnAfterDelay(despawnDuration));
    }

    IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
