using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maxSizePool;

    private ObjectPool<Cube> _pool;
    private float _delayBetweenSpawn = 1;
    private bool _isCoroutineWork = true;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxSizePool);
    }

    private void Start() => StartCoroutine(StartCreate());

    private IEnumerator StartCreate()
    {
        WaitForSeconds wait = new WaitForSeconds(_delayBetweenSpawn);

        while (_isCoroutineWork)
        {
            GetCube();

            yield return wait;
        }
    }

    private void GetCube() => _pool.Get();

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = _spawnPosition.position + new Vector3(GetRandomPosition(), 0f, GetRandomPosition());
        cube.SetColor(Color.white);
        cube.gameObject.SetActive(true);
    }

    private float GetRandomPosition()
    {
        float minPosition = -10;
        float maxPosition = 10;

        return Random.Range(minPosition, maxPosition);
    }
}