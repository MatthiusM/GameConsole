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
            GameObject particleInstance = Instantiate(gunShotParticlePrefab, transform.position, Quaternion.identity);

            if (particleInstance.TryGetComponent(out ParticleSystem gunShotParticle))
            {
                gunShotParticle.Play();
                StartCoroutine(StopParticleSystemAfterDelay(gunShotParticle, 0.1f));
            }
        }

        rb.AddForce(transform.forward * forwardForce);
    }

    private IEnumerator StopParticleSystemAfterDelay(ParticleSystem particleSystem, float delay)
    {
        yield return new WaitForSeconds(delay);
        particleSystem.Stop();

        Destroy(particleSystem.gameObject, particleSystem.main.startLifetime.constantMax);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Police") || other.CompareTag("Pistol")) { return; }
        pool.Release(this.gameObject);
    }

    public void SetPool(BulletObjectPool bulletObjectPool)
    {
        this.pool = bulletObjectPool.Pool;
    }
}
