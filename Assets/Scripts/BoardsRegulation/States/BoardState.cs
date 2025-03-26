
namespace BoardsRegulation
{
    public abstract class BoardState
    {
        protected readonly StateMachine StateMachine;

        public BoardState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}
