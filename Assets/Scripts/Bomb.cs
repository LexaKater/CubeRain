using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : Figure
{
    private Color _newColor;

    public event Action<Bomb> LifeEnded;

    public void Init()
    {
        SetStartColor();
        SetLifeTime();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartLifeCycle());
    }

    public override void SetStartColor()
    {
        _newColor.a = 1;
        _renderer.material.color = _newColor;
    }

    protected override IEnumerator StartLifeCycle()
    {
        float elapsedTime = 0;

        while (elapsedTime < _lifeTime)
        {
            elapsedTime += Time.deltaTime;

            ChangeAlpha();

            yield return null;
        }

        LifeEnded?.Invoke(this);
    }

    protected override void SetLifeTime()
    {
        float minLifeTime = 2;
        float maxLifeTime = 5;

        _lifeTime = UnityEngine.Random.Range(minLifeTime, maxLifeTime);
    }

    private void ChangeAlpha()
    {
        float speed = 0.001f;

        _newColor.a -= speed;
        _renderer.material.color = _newColor;
    }
}