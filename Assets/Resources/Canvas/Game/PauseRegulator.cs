using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseRegulator : MonoBehaviour
{
    [SerializeField] GameObject _pause;
    [SerializeField] GameObject _settings;
    [SerializeField] GameObject _leaderboard;

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
    
    public void OpenPause()
    {
        Time.timeScale = 0f;
        _pause.SetActive(true);
        Paused?.Invoke();
    }

    public void LoadMainMenu()
    { 
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OpenSettings()
    {
        _settings.SetActive(true);
        _pause.SetActive(false);
    }

    public void CloseSettings()
    {
        _settings.SetActive(false);
        _pause.SetActive(true);
    }

    public void OpenLeaderboard()
    {
        _leaderboard.SetActive(true);
        _pause.SetActive(false);
    }

    public void CloseLeaderboard()
    {
        _leaderboard.SetActive(false); 
        _pause.SetActive(true);
    }
}
