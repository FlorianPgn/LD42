using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Carryable
{
    private Material _material;

    private void Start()
    {
        GetComponent<Renderer>().material = Type.material;
        IsSeed = true;
    }
}
