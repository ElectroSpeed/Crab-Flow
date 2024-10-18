using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    public void Finish(string levelName)
    {
        if(SaveManager.Instance.IsLevelComplete(levelName) == false)
        {
            LevelManager.Instance.AddCrab();
            SaveManager.Instance.LevelComplete(levelName);
        }
    }
}
