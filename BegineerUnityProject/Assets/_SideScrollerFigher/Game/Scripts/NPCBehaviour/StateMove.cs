using System.Collections;
using UnityEngine;
using UnityStandardAssets._2D;

namespace NPCBehaviour
{
    public class StateMove : IState
    {
        private readonly Agent agent;
        private readonly float direction;
        private readonly PlatformerCharacter2D character2D;

        public StateMove(Agent agent, float direction, PlatformerCharacter2D character2D)
        {
            this.agent = agent;
            this.direction = direction;
            this.character2D = character2D;
        }

        public void Enter()
        {
            Debug.Log("state move -- enter");
            agent.StartCoroutine(Execute());
        }

        public IEnumerator Execute()
        {
            while (true)
            {
                character2D.Move(direction, false, false);
                yield return new WaitForFixedUpdate();
            }
        }

        public void Exit()
        {
            Debug.Log("state move -- exit");
        }
    }
}