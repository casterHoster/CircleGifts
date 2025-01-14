using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PresenterGame : MonoBehaviour
{
    private Button[,] _buttons;
    private Image[] _images;
    private Lines _cells;

    private void Start()
    {
        _cells = new Lines(ShowCell);
        InitButtons();
        _cells.Start();
    }

    public void ShowCell(float x, float y, float gift)
    {
    
    }

    public void Click()
    {

        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    private void InitButtons()
    {
        _buttons = new Button[Lines.Size, Lines.Size];

        for (int i = 0; i < Lines.Size * Lines.Size; i++)
            _buttons[i % Lines.Size, i / Lines.Size] = 
                GameObject.Find($"Button ({i})").GetComponent<Button>();

    }

    private int GetNumber(string name)
    {
        Regex regex = new Regex("\\((\\d+\\))");
    }
}
