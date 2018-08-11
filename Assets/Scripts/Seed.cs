using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Carryable
{
    private Material _material;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = Type.color;
        IsSeed = true;
    }
}
