using UnityEngine;

public interface IBoardState
{
    void EnterState(GameObject gameObject);
    void ExitState(GameObject gameObject);
}
