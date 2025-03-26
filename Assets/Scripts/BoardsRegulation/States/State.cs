using UnityEngine;

namespace BoardsRegulation
{
    public abstract class State : BoardState
    {
        protected GameObject _board;

        public State(StateMachine stateMachine, GameObject board) : base(stateMachine)
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
