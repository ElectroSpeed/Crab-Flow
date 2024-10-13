using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    public Transform _center;
    public Transform[] _levels;
    public float _rotationSpeed;
    public float _distanceFromCenter;

    void Start()
    {
        PositionLevels();
    }

    void FixedUpdate()
    {
        RotateLevels();
    }

    void PositionLevels()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            float angle = i * Mathf.PI * 2f / _levels.Length;
            Vector3 newPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _distanceFromCenter;
            _levels[i].position = _center.position + newPos;
        }
    }

    void RotateLevels()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].RotateAround(_center.position, Vector3.up, _rotationSpeed * Time.deltaTime);
        }
    }
}
