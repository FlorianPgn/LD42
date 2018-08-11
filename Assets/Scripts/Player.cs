using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

    public Transform CarryTarget;
    private bool carrySomething = false;
    private Carryable _carriedObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Give(Carryable obj)
    {
        print("Give");
        if (!carrySomething || _carriedObject.IsSeed)
        {
            print("New carrying");
            DiscardSeed();
            _carriedObject = Instantiate(obj, CarryTarget.position, Quaternion.identity);
            _carriedObject.transform.SetParent(CarryTarget);
            carrySomething = true;
        }
    }

    public Seed GetSeed()
    {
        print(_carriedObject);
        if (_carriedObject.IsSeed)
            return _carriedObject as Seed;
        else
            return null;
    }

    public void DiscardSeed()
    {
        if (_carriedObject != null && _carriedObject.IsSeed)
        {
            Destroy(_carriedObject.gameObject);
        }
    }
}
