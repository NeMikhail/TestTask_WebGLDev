using MAEngine;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameCoreModule
{
    public class GameControlActions : IAction, IInitialisation, ICleanUp
    {
        private StateEventsBus _stateEventsBus;
        private List<GameObject> _screensList;

        [Inject]
        public void Construct(StateEventsBus stateEventsBus, CanvasList canvasList)
        {
            _stateEventsBus = stateEventsBus;
        }

        public void Initialisation()
        {
            
        }

        public void Cleanup()
        {
            
        }
    }
}
