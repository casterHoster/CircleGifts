using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseRegulator : MonoBehaviour
{
    [SerializeField] GameObject _pause;

    public Action Paused;
    public Action Resumed;

    private void Awake()
    {
        _pause.SetActive(false);
    }

    public void Resume()
    {
        _pause.SetActive(false);
        Time.timeScale = 1f;
        Resumed?.Invoke();
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        _pause.SetActive(true);
        Paused?.Invoke();
    }

    public void LoadMainMenu()
    { 
        Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
    }
}
