using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MoneyObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject moneyPrefab;
    public ObjectPool<GameObject> MoneyPool {get; private set;}

    private MoneySpawner moneySpawner;
    private GameObject moneyContainer;

    private void Awake()
    {
        GameObject spawnMoneyObject = GameObject.FindGameObjectWithTag("MoneySpawner");
        if (spawnMoneyObject != null)
        {
            moneySpawner = spawnMoneyObject.GetComponent<MoneySpawner>();
        }

        moneyContainer = new GameObject("MoneyContainer");

        MoneyPool = new ObjectPool<GameObject>(
            createFunc: CreateMoney,
            actionOnGet: OnGetMoney,
            actionOnRelease: OnReleaseMoney,
            actionOnDestroy: OnDestroyMoney,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 15
        );
    }

    private GameObject CreateMoney()
    {
        GameObject money = Instantiate(moneyPrefab, moneySpawner.SpawnPoint.position, Quaternion.identity, moneyContainer.transform);
        if (money.TryGetComponent(out Money component))
        {
            component.SetPool(MoneyPool);
        }
        return money;
    }

    private void OnGetMoney(GameObject obj)
    {
        obj.transform.SetPositionAndRotation(moneySpawner.SpawnPoint.position, Quaternion.identity);

        obj.SetActive(true);
    }

    private void OnReleaseMoney(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyMoney(GameObject obj)
    {
        Destroy(obj);
    }
}
