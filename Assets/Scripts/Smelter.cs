using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : MonoBehaviour
{
    private Material _material;
    private Color _originalColor;
    private Player _player;
    private Game _game;

    public Animator Animator;
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
        _game.ToggleTooltip(Game.Machine.SMELTER);
    }

    public void Exit()
    {
        _material.color = _originalColor;
        _game.ToggleTooltip(Game.Machine.SMELTER);
    }

    public void Select()
    {
        Plant playerPlant = _player.GetPlant();
        if (playerPlant != null)
        {
            if (playerPlant.Type.plant == Carryable.PlantType.METAL)
            {
                if (_game.AddMetal())
                {
                    _player.DiscardItem();
                    Animator.SetTrigger("Play");
                }
            }
        }
    }
}
