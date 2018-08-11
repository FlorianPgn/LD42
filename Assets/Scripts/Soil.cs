using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour, IClickableObj {

    private Material _material;
    private Color _originalColor;
    private Player _player;
    private Plant _growingPlant;
    private bool _hasPlant;

    public Color SelectedColor;
    public Transform PlantationPoint;

    [System.Serializable]
    public struct PlantType
    {
        public Carryable.PlantType Type;
        public Plant Plant;
    }
    public PlantType[] PlantTypes;



    // Use this for initialization
    void Start () {
        _material = GetComponent<Renderer>().material;
        _originalColor = _material.color;
        _player = FindObjectOfType<Player>();
    }

    public void PlantSeed(Seed seed)
    {
        print("Soil has plant : "+_hasPlant);
        if (!_hasPlant)
        {
            print("Trying to plant");
            int index = 0;
            foreach (PlantType p in PlantTypes)
            {
                print(p.Type + " " + seed.Type.plant + " " + (p.Type == seed.Type.plant));
                if (p.Type == seed.Type.plant)
                {
                    _growingPlant = Instantiate(PlantTypes[index].Plant, PlantationPoint.position, Quaternion.identity, PlantationPoint);
                    _hasPlant = true;
                    _player.DiscardSeed();
                    break;
                }
                index++;
            }
        }
    }

    public void Select()
    {
        Seed seed = _player.GetSeed();
        print(seed);
        if (seed != null)
        {
            PlantSeed(seed);
        }
    }

    public void Enter()
    {
        _material.color = SelectedColor;
    }

    public void Exit()
    {
        _material.color = _originalColor;
    }
}
