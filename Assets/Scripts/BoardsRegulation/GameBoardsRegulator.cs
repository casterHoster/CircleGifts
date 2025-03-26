using UnityEngine;

namespace BoardsRegulation
{
    public class GameBoardsRegulator : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsBoard;
        [SerializeField] private GameObject _gameFinisherBoard;
        [SerializeField] private GameObject _pauseBoard;

        [SerializeField] private GameFinisher gameFinisher;
        [SerializeField] private PauseRegulator _pauseRegulator;


        private StateMachine _stateMachine;

        public void Initial()
        {
            _stateMachine = new StateMachine();
            _stateMachine.AddState(new StateSettings(_stateMachine, _settingsBoard));
            _stateMachine.AddState(new StateGameFinisher(_stateMachine, _gameFinisherBoard));
            _stateMachine.AddState(new StatePause(_stateMachine, _pauseBoard));
            _stateMachine.AddState(new StatePlayGame(_stateMachine, _gameFinisherBoard));

            gameFinisher.GameIsOver += UseGameFinisher;
            gameFinisher.GameIsContinued += UsePlayGame;
            _pauseRegulator.Paused += UsePause;
            _pauseRegulator.Resumed += UsePlayGame;
            _pauseRegulator.SettingsOpened += UseSettings;
        }

        public void OnDisable()
        {
            gameFinisher.GameIsOver -= UseGameFinisher;
            gameFinisher.GameIsContinued -= UsePlayGame;
            _pauseRegulator.Paused -= UsePause;
            _pauseRegulator.Resumed -= UsePlayGame;
            _pauseRegulator.SettingsOpened -= UseSettings;
        }

        public void UseSettings()
        {
            _stateMachine.SetState<StateSettings>();
        }

        public void UseGameFinisher()
        {
            _stateMachine.SetState<StateGameFinisher>();
        }

        public void UsePause()
        {
            _stateMachine.SetState<StatePause>();
        }

        public void UsePlayGame()
        {
            _stateMachine.SetState<StatePlayGame>();
        }
    }
}
