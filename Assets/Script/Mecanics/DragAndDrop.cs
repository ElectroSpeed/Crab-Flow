using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Vector3 _mousePosition;

    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        _mousePosition = Input.mousePosition - GetMousePosition();
        this.GetComponent<SphereCollider>().isTrigger = false;
    }

    private void OnMouseDrag()
    {
        if (gameObject.GetComponent<Crab>()._canDisconnected || gameObject.CompareTag("Crab"))
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        }
    }
}
