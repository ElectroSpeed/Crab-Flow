using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Transform _targetCameraPosition;
    private Transform _startCameraPosition;
    private float _zoomTime = 0f;
    private float _zoomSpeed = 0.1f;
    private bool _isZooming = false;

    public static Zoom Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (_isZooming)
        {
            _zoomTime += Time.deltaTime * _zoomSpeed;
            transform.position = Vector3.Lerp(_startCameraPosition.position, _targetCameraPosition.position, _zoomTime);

            if (_zoomTime >= 1f)
            {
                _isZooming = false;
            }
        }
    }

    public void ZoomTo(Transform targetCameraPosition)
    {
        _targetCameraPosition = targetCameraPosition;
        _startCameraPosition = this.transform;
        _isZooming = true;
    }
}
