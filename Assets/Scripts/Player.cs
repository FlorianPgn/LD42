using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

    public Transform CarryTarget;
    public float MoveSpeed;

    private bool carrySomething = false;
    private Carryable _carriedObject;
    private Rigidbody _rb;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector3.right * Input.GetAxisRaw("Horizontal") * MoveSpeed + Vector3.forward * Input.GetAxisRaw("Vertical") * MoveSpeed;
    }

    public bool Give(Carryable obj)
    {
        print("Give");
        if (!carrySomething || (carrySomething && _carriedObject.IsSeed))
        {
            if (obj.IsSeed)
            {
                print("Carrying new seed");
                DiscardItem();
                _carriedObject = Instantiate(obj, CarryTarget.position, Quaternion.Euler(Vector3.right * -90));
                _carriedObject.transform.SetParent(CarryTarget);
                carrySomething = true;
            }
            else
            {
                print("Carrying new plant");
                Plant plant = obj as Plant;
                _carriedObject = plant;
                plant.Harvest();
                plant.transform.SetParent(CarryTarget);
                plant.transform.localPosition = Vector3.zero;
                plant.transform.localScale *= 2f;
                carrySomething = true;
            }
            return true;
        }
        return false;
    }

    public Seed GetSeed()
    {
        print(_carriedObject);
        if (_carriedObject != null)
        {
            if (_carriedObject.IsSeed)
                return _carriedObject as Seed;
        }
        return null;
    }

    public Plant GetPlant()
    {
        print(_carriedObject);
        if (_carriedObject != null)
        {
            if (!_carriedObject.IsSeed)
                return _carriedObject as Plant;
        }
        return null;
    }

    public void DiscardItem()
    {
        if (_carriedObject != null)
        {
            Destroy(_carriedObject.gameObject);
            carrySomething = false;
        }
    }
}
