using MAEngine;
using System;
using UnityEngine;
using Zenject;

namespace GameCoreModule
{
    public class GameStateActions : IAction, IInitialisation, ICleanUp
    {
        private GameEventBus _gameEventBus;
        private StateEventsBus _stateEventsBus;
        
        private GameState _currentGameState;

        [Inject]
        public void Construct(GameEventBus gameEventBus, StateEventsBus stateEventsBus)
        {
            _gameEventBus = gameEventBus;
            _stateEventsBus = stateEventsBus;
        }

        public void Initialisation()
        {
            _stateEventsBus.OnPlayStateActivate += SetPlayingState;
            _stateEventsBus.OnPauseStateActivate += SetPauseState;
            SetPlayingState();
        }

        public void Cleanup()
        {
            _stateEventsBus.OnPlayStateActivate -= SetPlayingState;
            _stateEventsBus.OnPauseStateActivate -= SetPauseState;
            
        }

        private void SetPlayingState()
        {
            if (_currentGameState != GameState.PlayState)
            {
                Time.timeScale = 1f;
                _currentGameState = GameState.PlayState;
                _gameEventBus.OnStateChanged?.Invoke(_currentGameState);
            }
        }
        private void SetPauseState()
        {
            if (_currentGameState != GameState.PauseState)
            {
                Time.timeScale = 0f;
                _currentGameState = GameState.PauseState;
                _gameEventBus.OnStateChanged?.Invoke(_currentGameState);
            }
        }
    }
}
