using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{

    private Camera _camera;
    private NavMeshAgent _agent;
    private Animator _animator;
    private Game _game;

    public LayerMask GroundMask;
    public LayerMask ClickableMask;

    private Collider _previousCollider;

    // Use this for initialization
    void Start()
    {
        _camera = Camera.main;
        _game = FindObjectOfType<Game>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!_game.Finished)
        {
            _animator.SetFloat("Speed", _agent.desiredVelocity.magnitude);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Input.GetButton("Fire1"))
            {
                UpdateTargetPosition(ray, out hit);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ClickableMask))
            {
                Vector3 intersectPoint = hit.point; // ray.GetPoint(rayDistance);
                Debug.DrawLine(ray.origin, intersectPoint, Color.blue);
                Collider currentCollider = hit.collider;

                if (_previousCollider != currentCollider)
                {
                    currentCollider.SendMessage("Enter", SendMessageOptions.DontRequireReceiver);
                    if (_previousCollider != null)
                    {
                        _previousCollider.SendMessage("Exit", SendMessageOptions.DontRequireReceiver);
                        _previousCollider = null;
                    }
                    _previousCollider = currentCollider;
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, ClickableMask, QueryTriggerInteraction.Collide);
                    bool colliderFound = false;
                    foreach (Collider c in colliders)
                    {
                        if (c == currentCollider)
                        {
                            currentCollider.SendMessage("Select", SendMessageOptions.DontRequireReceiver);
                            colliderFound = true;
                        }

                    }
                    if (!colliderFound)
                    {
                        _agent.SetDestination(hit.point);
                    }
                }
            }
            else
            {
                if (_previousCollider != null)
                {
                    _previousCollider.SendMessage("Exit", SendMessageOptions.DontRequireReceiver);
                    _previousCollider = null;
                }
            }
            if (_agent.isStopped)
            {
                LookAt(hit.point);
            }
            {
                LookAt(_agent.steeringTarget);
            }
        }
    }

    private void UpdateTargetPosition(Ray ray, out RaycastHit hit)
    {

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GroundMask) && !(Physics.Raycast(ray, Mathf.Infinity, ClickableMask)))
        {
            Vector3 intersectPoint = hit.point; // ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, intersectPoint, Color.red);
            _agent.SetDestination(intersectPoint);
        }
    }

    private void LookAt(Vector3 targetPosition)
    {
        Vector3 lookPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.LookAt(lookPosition);
    }
}
