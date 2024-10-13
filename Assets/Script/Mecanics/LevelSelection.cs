using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string _worldName;
    private void OnMouseDown()
    {
        if (this.CompareTag("WorldLevel"))
        {
            OnLevelSelected(_worldName);
        }
    }


    private void OnLevelSelected(string name)
    {
        Zoom.Instance.ZoomTo(this.transform);
    }
}
