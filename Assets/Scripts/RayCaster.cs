using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour {

    private Collider _previousCollider;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Selectable")))
        {
            Collider currentCollider = hit.collider;

            if (_previousCollider != currentCollider)
            {
                currentCollider.SendMessage("OnRayEnter", SendMessageOptions.DontRequireReceiver);

                _previousCollider = currentCollider;
            }

            if (Input.GetButton("Fire1"))
            {
                currentCollider.SendMessage("OnRaySelect", SendMessageOptions.DontRequireReceiver);
               
            }
        }
        else
        {
            if (_previousCollider != null)
            {
                _previousCollider.SendMessage("OnRayExit", SendMessageOptions.DontRequireReceiver);
                _previousCollider = null;
            }
        }
    }

    protected virtual void OnSelect(GameObject gameObject)
    {

        Debug.Log(gameObject);
        /*if (gameObject.GetComponent<Movable>())
        {
            Player.GrabObject(gameObject);
        }
        if (gameObject.GetComponent<Pickable>())
        {
            Debug.Log(gameObject);
            Player.PickObject(gameObject);
        }
        if (gameObject.GetComponent<LockScreenAction>())
        {
            Player.LockScreen(gameObject.GetComponent<LockScreenAction>());
        }*/

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(Camera.main.transform.position, (Input.mousePosition - Camera.main.transform.position).normalized);
    }
}