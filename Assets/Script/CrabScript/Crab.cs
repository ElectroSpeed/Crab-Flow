using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crab : MonoBehaviour
{
    private GameObject _crab;
    private SphereCollider _crabDetectionZone;
    private bool _canBeLinked = false;
    private bool _canCreateVisualLink = true;
    private bool _onDrag = false;
    private int _linkDetected = 0;
    private Animator _animator;

    [SerializeField] private string _crabName;
    [SerializeField] private int _minimumFirstNumberLink;
    [SerializeField] private int _maximumFirstNumberLink;

    public int _radiusZoneLink;
    public bool _canDisconnected;
    public bool _canConductCurrent;
    public bool _isInvulnerable;
    public bool _isSteelCrab;

    public List<CrabLink> _visualLink = new List<CrabLink>();
    public List<CrabLink> _linkList = new List<CrabLink>();
    public List<Crab> _crabsLinked = new List<Crab>();
    public List<Crab> _linkPotentials = new List<Crab>();

    private void Start()
    {
        _crab = this.gameObject;
        _crabDetectionZone = GetComponent<SphereCollider>();
        _crabDetectionZone.radius = 0.5f;
        _crabDetectionZone.isTrigger = false;
        _animator = this.GetComponent<Animator>();
        if (_isSteelCrab)
        {
            InitialiseSteelLink();
        }
    }

    private void Update()
    {
        if (_linkList.Count <= 1 && _crab.layer != 8)
        {
            if (!_onDrag)
            {
                _crab.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            _crab.transform.eulerAngles = new Vector3(90, 180, 0);
        }
    }

    private void InitialiseSteelLink()
    {
        int layer = 9;

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> steelCrabs = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer && obj != this)
            {
                steelCrabs.Add(obj);
                ConnectionManager.Instance.CreateSteelCrabLink(this, obj.GetComponent<Crab>());
            }
        }
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
            if (_linkList.Count == 1)
            {
                _crab.transform.eulerAngles = new Vector3(90, 180, 0);
                this.tag = "Crab";
                DisconnectAllLinks();
            }
        }
    }

    public void AddVisualLink(CrabLink link)
    {
        if (!_visualLink.Contains(link))
        {
            _visualLink.Add(link);
        }
    }

    public void RemoveVisualLink(CrabLink link)
    {
        if (_visualLink.Contains(link))
        {
            _visualLink.Remove(link);
        }
    }

    private void OnMouseUp()
    {
        _animator.SetInteger("State", 0);
        if (_canBeLinked)
        {
            DestroyVisualLink();
            foreach (Crab potentialCrab in _linkPotentials)
            {
                CrabConnection(this, potentialCrab);
            }
            _linkPotentials.Clear();
            this.tag = "StaticCrab";
        }
        else
        {
            if(_canDisconnected)
            {
                _crab.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        _onDrag = false;
        ResetDetectionZone();
    }

    private void OnMouseDown()
    {
        if (_canDisconnected || this.CompareTag("Crab"))
        {
            _onDrag = true;
            _crab.transform.eulerAngles = new Vector3(90, 180, 0);
            this.tag = "Crab";
            _animator.SetInteger("State", 1);
            DisconnectAllLinks();
            ExpandDetectionZone();
        }
    }

    private void OnMouseDrag()
    {
        _canBeLinked = _linkDetected >= _minimumFirstNumberLink && _linkDetected <= _maximumFirstNumberLink;
        if(_canBeLinked)
        {
            CreateVisualLink();
        }
        else
        {
            DestroyVisualLink();
        }
    }
    
    private void CreateVisualLink()
    {
        if(_canCreateVisualLink)
        {
            _canCreateVisualLink = false;
            foreach (Crab potentialCrab in _linkPotentials)
            {
                ConnectionManager.Instance.CreateVisualCrabLink(this, potentialCrab);
            }
        }
    }

    private void DestroyVisualLink()
    {
        if (!_canCreateVisualLink && _visualLink != null)
        {
            List<CrabLink> visualLinksCopy = new List<CrabLink>(_visualLink);

            foreach (CrabLink visualLink in visualLinksCopy)
            {
                ConnectionManager.Instance.DestroyVisualCrabLink(visualLink);
            }

            _canCreateVisualLink = true;
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Land") && this.CompareTag("StaticCrab"))
        {
            _crab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        if (collision.gameObject.CompareTag("Finish") && this.CompareTag("StaticCrab"))
        {
            collision.gameObject.GetComponent<SphereCollider>().enabled = false;
            MenuEvents.Instance.WinMenu();
            Debug.Log("Win");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Land") && this.CompareTag("Crab"))
        {
            _crab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }
    }

    private void ResetDetectionZone()
    {
        _crabDetectionZone.radius = 0.8f;
        _crabDetectionZone.isTrigger = false;
    }

    private void ExpandDetectionZone()
    {
        _crabDetectionZone.radius = _radiusZoneLink * 2;
        _crabDetectionZone.isTrigger = true;
    }
}
