using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private ObjectPool<GameObject> pool;
    private Rigidbody rb;
    [SerializeField] private GameObject gunShotParticlePrefab;
    private readonly float forwardForce = 500f;
    private Transform playerTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    private void OnEnable()
    {
        if (gunShotParticlePrefab != null)
        {
            Instantiate(gunShotParticlePrefab, transform.position, Quaternion.identity);
        }

        if (playerTransform != null)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            rb.AddForce(directionToPlayer * forwardForce); 
        }
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
