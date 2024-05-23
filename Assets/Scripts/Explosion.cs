using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private float _explosForce;
    [SerializeField] private float _explosRadius;

    public void Explode(Bomb bomb)
    {
        Instantiate(_effect, bomb.transform.position, bomb.transform.rotation);

        foreach (Rigidbody explodableObject in GetExplodedObjects(bomb.transform.position, _explosRadius))
            explodableObject.AddExplosionForce(_explosForce, bomb.transform.position, _explosRadius);
    }

    private IEnumerable<Rigidbody> GetExplodedObjects(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                yield return hit.attachedRigidbody;
        }
    }
}