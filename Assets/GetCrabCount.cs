using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetCrabCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _CrabCount;

    private void Update()
    {
        _CrabCount.text = "X " + LevelManager.Instance._totalCrabsCollected;
    }
}
