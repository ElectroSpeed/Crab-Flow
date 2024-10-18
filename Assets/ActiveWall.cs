using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWall : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private GameObject _wall2;
    private float _scaleMultiplier = 2f;

    private void Update()
    {
        if (this.transform.GetChild(1).GetComponent<Crab>()._linkList.Count >= 1)
        {
            DesactiveWallS();
        }
        else
        {
            ActiveWallS();
        }
    }

    private void DesactiveWallS()
    {
        _wall.SetActive(false);
        _wall2.SetActive(false);
    }

    private void ActiveWallS()
    {
        _wall.SetActive(true);
        _wall2.SetActive(true);
    }
}
