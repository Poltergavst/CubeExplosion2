using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private int _baseExplosionRadius = 100;
    [SerializeField] private int _baseExplosionForce = 100;
    private float _explosionRadius;
    private float _explosionForce;

    public void Explode(Transform explodingCube)
    {
        ModifyExplosionParameteres(explodingCube.localScale);

        foreach (Rigidbody affectedCube in GetExplodableObjects(explodingCube.position)) 
        {
            affectedCube.AddExplosionForce(_explosionForce, explodingCube.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 explosionPosition) 
    {
        Collider[] hits = Physics.OverlapSphere(explosionPosition, _explosionRadius);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }
        
        return cubes;
    }

    private void ModifyExplosionParameteres(Vector3 cubeScale)
    {
        _explosionRadius = _baseExplosionRadius / cubeScale.magnitude;
        _explosionForce = _baseExplosionForce / cubeScale.magnitude;
    }
}
