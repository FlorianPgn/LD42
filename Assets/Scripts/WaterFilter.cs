using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFilter : MonoBehaviour
{
    private Material _material;
    private Material _waterMaterial;
    private Color _originalColor;
    private Color _originalWaterColor;
    private Color _waterColorWhite;
    private Game _game;

    public bool Running;
    public bool CanRun;
    public Animator Animator;
    public Color SelectedColor;
    // Use this for initialization
    void Start()
    {
        _material = GetComponent<Renderer>().materials[1];
        _originalColor = _material.color;
        _waterMaterial = GetComponent<Renderer>().materials[2];
        _originalWaterColor = _waterMaterial.color;
        _game = FindObjectOfType<Game>();
        _waterColorWhite = Color.white;
        _waterColorWhite.a = 0.4f;
    }

    private void Update()
    {
        _waterMaterial.color = Running ? _originalWaterColor : _waterColorWhite;
        Animator.SetBool("On", Running);
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
        if (CanRun)
        {
            Running = !Running;
            Animator.SetBool("On", Running);
            _game.TradeWater(Running);
        }
    }
}
