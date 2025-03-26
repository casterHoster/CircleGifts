using UnityEngine;

public class EnabledState : IBoardState
{
    public void ExitState(GameObject currentBoard)
    {
        currentBoard.SetActive(false);
    }

    public void EnterState(GameObject currentBoard)
    {
        currentBoard.SetActive(true);
    }
}
