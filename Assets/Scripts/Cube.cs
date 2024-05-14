using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private bool _isFirstCollision = true;
    private MeshRenderer _meshRenderer;
    private float _lifeTime;
    private Coroutine _coroutine;

    public event Action<Cube> LifeEnded;

    private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Plane plane))
        {
            if (_isFirstCollision)
            {
                _isFirstCollision = false;
                SetColor(Color.green);
                SetLifeTime();

                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                _coroutine = StartCoroutine(StartLifeCycle());
            }
        }
    }

    public void SetColor(Color color) => _meshRenderer.material.color = color;

    public void SetCollision(bool isFirstCollision = true) => _isFirstCollision = isFirstCollision;

    private IEnumerator StartLifeCycle()
    {
        yield return new WaitForSeconds(_lifeTime);

        LifeEnded?.Invoke(this);
    }

    private void SetLifeTime()
    {
        float minLifeTime = 2;
        float maxLifeTime = 7;

        _lifeTime = UnityEngine.Random.Range(minLifeTime, maxLifeTime);
    }
}
