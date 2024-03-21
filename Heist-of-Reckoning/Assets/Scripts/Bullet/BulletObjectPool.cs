using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : AbstractObjectPool
{
    [SerializeField] private Transform spawn;

    [SerializeField] PoliceAnimationEvents policeAnimationEvents;

    protected override int DefaultCapacity => 15;

    protected override int MaxSize => 20;

    protected override bool CollectionCheck => true;

    protected override GameObject CreateObject()
    {
        GameObject money = Instantiate(prefab, spawn.position, Quaternion.identity);
        if (money.TryGetComponent(out Bullet component))
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
        obj.transform.SetPositionAndRotation(spawn.position, Quaternion.Euler(90, 0, 0));

        obj.SetActive(true);
    }

    protected override void OnReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnEnable()
    {
        policeAnimationEvents.OnShoot += SpawnBulletPrefab;
    }

    private void OnDisable()
    {
        policeAnimationEvents.OnShoot -= SpawnBulletPrefab;
    }

    private void SpawnBulletPrefab()
    {
        Pool.Get();
    }
}
