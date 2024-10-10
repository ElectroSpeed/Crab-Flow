using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    private GameObject _crab;
    [SerializeField] private string _crabName;

    [SerializeField] private int _minimumFirstNumberLink;
    [SerializeField] private int _maximumFirstNumberLink;

    [SerializeField] private Material _crabConnected;
    [SerializeField] private Material _crabDisconnected;

    public int _radiusZoneLink;

    private SphereCollider _crabDetectionZone;

    public bool _canDisconnected;
    public bool _canConductCurrent;
    public bool _isInvulnerable;

    private bool _canBeLinked = false;
    private int _linkDetected = 0;

    public List<CrabLink> _linkList = new List<CrabLink>();
    public List<Crab> _crabsLinked = new List<Crab>();
    public List<Crab> _linkPotentials = new List<Crab>();

    private void Start()
    {
        _crab = this.gameObject;
        _crabDetectionZone = GetComponent<SphereCollider>();
        _crabDetectionZone.radius = 0.5f;
        _crabDetectionZone.isTrigger = false;
    }

    public void CrabConnection(Crab connectedCrab, Crab tiedCrab)
    {
        if (!_crabsLinked.Contains(tiedCrab))
        {
            _crabsLinked.Add(tiedCrab);
            tiedCrab._crabsLinked.Add(connectedCrab);
            ConnectionManager.Instance.CreateCrabLink(connectedCrab, tiedCrab);
        }
    }

    public void CrabDisconnection(Crab disconnectedCrab)
    {
        if (_crabsLinked.Contains(disconnectedCrab))
        {
            _crabsLinked.Remove(disconnectedCrab);
            disconnectedCrab._crabsLinked.Remove(this);

            List<CrabLink> linksToRemove = _linkList.FindAll(link =>
                link.GetFirstCrab() == disconnectedCrab || link.GetSecondCrab() == disconnectedCrab);

            foreach (CrabLink crabLink in linksToRemove)
            {
                ConnectionManager.Instance.DestroyCrabLink(crabLink);
            }
        }
    }

    public void DisconnectAllLinks()
    {
        List<Crab> crabsToDisconnect = new List<Crab>(_crabsLinked);

        foreach (Crab crab in crabsToDisconnect)
        {
            CrabDisconnection(crab);
        }
        ResetCrabState();
    }

    private void ResetCrabState()
    {
        _canBeLinked = false;
        _linkDetected = 0;
        _linkPotentials.Clear();
        ResetDetectionZone();
    }

    public void AddLink(CrabLink link)
    {
        if (!_linkList.Contains(link))
        {
            _linkList.Add(link);
        }
    }

    public void RemoveLink(CrabLink link)
    {
        if (_linkList.Contains(link))
        {
            _linkList.Remove(link);
        }
    }

    private void OnMouseUp()
    {
        if (_canBeLinked)
        {
            foreach (Crab potentialCrab in _linkPotentials)
            {
                CrabConnection(this, potentialCrab);
            }
            _linkPotentials.Clear();
            this.tag = "StaticCrab";
            this.GetComponent<Renderer>().material = _crabConnected;
        }
        ResetDetectionZone();
    }

    private void OnMouseDown()
    {
        if (_canDisconnected || this.CompareTag("Crab"))
        {
            this.GetComponent<Renderer>().material = _crabDisconnected;
            this.tag = "Crab";
            DisconnectAllLinks();
            ExpandDetectionZone();
        }
    }

    private void OnMouseDrag()
    {
        _canBeLinked = _linkDetected >= _minimumFirstNumberLink && _linkDetected <= _maximumFirstNumberLink;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StaticCrab"))
        {
            Crab otherCrab = other.gameObject.GetComponent<Crab>();
            if (otherCrab != null && !_linkPotentials.Contains(otherCrab))
            {
                _linkDetected++;
                _linkPotentials.Add(otherCrab);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StaticCrab"))
        {
            Crab otherCrab = other.gameObject.GetComponent<Crab>();
            if (otherCrab != null && _linkPotentials.Contains(otherCrab))
            {
                _linkDetected--;
                _linkPotentials.Remove(otherCrab);
            }
        }
    }

    private void ResetDetectionZone()
    {
        _crabDetectionZone.radius = 0.5f;
        _crabDetectionZone.isTrigger = false;
    }

    private void ExpandDetectionZone()
    {
        _crabDetectionZone.radius = _radiusZoneLink;
        _crabDetectionZone.isTrigger = true;
    }
}