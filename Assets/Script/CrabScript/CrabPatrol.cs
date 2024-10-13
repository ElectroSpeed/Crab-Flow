using UnityEngine;

public class CrabPatrol : MonoBehaviour
{
    public Transform _targetCrab;
    public float _speed = 20.0f;

    void Update()
    {
        Patrol();
    }
    
    public void Patrol()
    {
        if (_targetCrab == null)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<SphereCollider>().enabled = true;
            return;
        }

        if (_targetCrab.CompareTag("Crab") || !_targetCrab.GetComponent<Crab>()._crabsLinked.Contains(_targetCrab.GetComponent<Crab>()))
        {
            _targetCrab = null;
            return;
        }

        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<SphereCollider>().enabled = false;
        if (transform.position != _targetCrab.position)
        {
            float crabSpeedPatrol = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetCrab.position, crabSpeedPatrol);
            return;
        }

        if (_targetCrab.GetComponent<Crab>()._crabsLinked == null)
        {
            return;
        }

        int crabIndex = Random.Range(0, _targetCrab.GetComponent<Crab>()._crabsLinked.Count);
        _targetCrab = _targetCrab.GetComponent<Crab>()._crabsLinked[crabIndex].transform;
    }

    private void OnMouseDown()
    {
        _targetCrab = null;
    }
}
