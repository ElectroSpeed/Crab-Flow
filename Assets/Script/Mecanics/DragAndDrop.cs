using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Vector3 _mousePosition;

    private Camera _cam;
    private LayerMask _layerMask = 1 << 3;

    private void Start()
    {
        _cam = Camera.main;
    }

    private Vector3 GetMousePosition()
    {
        return _cam.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        _mousePosition = Input.mousePosition - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            if (gameObject.GetComponent<Crab>()._canDisconnected || gameObject.CompareTag("Crab"))
            {
                transform.position = _cam.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
            }
        }
        else
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
