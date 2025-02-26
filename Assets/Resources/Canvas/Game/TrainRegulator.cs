using System;
using UnityEngine;

public class TrainRegulator : MonoBehaviour
{
    [SerializeField] private GameObject _trainWindow;

    public Action Opened;
    public Action Closed;

    public void Initial()
    {
        Time.timeScale = 0f;
        _trainWindow.SetActive(true);
        Opened?.Invoke();
    }

    public void CloseTrainWindow()
    {
        Time.timeScale = 1f;
        _trainWindow.SetActive(false);
        Closed?.Invoke();
    } 
}
