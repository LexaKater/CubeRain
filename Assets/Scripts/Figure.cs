using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public abstract class Figure : MonoBehaviour
{
    protected float _lifeTime;
    protected Renderer _renderer;
    protected Coroutine _coroutine;

    private void Awake() => _renderer = GetComponent<Renderer>();

    public abstract void SetStartColor();

    protected abstract IEnumerator StartLifeCycle();

    protected abstract void SetLifeTime();
}
