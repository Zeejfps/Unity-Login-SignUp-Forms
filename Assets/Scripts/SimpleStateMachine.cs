namespace Tests
{
    public sealed class SimpleStateMachine : IStateMachine
    {
        private IState m_State;

        public IState State
        {
            get => m_State;
            set
            {
                if (m_State == value)
                    return;

                var prevState = m_State;
                m_State = value;
                var currState = m_State;

                prevState?.OnExited();
                currState?.OnEntered();
            }
        }
    }
}