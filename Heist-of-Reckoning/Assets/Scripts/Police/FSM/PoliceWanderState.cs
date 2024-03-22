using PoliceAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public class PoliceWanderState : PoliceState
    {

        Vector3 wanderLocation;

        public PoliceWanderState(PoliceStateMachine stateMachine) : base(stateMachine)
        {
            
        }

        public override void Enter()
        {
            wanderLocation = stateMachine.LocationManager.GetRandomLocation(stateMachine.transform.position);
            Move(wanderLocation);

            stateMachine.PoliceVision.OnPlayerDetected += SwitchToPursue;
        }

        public override void Exit()
        {
            stateMachine.PoliceVision.OnPlayerDetected -= SwitchToPursue;
        }

        public override void Update(float deltaTime)
        {
            AnimateMovementSpeed(deltaTime);
            SwitchToWander();
        }

        void SwitchToWander()
        {
            if (Vector3.Distance(stateMachine.transform.position, wanderLocation) < 0.2f)
            {
                stateMachine.SetCurrentState(new PoliceWanderState(stateMachine));
            }
        }

        void SwitchToPursue()
        {
            stateMachine.SetCurrentState(new PolicePursueState(stateMachine));
        }

        void AnimateMovementSpeed(float deltaTime)
        {
            float speed = stateMachine.Agent.velocity.magnitude < 0.1f ? 0f : (stateMachine.Running ? 1.0f : 0.5f);
            int hash = stateMachine.PoliceAnimationHashes.GetHash(PoliceParameters.Speed);
            UpdateAnimationFloat(hash, speed, deltaTime);
        }
    }
}

