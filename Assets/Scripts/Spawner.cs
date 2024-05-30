using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Exploder))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    private Ray _ray;
    private RaycastHit _hitInfo;
    private Camera _camera;
    private Cube _interactedCube;
    private Exploder _exploder;

    private int _minToSpawn = 2;
    private int _maxToSpawn = 6;
    private int _cubesToSpawn;

    private int _scaleDivider = 2;
    private int _divisionChanceReductor = 2;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _camera = Camera.main;
    }

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0) && Physics.Raycast(_ray, out _hitInfo))
        {
            if (_hitInfo.transform.TryGetComponent(out _interactedCube) == true)
            {
                if(_interactedCube.CanBeDivided())
                {
                    SpawnCubes(_interactedCube);
                }
                else
                {
                    _exploder.Explode(_interactedCube.transform);
                }

                Destroy(_interactedCube.gameObject);
            }
        }
    }

    private void SpawnCubes(Cube originCube)
    {
        Cube newCube;
        Rigidbody[] newCubes;

        _cubesToSpawn = Random.Range(_minToSpawn, _maxToSpawn + 1);
        newCubes = new Rigidbody[_cubesToSpawn];

        for (int i = 0; i < _cubesToSpawn; i++)
        {
            newCube = Instantiate(_prefab, originCube.transform.position, Quaternion.identity);

            newCube.Initialize(originCube.transform.localScale / _scaleDivider, Random.ColorHSV(), originCube.DivisionChance / _divisionChanceReductor);

            newCubes[i] = newCube.GetComponent<Rigidbody>();
        }
    }
}
