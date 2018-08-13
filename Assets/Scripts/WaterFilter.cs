using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFilter : MonoBehaviour
{
    private Material _material;
    private Color _originalColor;
    private Game _game;
    private bool _running;

    public bool CanRun;
    public Animator Animator;
    public Color SelectedColor;
    // Use this for initialization
    void Start()
    {
        _material = GetComponent<Renderer>().materials[1];
        _originalColor = _material.color;
        _game = FindObjectOfType<Game>();

    }

    public void Enter()
    {
        _material.color = SelectedColor;
        _game.ToggleTooltip(Game.Machine.WATER_FILTER);
    }

    public void Exit()
    {
        _material.color = _originalColor;
        _game.ToggleTooltip(Game.Machine.WATER_FILTER);
    }

    public void Select()
    {
        if(CanRun)
        {
            _running = !_running;
            Animator.SetBool("On",_running);
            _game.TradeWater(_running);
        }
    }
}
