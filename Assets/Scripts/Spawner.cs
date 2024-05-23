using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected int PoolCapacity;
    [SerializeField] protected int MaxSizePool;
    [SerializeField] protected TextMeshPro _countFigure;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(Prefab),
            actionOnGet: (figure) => ActionOnGet(figure),
            actionOnRelease: (figure) => ActionOnRelease(figure),
            actionOnDestroy: (figure) => Destroy(figure),
            collectionCheck: true,
            defaultCapacity: PoolCapacity,
            maxSize: MaxSizePool);
    }

    private void Update() => ShowCountFigure();

    protected void ShowCountFigure() => _countFigure.text = $"{Prefab.name} Активные - {_pool.CountActive}, все - {_pool.CountAll}";

    protected void OnRelease(T figure) => _pool.Release(figure);

    protected void GetObject() => _pool.Get();

    protected abstract void ActionOnRelease(T figure);

    protected abstract void ActionOnGet(T figure);
}