using System.Collections;
using UnityEngine;

namespace NPCBehaviour
{
    public class Agent : MonoBehaviour
    {
        private StateMachine fsm;

        private void Awake()
        {
            fsm = new StateMachine(this);
        }

        public IEnumerator StateWrapper(IEnumerator state)
        {
            while (state.MoveNext())
            {
                yield return state.Current;
            }
        }
    }
}