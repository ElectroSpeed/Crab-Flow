using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private Dictionary<string, bool> _levelCompletionStatus = new Dictionary<string, bool>
    {
        { "FirstWorldFirstLevel", false },
        { "FirstWorldSecondLevel", false },

        { "SecondWorldFirstLevel", false },
        { "SecondWorldSecondLevel", false },

        { "ThirdWorldFirstLevel", false },
        { "ThirdWorldSecondLevel", false }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        foreach (var level in _levelCompletionStatus)
        {
            PlayerPrefs.SetInt(level.Key, level.Value ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        foreach (var level in _levelCompletionStatus.Keys)
        {
            _levelCompletionStatus[level] = PlayerPrefs.GetInt(level, 0) == 1;
        }
    }

    public void LevelComplete(string levelName)
    {
        Debug.Log("Enter");
        if (_levelCompletionStatus.ContainsKey(levelName))
        {
            _levelCompletionStatus[levelName] = true;
            SaveData();
        }
        else
        {
            Debug.LogError("Invalid level name: " + levelName);
        }
    }

    public bool IsLevelComplete(string levelName)
    {
        if (_levelCompletionStatus.ContainsKey(levelName))
        {
            return _levelCompletionStatus[levelName];
        }
        else
        {
            Debug.LogError("Invalid level name: " + levelName);
            return false;
        }
    }
}
