using MAEngine;

namespace Economy
{
    public class EconomyModule : BasicModule
    {
        
        public override void Initialise()
        {
            base.Initialise();

            InitializeProductionActions();
        }

        private void InitializeProductionActions()
        {
            ProductionActions productionActions =
                _di.Resolve<ProductionActions>();
            _actions.Add(productionActions);
        }
    }
}