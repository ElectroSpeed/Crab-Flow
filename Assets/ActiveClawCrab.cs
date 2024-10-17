using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveClawCrab : MonoBehaviour
{
    [SerializeField] private string _layerName;
    private float _scaleMultiplier = 2f;


    private void Start()
    {
        UpScale();
    }

    private void UpScale()
    {
        int layer = LayerMask.NameToLayer(_layerName);

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                obj.transform.localScale *= _scaleMultiplier;
            }
        }
    }

    private void DownScale()
    {
        int layer = LayerMask.NameToLayer(_layerName);

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                obj.transform.localScale /= _scaleMultiplier;
            }
        }
    }
}
