using UnityEngine;

namespace BoardsRegulation
{
    public class MenuBoardsRegulator : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsBoard;
        [SerializeField] private GameObject _mainMenuBoard;

        [SerializeField] private MenuRegulator _menuRegulator;

        private StateMachine _stateMachine;

        public void Initial()
        {
            _stateMachine = new StateMachine();
            _stateMachine.AddState(new StateSettings(_stateMachine, _settingsBoard));
            _stateMachine.AddState(new StateMainMenu(_stateMachine, _mainMenuBoard));
            _stateMachine.SetState<StateMainMenu>();

            _menuRegulator.MenuOpened += UseMainMenu;
            _menuRegulator.SettingsOpened += UseSettings;
        }

        private void OnDisable()
        {
            _menuRegulator.MenuOpened -= UseMainMenu;
            _menuRegulator.SettingsOpened -= UseSettings;
        }

        private void UseSettings()
        {
            _stateMachine.SetState<StateSettings>();
        }

        private void UseMainMenu()
        {
            _stateMachine.SetState<StateMainMenu>();
        }
    }
}
