namespace NPCBehaviour
{
    public class StateMachine
    {
        protected Agent owner;
        protected IState currentState;

        public StateMachine(Agent owner)
        {
            this.owner = owner;
        }

        public void ChangeState(IState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter();
        }
    }
}