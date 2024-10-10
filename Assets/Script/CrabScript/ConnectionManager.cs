using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager Instance;

    [SerializeField] private GameObject _linkCrabPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateCrabLink(Crab firstCrab, Crab secondCrab)
    {
        GameObject linkObject = Instantiate(_linkCrabPrefab);

        CrabLink crabLink = linkObject.AddComponent<CrabLink>();
        crabLink.InitializeLink(firstCrab, secondCrab, linkObject);
    }

    public void DestroyCrabLink(CrabLink crabLink)
    {
        Crab firstCrab = crabLink.GetFirstCrab();
        Crab secondCrab = crabLink.GetSecondCrab();

        firstCrab.RemoveLink(crabLink);
        secondCrab.RemoveLink(crabLink);

        crabLink.DestroyLink();
    }
}
