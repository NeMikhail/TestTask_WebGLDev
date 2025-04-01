using GameCoreModule;
using MAEngine;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PauseActions : IAction, IInitialisation, ICleanUp
    {
        private PauseUIView _pauseUIView;
        private StateEventsBus _stateEventsBus;

        [Inject]
        public void Construct(PauseUIView pauseUIView, StateEventsBus stateEventsBus)
        {
            _pauseUIView = pauseUIView;
            _stateEventsBus = stateEventsBus;
        }
        
        public void Initialisation()
        {
            _pauseUIView.ContinueButton.Button.onClick.AddListener(ContinueGame);
            _pauseUIView.ExitButton.Button.onClick.AddListener(ExitGame);
            _pauseUIView.SettingsButton.Button.onClick.AddListener(OpenSettings);
            _pauseUIView.SettingsApplyButton.Button.onClick.AddListener(ApplySettings);
            _pauseUIView.SettingsBackButton.Button.onClick.AddListener(CloseSettings);
        }

        public void Cleanup()
        {
            _pauseUIView.ContinueButton.Button.onClick.RemoveListener(ContinueGame);
            _pauseUIView.ExitButton.Button.onClick.RemoveListener(ExitGame);
            _pauseUIView.SettingsButton.Button.onClick.RemoveListener(OpenSettings);
            _pauseUIView.SettingsApplyButton.Button.onClick.RemoveListener(ApplySettings);
            _pauseUIView.SettingsBackButton.Button.onClick.RemoveListener(CloseSettings);
        }
        
        private void ContinueGame()
        {
            _stateEventsBus.OnPlayStateActivate?.Invoke();
        }
        
        private void ExitGame()
        {
            Application.Quit();
        }
        
        private void OpenSettings()
        {
            _pauseUIView.SettingsRectTransform.gameObject.SetActive(true);
            _pauseUIView.PauseRectTransform.gameObject.SetActive(false);
        }
        
        private void CloseSettings()
        {
            _pauseUIView.SettingsRectTransform.gameObject.SetActive(false);
            _pauseUIView.PauseRectTransform.gameObject.SetActive(true);
        }
        
        private void ApplySettings()
        {
            
            CloseSettings();
        }
        

    }
}