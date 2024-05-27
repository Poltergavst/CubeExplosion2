using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;

    public int DivisionChance { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        DivisionChance = 100;
    }

    public void Initialize(Vector3 scale, Color color, int divisionChance)
    {
        transform.localScale = scale;
        DivisionChance = divisionChance;
        _renderer.material.color = color;
    }

    public bool CanBeDivided()
    {
        int maxChance = 100;
        int minChance = 0;

        return Random.Range(minChance, maxChance) <= DivisionChance;
    }
}
