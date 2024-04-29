using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private ObjectPool<GameObject> pool;
    private Rigidbody rb;
    [SerializeField] private GameObject gunShotParticlePrefab;
    private readonly float forwardForce = 500f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (gunShotParticlePrefab != null)
        {
            Instantiate(gunShotParticlePrefab, transform.position, Quaternion.identity);
        }

        rb.AddForce(transform.forward * forwardForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Police") || other.CompareTag("Pistol") || other.CompareTag("Bullet")) { return; }
        Debug.Log(other.name);
        pool.Release(this.gameObject);
    }

    public void SetPool(BulletObjectPool bulletObjectPool)
    {
        this.pool = bulletObjectPool.Pool;
    }
}
