using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour, IClickableObj {

    private Material _material;
    private Player _player;
    private Game _game;

    // Use this for initialization

    private void Start () {
        _material = GetComponent<Renderer>().material;
        _player = FindObjectOfType<Player>();
        _game = FindObjectOfType<Game>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Select()
    {
        Plant playerPlant = _player.GetPlant();
        if (playerPlant != null) {
            if (playerPlant.Type.plant == Carryable.PlantType.ENERGY)
            {
                _game.AddEnergy(20);
                _player.DiscardItem();
            }
            if (playerPlant.Type.plant == Carryable.PlantType.OXYGEN)
            {
                _game.AddOxygen(10);
                _player.DiscardItem();
            }
        }
    }
}
