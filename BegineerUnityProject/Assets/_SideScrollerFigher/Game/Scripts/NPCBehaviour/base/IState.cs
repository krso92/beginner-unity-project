using System.Collections;

namespace NPCBehaviour
{
    public interface IState
    {
        void Enter();

        IEnumerator Execute();
        
        void Exit();
    }
}