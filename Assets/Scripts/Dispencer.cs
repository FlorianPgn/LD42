using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispencer : MonoBehaviour, IClickableObj {

    public Carryable.SeedType[] Seeds;
    public Carryable SeedPrefab;

    private Material _material;
    private int _currentIndex = 0;
    private Player _player;

	// Use this for initialization
	void Start () {
        _material = GetComponent<Renderer>().material;
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
       
    }

    public void Select()
    {
        SeedPrefab.Type = Seeds[_currentIndex];
        _player.Give(SeedPrefab);
    }

    public void Exit()
    {
       
    }
}
