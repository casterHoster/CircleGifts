using UnityEngine;

namespace BoardsRegulation
{
    public class GameBoardsRegulator : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsBoard;
        [SerializeField] private GameObject _gameFinisherBoard;
        [SerializeField] private GameObject _pauseBoard;
        [SerializeField] private GameFinisher _gameFinisher;
        [SerializeField] private PauseRegulator _pauseRegulator;

        private StateMachine _stateMachine;

        public void Initial()
        {
            _stateMachine = new StateMachine();
            _stateMachine.AddState(new StateSettings(_stateMachine, _settingsBoard));
            _stateMachine.AddState(new StateGameFinisher(_stateMachine, _gameFinisherBoard));
            _stateMachine.AddState(new StatePause(_stateMachine, _pauseBoard));
            _stateMachine.AddState(new StatePlayGame(_stateMachine, _gameFinisherBoard));

            _gameFinisher.GameIsOvered += UseGameFinisher;
            _gameFinisher.GameIsContinued += UsePlayGame;
            _pauseRegulator.Paused += UsePause;
            _pauseRegulator.Resumed += UsePlayGame;
            _pauseRegulator.SettingsOpened += UseSettings;
        }

        private void OnDisable()
        {
            _gameFinisher.GameIsOvered -= UseGameFinisher;
            _gameFinisher.GameIsContinued -= UsePlayGame;
            _pauseRegulator.Paused -= UsePause;
            _pauseRegulator.Resumed -= UsePlayGame;
            _pauseRegulator.SettingsOpened -= UseSettings;
        }

        private void UseSettings()
        {
            _stateMachine.SetState<StateSettings>();
        }

        private void UseGameFinisher()
        {
            _stateMachine.SetState<StateGameFinisher>();
        }

        private void UsePause()
        {
            _stateMachine.SetState<StatePause>();
        }

        private void UsePlayGame()
        {
            _stateMachine.SetState<StatePlayGame>();
        }
    }
}
