using UnityEngine;

public class CrabLink : MonoBehaviour
{
    public Crab _firstCrab;
    public Crab _secondCrab;
    private LineRenderer _lineRenderer;
    private float _maxLinkDistance;
    private float _crabDistance;
    private float _springForce = 300;
    private float _dampingFactor = 3f;

    public void InitializeLink(Crab firstCrab, Crab secondCrab, GameObject linkPrefab)
    {
        _firstCrab = firstCrab;
        _secondCrab = secondCrab;
        _maxLinkDistance = firstCrab._radiusZoneLink + 1f;
        _crabDistance = Vector3.Distance(_firstCrab.transform.position, _secondCrab.transform.position);

        _lineRenderer = GetComponent<LineRenderer>();

        if (_lineRenderer == null)
        {
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        linkPrefab.transform.position = secondCrab.transform.position;

        UpdateLinkPositions();

        firstCrab.AddLink(this);
        secondCrab.AddLink(this);
    }

    public void UpdateLinkPositions()
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.SetPosition(0, _firstCrab.transform.position);
            _lineRenderer.SetPosition(1, _secondCrab.transform.position);
        }
    }

    public Crab GetFirstCrab()
    {
        return _firstCrab;
    }

    public Crab GetSecondCrab()
    {
        return _secondCrab;
    }

    public void DestroyLink()
    {
        if (_lineRenderer != null)
        {
            Destroy(_lineRenderer.gameObject);
        }
        Destroy(gameObject);
    }

    private void ApplySpringForce()
    {
        Vector3 direction = _secondCrab.transform.position - _firstCrab.transform.position;
        float currentDistance = direction.magnitude;

        if (currentDistance > _maxLinkDistance)
        {
            ConnectionManager.Instance.DestroyCrabLink(this);
            return;
        }

        direction.Normalize();

        float distanceDelta = currentDistance - _crabDistance;
        Vector3 force = direction * (distanceDelta * _springForce);

        _firstCrab.GetComponent<Rigidbody>().AddForce(force);
        _secondCrab.GetComponent<Rigidbody>().AddForce(-force);

        Vector3 relativeVelocity = _secondCrab.GetComponent<Rigidbody>().velocity - _firstCrab.GetComponent<Rigidbody>().velocity;
        Vector3 dampingForce = relativeVelocity * _dampingFactor;

        _firstCrab.GetComponent<Rigidbody>().AddForce(dampingForce);
        _secondCrab.GetComponent<Rigidbody>().AddForce(-dampingForce);
    }

    private void Update()
    {
        UpdateLinkPositions();
        ApplySpringForce();
    }
}
