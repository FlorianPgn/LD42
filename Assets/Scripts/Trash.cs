using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Material _material;
    private Color _originalColor;
    private Player _player;

    public Color SelectedColor;

    // Use this for initialization
    private void Start()
    {
        _material = GetComponent<Renderer>().materials[1];
        _originalColor = _material.color;
        _player = FindObjectOfType<Player>();
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
        _player.DiscardItem();
    }
}
