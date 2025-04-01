using MAEngine;

namespace Player
{
    public class PlayerModule : BasicModule
    {
        public override void Initialise()
        {
            base.Initialise();

            InitializePlayerActions();
        }

        private void InitializePlayerActions()
        {
            PlayerActions productionActions =
                _di.Resolve<PlayerActions>();
            _actions.Add(productionActions);
        }
    }
}