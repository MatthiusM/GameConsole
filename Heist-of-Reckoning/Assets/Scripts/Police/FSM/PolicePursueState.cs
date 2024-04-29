using PoliceAnimation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public class PolicePursueState : PoliceState
    {
        Vector3 wanderLocation;

        public PolicePursueState(PoliceStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            wanderLocation = LocationManager.PlayerPosition;
            Move(wanderLocation);

            stateMachine.SetRunning(true);

            stateMachine.PoliceCollision.onPlayerEnterTrigger += SwitchToShoot;
        }

        public override void Exit()
        {
            stateMachine.SetRunning(false);
            stateMachine.PoliceCollision.onPlayerEnterTrigger -= SwitchToShoot;
            stateMachine.Agent.ResetPath();
        }

        public override void Update(float deltaTime)
        {
            SwitchToPursue();
            AnimateMovementSpeed(deltaTime);
        }

        private void SwitchToShoot()
        {
            stateMachine.SetCurrentState(new PoliceShootState(stateMachine));
        }

        void SwitchToPursue()
        {
            if (Vector3.Distance(stateMachine.transform.position, wanderLocation) < 0.2f)
            {
                stateMachine.SetCurrentState(new PolicePursueState(stateMachine));
            }
        }

        void AnimateMovementSpeed(float deltaTime)
        {
            float speed = stateMachine.Agent.velocity.magnitude < 0.1f ? 0f : (stateMachine.Running ? 1.0f : 0.5f);
            int hash = stateMachine.PoliceAnimationHashes.GetHash(PoliceParameters.Speed);
            UpdateAnimationFloat(hash, speed, deltaTime);
        }
    }
}

