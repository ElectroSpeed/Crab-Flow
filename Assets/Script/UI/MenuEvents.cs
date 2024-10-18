using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{
    public static MenuEvents Instance;

    public bool _pauseIsActive = false;
    public bool _settingIsActive = false;
    public bool _startIsActive;

    [SerializeField] GameObject _gameMenu;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _settingsMenu;
    [SerializeField] GameObject _startMenu;
    [SerializeField] GameObject _allLevels;
    [SerializeField] GameObject _winMenu;

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
    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        Time.timeScale = 1.0f;
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void WinMenu()
    {
        Time.timeScale = 0f;
        _gameMenu.SetActive(false);
        _winMenu.SetActive(true);
    }

    public void StartMenu()
    {
        if (!_startIsActive)
        {
            _allLevels.SetActive(false);
            _startMenu.SetActive(true);
            _startIsActive = true;
            return;
        }
        else
        {
            _startMenu.SetActive(false);
            _allLevels.SetActive(true);
            _startIsActive = false;
            return;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayScene(string name)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(name);
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void PauseMenu()
    {
        if (!_pauseIsActive)
        {
            _gameMenu.SetActive(false);
            _pauseMenu.SetActive(true);
            _pauseIsActive = true;
            Time.timeScale = 0.0f;
            return;
        }
        else
        {
            _pauseMenu.SetActive(false);
            _gameMenu.SetActive(true);
            _pauseIsActive = false;
            Time.timeScale = 1.0f;
            return;
        }
    }

    public void SettingsMenu()
    {
        if (!_settingIsActive)
        {
            _startMenu.SetActive(false);
            _settingsMenu.SetActive(true);
            _settingIsActive = true;
            return;
        }
        else
        {
            _settingsMenu.SetActive(false);
            _startMenu.SetActive(true);
            _settingIsActive = false;
            return;
        }
    }
}
