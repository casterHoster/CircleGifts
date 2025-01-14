using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void InitButtons()
    {
        _buttons = new Button[Lines.Size, Lines.Size];

        for (int i = 0; i < Lines.Size * Lines.Size; i++)
            _buttons[i % Lines.Size, i / Lines.Size] = 
                GameObject.Find($"Gift {i}").GetComponent<Button>();

    }
}
