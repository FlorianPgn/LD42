using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystalizer : MonoBehaviour {

    private Material _material;
    private Color _originalColor;
    private Player _player;
    private Game _game;

    public Color SelectedColor;

    // Use this for initialization
    private void Start()
    {
        _material = GetComponent<Renderer>().materials[1];
        _originalColor = _material.color;
        _player = FindObjectOfType<Player>();
        _game = FindObjectOfType<Game>();
    }

    public void Enter()
    {
        _material.color = SelectedColor;
    }

    public void Exit()
    {
        _material.color = _originalColor;
    }

    public void Select()
    {
        Plant playerPlant = _player.GetPlant();
        if (playerPlant != null)
        {
            if (playerPlant.Type.plant == Carryable.PlantType.CRYSTAL)
            {
                if(_game.AddCrystal())
                {
                    _player.DiscardItem();
                }
            }
        }
    }
}
