﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour, IClickableObj {

    private Material _material;
    private Color _originalColor;
    private Player _player;
    private Game _game;

    public Animator Animator;
    public Color SelectedColor;

    // Use this for initialization

    private void Start () {
        _material = GetComponent<Renderer>().materials[1];
        _originalColor = _material.color;
        _player = FindObjectOfType<Player>();
        _game = FindObjectOfType<Game>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Enter()
    {
        _material.color = SelectedColor;
        _game.ToggleTooltip(Game.Machine.TRANSPORTER);
    }

    public void Exit()
    {
        _material.color = _originalColor;
        _game.ToggleTooltip(Game.Machine.TRANSPORTER);
    }

    public void Select()
    {
        Plant playerPlant = _player.GetPlant();
        if (playerPlant != null) {
            if (playerPlant.Type.plant == Carryable.PlantType.ENERGY)
            {
                _game.AddEnergy(50);
                _player.DiscardItem();
                Animator.SetTrigger("PlayE");
            }
            if (playerPlant.Type.plant == Carryable.PlantType.OXYGEN)
            {
                if(_game.AddOxygen(40))
                {
                    _player.DiscardItem();
                    Animator.SetTrigger("PlayO");
                } else
                {
                    _game.NeedEnergy();
                }
            }
        }
    }
}
