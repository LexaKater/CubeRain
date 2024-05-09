using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private bool _isFirstCollision = true;
    private MeshRenderer _meshRenderer;
    private float _lifeTime;

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

                StartCoroutine(StartLifeCycle());
            }
        }
    }

    public void SetColor(Color color) => _meshRenderer.material.color = color;

    private IEnumerator StartLifeCycle()
    {
        yield return new WaitForSeconds(_lifeTime);

        gameObject.SetActive(false);
    }

    private void SetLifeTime()
    {
        float minLifeTime = 2;
        float maxLifeTime = 7;

        _lifeTime = Random.Range(minLifeTime, maxLifeTime);
    }
}
