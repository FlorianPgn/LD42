using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour {

    private Camera _camera;
    private NavMeshAgent _agent;

    public LayerMask GroundMask;

	// Use this for initialization
	void Start () {
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		if (Input.GetButton("Fire1"))
        {
            UpdateTargetPosition();
        }
	}

    private void UpdateTargetPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, GroundMask))
        {
            Vector3 intersectPoint = hit.point; // ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, intersectPoint, Color.red);
            _agent.SetDestination(intersectPoint);
        }
    }
}
