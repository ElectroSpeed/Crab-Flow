using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    private Camera Camera;
    private float Speed = 5;
    private float Marge = 50.0f;

    [SerializeField]private int _maxX, _maxY,_minX, _minY;

    void Awake()
    {
        Camera = GetComponent<Camera>();
    }
    void Update()
    {
        MoveCamera();
    }
    private void MoveCamera()
    {
        if (Camera != null)
        {
            if (Camera.transform.position.x < _maxX)
            {
                if (Input.mousePosition.x >= Screen.width - Marge)
                {
                    Camera.transform.position += new Vector3(3, 0, 0) * Time.deltaTime * Speed;
                }
            }

            if (Camera.transform.position.x > _minX)
            {
                if (Input.mousePosition.x <= 0 + Marge)
                {
                    Camera.transform.position -= new Vector3(3, 0, 0) * Time.deltaTime * Speed;
                }
            }


            if (Camera.transform.position.y < _maxY)
            {
                if (Input.mousePosition.y >= Screen.height - Marge)
                {
                    Camera.transform.position += new Vector3(0, 3, 0) * Time.deltaTime * Speed;
                }
            }

            if (Camera.transform.position.y > _minY)
            {
                if (Input.mousePosition.y <= 0 + Marge)
                {
                    Camera.transform.position -= new Vector3(0, 3, 0) * Time.deltaTime * Speed;
                }
            }
        }
    }
}