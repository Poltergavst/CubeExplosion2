using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Теперь реализуем именно взрыв, в случае если не происходит создание новых кубов.
//При нажатии на куб, он взрывается и исчезает, толкая другие кубы в разные стороны.
//Взрыв, действует в некоторой области. Чем дальше объект от центра взрыва, тем меньше силы будет приложено на объект.
//Чем меньше куб, тем больше радиус и сила с которой он раскидывает другие кубы.

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
