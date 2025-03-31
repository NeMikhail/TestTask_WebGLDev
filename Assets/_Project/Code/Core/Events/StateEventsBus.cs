using System;

namespace GameCoreModule
{
    public class StateEventsBus
    {
        private Action _onPlayStateActivate;
        private Action _onPauseStateActivate;

        public Action OnPlayStateActivate => _onPlayStateActivate;
        public Action OnPauseStateActivate => _onPauseStateActivate;
    }
}
