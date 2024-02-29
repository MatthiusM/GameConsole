using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyObjectPool : AbstractObjectPool
{
    protected override int DefaultCapacity => 10;

    protected override int MaxSize => 15;

    protected override bool CollectionCheck => true;

    private MoneySpawner moneySpawner;
    private GameObject moneyContainer;

    private new void Awake()
    {
        GameObject spawnMoneyObject = GameObject.FindGameObjectWithTag("MoneySpawner");
        if (spawnMoneyObject != null)
        {
            moneySpawner = spawnMoneyObject.GetComponent<MoneySpawner>();
        }

        moneyContainer = new GameObject("MoneyContainer");

        base.Awake();
    }

    protected override GameObject CreateObject()
    {
        GameObject money = Instantiate(prefab, moneySpawner.SpawnPoint.position, Quaternion.identity, moneyContainer.transform);
        if (money.TryGetComponent(out Money component))
        {
            component.SetPool(this);
        }
        return money;
    }

    protected override void OnDestroyObject(GameObject obj)
    {
        Destroy(obj);
    }

    protected override void OnGetObject(GameObject obj)
    {
        obj.transform.SetPositionAndRotation(moneySpawner.SpawnPoint.position, Quaternion.identity);

        obj.SetActive(true);
    }

    protected override void OnReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
