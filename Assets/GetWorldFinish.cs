using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWorldFinish : MonoBehaviour
{
    [SerializeField] private string _levelName;
    private bool _isFinished;
    void Update()
    {
        _isFinished = SaveManager.Instance.IsLevelComplete(_levelName);
        if (_isFinished)
        {
            this.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            this.GetComponent<Image>().color = Color.black;
        }
    }
}
