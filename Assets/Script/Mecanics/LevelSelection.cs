using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string _worldName;
    [SerializeField] private GameObject _worldPanel;
    [SerializeField] private GameObject _levelPanel;

    private Transform _zoomTransform;
    private void OnMouseDown()
    {
        if (this.CompareTag("WorldLevel"))
        {
            OnLevelSelected(_worldName);
        }
    }

    private void OnLevelSelected(string name)
    {
        _levelPanel.SetActive(false);
        _worldPanel.SetActive(true);
        Zoom.Instance.ZoomTo(this.transform.GetChild(0).transform);
    }
}
