using UnityEngine;
using UnityEngine.Pool;

public abstract class AbstractObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;

    public ObjectPool<GameObject> Pool { get; private set; }

    protected abstract int DefaultCapacity { get; }
    protected abstract int MaxSize { get; }

    protected abstract bool CollectionCheck { get; }

    protected abstract GameObject CreateObject();
    protected abstract void OnGetObject(GameObject obj);
    protected abstract void OnReleaseObject(GameObject obj);
    protected abstract void OnDestroyObject(GameObject obj);

    protected virtual void Awake()
    {
        InitializePool(CollectionCheck, DefaultCapacity, MaxSize);
    }

    private void InitializePool(bool collectionCheck, int defaultCapacity, int maxSize)
    {
        Pool = new ObjectPool<GameObject>(
            createFunc: CreateObject,
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject,
            collectionCheck: collectionCheck,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }
}
