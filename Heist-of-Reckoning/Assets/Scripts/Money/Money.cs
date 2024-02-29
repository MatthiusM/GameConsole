using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Money : MonoBehaviour
{
    [SerializeField] private float despawnDuration = 3f;

    private MoneySpawner moneySpawner; 

    private ObjectPool<GameObject> pool;

    void Awake()
    {
        GameObject spawnMoneyObject = GameObject.FindGameObjectWithTag("MoneySpawner");
        if (spawnMoneyObject != null)
        {
            moneySpawner = spawnMoneyObject.GetComponent<MoneySpawner>();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DespawnAfterDelay(despawnDuration * 3));
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DespawnAfterDelay(despawnDuration));
    }

    IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (moneySpawner != null && gameObject.activeSelf)
        {
            pool.Release(this.gameObject);
        }
    }

    public void SetPool(MoneyObjectPool moneyObjectPool)
    {
        this.pool = moneyObjectPool.Pool;
    }
}
