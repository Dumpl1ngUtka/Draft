using Services.GameControlService.GridStateMachine;

namespace Grid
{
    public class GridModel
    {
        protected GridStateMachine StateMachine;
        
        public GridModel(GridStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}