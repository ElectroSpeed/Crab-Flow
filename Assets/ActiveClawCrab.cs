using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveClawCrab : MonoBehaviour
{
    [SerializeField] private string _layerName;
    private float _scaleMultiplier = 2f;
    private bool _activeDown = false;
    private bool _activeUp = true;

    private void Update()
    {
        if (this.transform.GetChild(1).GetComponent<Crab>()._linkList.Count >= 1)
        {
            if (_activeUp)
            {
                UpScale();
            }
        }
        else
        {
            if (_activeDown)
            {
                DownScale();
            }
        }
    }

    private void UpScale()
    {
        _activeDown = true;
        _activeUp = false;
        int layer = LayerMask.NameToLayer(_layerName);

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                obj.transform.localScale *= _scaleMultiplier;
                obj.tag = "StaticCrab";
            }
        }
    }

    private void DownScale()
    {
        _activeDown = false;
        _activeUp = true;
        int layer = LayerMask.NameToLayer(_layerName);

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
            {
                obj.transform.localScale /= _scaleMultiplier;
                obj.GetComponent<Crab>().DisconnectAllLinks();
                obj.tag = "Crab";
            }
        }
    }
}
