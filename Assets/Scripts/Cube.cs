using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Cube : Figure
{
    private bool _isFirstCollision = true;

    public event Action<Cube> LifeEnded;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Plane plane))
        {
            if (_isFirstCollision)
            {
                _isFirstCollision = false;
                _renderer.material.color = Color.green;

                SetLifeTime();

                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                _coroutine = StartCoroutine(StartLifeCycle());
            }
        }
    }

    public void SetCollision(bool isFirstCollision = true) => _isFirstCollision = isFirstCollision;

    public override void SetStartColor() => _renderer.material.color = Color.white;

    protected override IEnumerator StartLifeCycle()
    {
        yield return new WaitForSeconds(_lifeTime);

        LifeEnded?.Invoke(this);
    }

    protected override void SetLifeTime()
    {
        float minLifeTime = 2;
        float maxLifeTime = 7;

        _lifeTime = UnityEngine.Random.Range(minLifeTime, maxLifeTime);
    }
}