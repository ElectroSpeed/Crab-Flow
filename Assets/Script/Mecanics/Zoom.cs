using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Transform _targetCameraPosition;
    private Transform _startCameraPosition;
    private Quaternion _cameraRotation;
    private float _zoomTime = 0f;
    private float _zoomDuration = 3f;
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
            _zoomTime += Time.deltaTime / _zoomDuration;

            transform.position = Vector3.Lerp(_startCameraPosition.position, _targetCameraPosition.position, _zoomTime);

            _cameraRotation = Quaternion.Euler(_targetCameraPosition.eulerAngles.x, _targetCameraPosition.parent.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(_startCameraPosition.rotation, _cameraRotation, _zoomTime);

            if (_zoomTime >= 1f)
            {
                _isZooming = false;
                _zoomTime = 0f;
            }
        }
        else
        {
            if (_targetCameraPosition != null && _targetCameraPosition.parent)
            {
                transform.position = _targetCameraPosition.position;
                transform.eulerAngles = new Vector3(_targetCameraPosition.eulerAngles.x, _targetCameraPosition.eulerAngles.y, 0);
            }
        }
    }

    public void ZoomTo(Transform targetCameraPosition)
    {
        _targetCameraPosition = targetCameraPosition;
        _startCameraPosition = this.transform;

        _cameraRotation = Quaternion.Euler(_targetCameraPosition.eulerAngles.x, _targetCameraPosition.parent.eulerAngles.y, 0);

        _zoomTime = 0f;
        _isZooming = true;
    }
}
