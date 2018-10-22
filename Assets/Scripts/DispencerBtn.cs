using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispencerBtn : MonoBehaviour, IClickableObj {

    public Dispenser Dispencer;
    public Color SelectedColor;

    private Material _material;
    private Color _originalColor;

    // Use this for initialization
    void Start () {
        _material = GetComponent<Renderer>().materials[1];
        _originalColor = _material.color;
    }
	
	// Update is called once per frame
	void Update () {
		
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
        Dispencer.NextSeed();
    }
}
