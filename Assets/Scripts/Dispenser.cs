using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour, IClickableObj {

    public Carryable.SeedType[] Seeds;
    public Carryable SeedPrefab;
    public Color SelectedColor;

    private Material _material;
    private Material _materialHover;
    private Color _originalColor;
    private int _currentIndex = 0;
    private Player _player;

	// Use this for initialization
	void Start () {
        _material = GetComponent<Renderer>().materials[3];
        _materialHover = GetComponent<Renderer>().materials[0];
        _originalColor = _materialHover.color;
        _player = FindObjectOfType<Player>();
        UpdateColor();
    }

    public void NextSeed()
    {
        _currentIndex = (_currentIndex + 1) % Seeds.Length;
        UpdateColor();
    }

    private void UpdateColor()
    {
        _material.color = Seeds[_currentIndex].color;
    }

    public void Enter()
    {
        _materialHover.color = SelectedColor;
    }

    public void Select()
    {
        SeedPrefab.Type = Seeds[_currentIndex];
        SeedPrefab.IsSeed = true;
        _player.Give(SeedPrefab);
    }

    public void Exit()
    {
        _materialHover.color = _originalColor;
    }
}
