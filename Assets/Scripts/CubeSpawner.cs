using UnityEngine;
using System.Collections;
using System;

public class CubeSpawner : Spawner<Cube>
{
    private float _delayBetweenSpawn = 1;
    private bool _isCoroutineWork = true;

    public event Action<Transform> CubeDeactivated;

    private void Start() => StartCoroutine(StartCreate());

    protected override void ActionOnRelease(Cube cube)
    {
        cube.LifeEnded -= OnRelease;
        cube.gameObject.SetActive(false);

        CubeDeactivated?.Invoke(cube.transform);
    }

    protected override void ActionOnGet(Cube cube)
    {
        cube.LifeEnded += OnRelease;

        cube.transform.position = transform.position + new Vector3(GetRandomPosition(), 0f, GetRandomPosition());
        cube.SetStartColor();
        cube.SetCollision();
        cube.gameObject.SetActive(true);
    }

    private IEnumerator StartCreate()
    {
        WaitForSeconds wait = new WaitForSeconds(_delayBetweenSpawn);

        while (_isCoroutineWork)
        {
            GetObject();

            yield return wait;
        }
    }

    private float GetRandomPosition()
    {
        float minPosition = -10;
        float maxPosition = 10;

        return UnityEngine.Random.Range(minPosition, maxPosition);
    }
}