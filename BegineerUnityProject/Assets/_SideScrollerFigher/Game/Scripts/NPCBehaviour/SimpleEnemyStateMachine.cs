using UnityEngine;

namespace NPCBehaviour
{
    public class SimpleEnemyStateMachine : StateMachine
    {
        public SimpleEnemyStateMachine(Agent owner) : base(owner)
        {
            if (owner == null)
            {
                Debug.LogError("Must have agent attached");
            }
        }
    }
}