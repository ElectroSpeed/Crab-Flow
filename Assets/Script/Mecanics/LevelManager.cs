using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private float _levelStartTime;
    private float _elapsedTime = 0f;
    public int _totalCrabsCollected = 0;

    public static LevelManager Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        StartLevelTimer();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    private void StartLevelTimer()
    {
        _levelStartTime = 0f;
        _elapsedTime = 0f;
    }

    public void AddCrab()
    {
        _totalCrabsCollected++;
    }

    public float GetElapsedTime()
    {
        return _elapsedTime;
    }

    public int GetTotalCrabsCollected()
    {
        return _totalCrabsCollected;
    }

    public void ResetLevel()
    {
        StartLevelTimer();
    }
}
