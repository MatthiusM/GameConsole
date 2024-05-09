using PoliceAnimation;
using System;
using System.Collections;
using UnityEngine;

namespace FiniteStateMachine
{
    public class PolicePursueState : PoliceState
    {
        Vector3 targetLocation;
        float detectionRadius = 10.0f; 

        public PolicePursueState(PoliceStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            targetLocation = LocationManager.PlayerPosition;
            Move(targetLocation);

            stateMachine.SetRunning(true);

            stateMachine.PoliceCollision.onPlayerEnterTrigger += SwitchToShoot;
            stateMachine.StartCoroutine(DetectionAndPursuitCheck());
        }
        public override void Exit()
        {
            stateMachine.SetRunning(false);
            stateMachine.PoliceCollision.onPlayerEnterTrigger -= SwitchToShoot;
            stateMachine.Agent.ResetPath();
            stateMachine.StopAllCoroutines(); 
        }
        public override void Update(float deltaTime)
        {
            AnimateMovementSpeed(deltaTime);
        }

        IEnumerator DetectionAndPursuitCheck()
        {
            while (true)
            {
                yield return new WaitForSeconds(3); 
                Collider[] hits = Physics.OverlapSphere(stateMachine.transform.position, detectionRadius);
                bool playerDetected = false;
                foreach (Collider hit in hits)
                {
                    if (hit.CompareTag("Player"))
                    {
                        playerDetected = true;
                        break;
                    }
                }
                if (!playerDetected)
                {
                    Debug.Log("Player lost, switching from Pursue to Wander state.");
                    stateMachine.SetCurrentState(new PoliceWanderState(stateMachine));
                    break;
                }
            }
        }

        private void SwitchToShoot()
        {
            stateMachine.SetCurrentState(new PoliceShootState(stateMachine));
        }

        void AnimateMovementSpeed(float deltaTime)
        {
            float speed = stateMachine.Agent.velocity.magnitude < 0.1f ? 0f : (stateMachine.Running ? 1.0f : 0.5f);
            int hash = stateMachine.PoliceAnimationHashes.GetHash(PoliceParameters.Speed);
            UpdateAnimationFloat(hash, speed, deltaTime);
        }
    }
}