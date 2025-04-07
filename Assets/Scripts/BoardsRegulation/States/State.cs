using UnityEngine;

namespace BoardsRegulation
{
    public abstract class State : BoardState
    {
        private GameObject _board;

        public State(GameObject board)
        {
            _board = board;
        }

        public override void Enter()
        {
            _board.SetActive(true);
        }

        public override void Exit()
        {
            _board.SetActive(false);
        }
    }
}
