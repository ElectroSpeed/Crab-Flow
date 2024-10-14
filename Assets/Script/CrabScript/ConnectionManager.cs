using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager Instance;
    [SerializeField] private GameObject _linkCrabPrefab;
    [SerializeField] private GameObject _visualLinkCrabPrefab;

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

    public void CreateVisualCrabLink(Crab firstCrab, Crab secondCrab)
    {
        GameObject linkObject = Instantiate(_visualLinkCrabPrefab);
        CrabLink crabLink = linkObject.AddComponent<CrabLink>();
        crabLink.InitializeVisualLink(firstCrab, secondCrab, linkObject);
    }

    public void DestroyVisualCrabLink(CrabLink crabLink)
    {
        Crab firstCrab = crabLink.GetFirstCrab();
        Crab secondCrab = crabLink.GetSecondCrab();

        firstCrab.RemoveVisualLink(crabLink);
        secondCrab.RemoveVisualLink(crabLink);

        crabLink.DestroyLink();
    }
}
