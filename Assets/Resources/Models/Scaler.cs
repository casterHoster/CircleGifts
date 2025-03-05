using UnityEngine;

public class Scaler
{
    private float _screenWidth;
    private float _screenHeight;

    public float Scaling;

    public Scaler()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        CalculateScaling();
    }

    private void CalculateScaling()
    {
        float screenAspectRatio = _screenWidth / _screenHeight;

        if (screenAspectRatio > 1)
        {
            Scaling = _screenHeight / _screenWidth;
        }
        else
        {
            Scaling = _screenWidth / _screenHeight;
        }
    }
}
