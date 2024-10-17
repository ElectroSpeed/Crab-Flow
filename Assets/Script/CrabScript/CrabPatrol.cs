using UnityEngine;

public class CrabPatrol : MonoBehaviour
{
    public Transform _targetCrab;
    public float _speed = 20.0f;

    private Camera _cam;
    private LayerMask _layerMask;
    private LayerMask _layerMaskRegister; // Masque de couche pour les layers 6 et 7
    private Ray _ray;
    private RaycastHit _hitCrab;

    void Start()
    {
        _cam = Camera.main;

        // Définir le layer mask pour inclure les layers 6 et 7
        _layerMask = (1 << 6) | (1 << 7);  // Utilisation du OR bit-à-bit pour combiner les layers
    }

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

    // Utilisation du raycasting dans OnMouseDrag pour les layers 6 et 7
    private void OnMouseDrag()
    {
        _ray = _cam.ScreenPointToRay(Input.mousePosition);

        // Lancer le rayon avec le layer mask pour les layers 6 et 7
        if (Physics.Raycast(_ray, out _hitCrab, Mathf.Infinity, _layerMask))
        {
            // Vérifier si l'objet touché a le tag "Crab" ou "CrabLink"
            if (_hitCrab.collider.CompareTag("Crab") || _hitCrab.collider.CompareTag("CrabLink"))
            {
                if (_layerMask == 6)
                {
                    _layerMaskRegister = 6;
                }
                else if (_layerMask == 7)
                {
                    _layerMaskRegister = 7;
                }
                // Traitement si l'objet est un Crab ou CrabLink
                Debug.Log("Objet touché : " + _hitCrab.collider.tag);

                // Ajouter ici la logique de déplacement ou de gestion selon le tag
                _targetCrab = _hitCrab.collider.transform;
            }
        }
        else
        {
            _layerMaskRegister = 0;
        }
    }

    private void OnMouseUp()
    {
        if(_layerMaskRegister == 6)
        {
            _targetCrab = _hitCrab.transform;
        }
        else if (_layerMaskRegister == 7)
        {
            _targetCrab = _hitCrab.transform.gameObject.GetComponent<CrabLink>()._firstCrab.transform;
        }
    }
}
