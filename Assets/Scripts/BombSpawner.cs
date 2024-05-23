using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cube;
    [SerializeField] private Explosion _explosion;

    private Transform _newPosition;

    private void OnEnable() => _cube.CubeDeactivated += SetPosition;

    private void OnDisable() => _cube.CubeDeactivated -= SetPosition;

    protected override void ActionOnRelease(Bomb bomb)
    {
        bomb.LifeEnded -= OnRelease;
        bomb.gameObject.SetActive(false);
        _explosion.Explode(bomb);
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        bomb.LifeEnded += OnRelease;
        bomb.transform.position = _newPosition.position;
        bomb.gameObject.SetActive(true);
        bomb.Init();
    }

    private void SetPosition(Transform bomb)
    {
        _newPosition = bomb;
        GetObject();
    }
}