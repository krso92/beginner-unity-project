using System.Collections;
using UnityEngine;

namespace NPCBehaviour
{
    public class StateAttack : IState
    {
        private readonly Agent agent;
        private readonly float reflex;
        private readonly float cooldown;
        private readonly Character2dAnimationHandler animationHandler;
        private readonly Transform target;

        public StateAttack(Agent agent, float reflex, float cooldown, Transform target)
        {
            this.agent = agent;
            this.reflex = reflex;
            this.cooldown = cooldown;
            this.target = target;
            animationHandler = agent.GetComponent<Character2dAnimationHandler>();
        }

        public void Enter()
        {
            agent.StartCoroutine(Execute());
        }

        private bool CanAttack()
        {
            return Vector2.Distance(agent.transform.position, target.position) < 3.5f;
        }

        public IEnumerator Execute()
        {
            // if I can hit him -- hit him
            do
            {
                yield return new WaitForSeconds(reflex);
                animationHandler.Attack();
                Debug.Log("amattaking bitch");
                if (!CanAttack())
                {
                    Exit();
                    yield break;
                }
                yield return new WaitForSeconds(cooldown);
            } while (CanAttack());
            Exit();
        }

        public void Exit()
        {
            Debug.Log("state attack -- exit");
        }
    }
}