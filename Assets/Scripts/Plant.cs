using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Carryable
{

    public Mesh[] meshes;
    public float GrowTime;

    private MeshFilter _filter;
    private float _nextGrowTime;
    private int _currentMeshIndex;
    public bool _readyToHarvest;

    // Use this for initialization
    void Start()
    {
        IsSeed = false;
        _filter = GetComponent<MeshFilter>();
        _currentMeshIndex = 0;
        Grow();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextGrowTime)
        {
            Grow();
        }
    }

    private void Grow()
    {
        if (_currentMeshIndex == meshes.Length)
        {
            _readyToHarvest = true;
        }
        else
        {
            _currentMeshIndex++;
            _filter.mesh = meshes[_currentMeshIndex - 1];
            _nextGrowTime = Time.time + (GrowTime / meshes.Length);
        }
    }
}
